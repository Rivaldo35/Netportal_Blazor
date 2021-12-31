using NetportalAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Novell.Directory.Ldap;

namespace EPSApi.Controllers
{
    public class TokenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Netportal_AuthDbContext _authDbContext;

        public TokenController(ApplicationDbContext context, UserManager<IdentityUser> userManager, Netportal_AuthDbContext authDbContext)
        {
            _context = context;
            _userManager = userManager;
            _authDbContext = authDbContext;
        }
        [Route("/token")]
        [HttpPost]
        public async Task<IActionResult> Create(string username, string password, string grant_type)
        {
            if (await IsActiveUser(username))
            {
                if (await IsInternalUser(username))
                {
                    if (await ValidateAccountAd(username, password))
                    {
                        return new ObjectResult(await GenerateToken(username));
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    if (await ValidateAccountDb(username, password))
                    {
                        return new ObjectResult(await GenerateToken(username));
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            else
            {
                return BadRequest();
            }

        }

        private async Task<dynamic> GenerateToken(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var roles = from ur in _context.UserRoles
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where ur.UserId == user.Id
                        select new { ur.UserId, ur.RoleId, r.Name };
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Email, username),
            new Claim (ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            var token = new JwtSecurityToken(
                new JwtHeader(

                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKeyIsSrecretSoDoNotTell")),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = username
            };

            return output;
        }
        private async Task<bool> IsValidUsernameAndPassword(string username, string password)
        {

            var user = await _userManager.FindByEmailAsync(username);
            return await _userManager.CheckPasswordAsync(user, password);
        }
        private async Task<bool> IsActiveUser(string username)
        {
            var isActive = await _authDbContext.Users.AnyAsync(a => a.Username.Equals(username) && a.Status != "Disabled");
            return isActive;
        }
        private async Task<bool> IsInternalUser(string username)
        {
            var isInternal = await _authDbContext.Users.AnyAsync(a => a.Username.Equals(username) && a.InternalUser == "y");
            return isInternal;
        }
        private async Task<bool> ValidateAccountAd(string username, string password)
        {
            //Creating an LdapConnection instance
            var ldapConn = new LdapConnection { SecureSocketLayer = true };

            //Connect function will create a socket connection to the server -Port 389 for insecure and 3269 for secure
            await ldapConn.ConnectAsync("cbmdc1.cbs.local", 3269);

            try
            {
               await ldapConn.BindAsync(username + "@cbvs.sr", password);
                return ldapConn.Bound;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> ValidateAccountDb(string username, string password)
        {
            var account = await _authDbContext.Users.SingleOrDefaultAsync(a => a.Username.Equals(username));
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.EnhancedVerify(password, account.Password))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
