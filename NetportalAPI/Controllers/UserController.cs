﻿using NetportalAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Models;
using NetportalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace NetportalAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly Netportal_AuthDbContext _authDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserData _userData;
        private readonly ILogger<UserController> _logger;

        public UserController(ApplicationDbContext context, Netportal_AuthDbContext authDbContext, UserManager<IdentityUser> userManager, IUserData userData, ILogger<UserController> logger)
        {
            _context = context;
            _authDbContext = authDbContext;
            _userManager = userManager;
            _userData = userData;
            _logger = logger;
        }
        [HttpGet]
        public UserModel GetById()
        {
            string UserId = User.Claims.FirstOrDefault(t => t.Type == "UserName")?.Value.ToString();
            var test = _userData.GetUserById(UserId);
            return test.Result;

        }

        public record UserRegistrationModel
            (
            string FirstName,
            string LastName,
            string EmailAdress,
            string Password
            );


        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationModel user)
        {
            if (ModelState.IsValid)
            {
                var userexisting = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Email.Trim() == user.EmailAdress.Trim());
                    var existingUser = await _userManager.FindByEmailAsync(user.EmailAdress);
                if (existingUser == null)
                {
                    IdentityUser newUser = new()
                    {
                        Email = user.EmailAdress,
                        EmailConfirmed = true,
                        UserName = user.EmailAdress
                    };

                    IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
                    if (result.Succeeded)
                    {
                        var exisingUser = await _userManager.FindByEmailAsync(user.EmailAdress);
                        if (exisingUser is null)
                        {
                            return BadRequest();
                        }
                        UserModel u = new()
                        {
                            Id = exisingUser.Id,
                            voornaam = user.FirstName,
                            achternaam = user.LastName,
                            email = exisingUser.Email
                        };
                        await _userData.InsertUser(u);
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllusers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> output = new List<ApplicationUserModel>();

            var users = _context.Users.ToList();
            var userRoles = from ur in _context.UserRoles
                            join r in _context.Roles on ur.RoleId equals r.Id
                            select new { ur.UserId, ur.RoleId, r.Name };
            foreach (var user in users)
            {
                ApplicationUserModel u = new ApplicationUserModel
                {
                    Id = user.Id,
                    Email = user.Email
                };

                u.Roles = userRoles.Where(x => x.UserId == u.Id).ToDictionary(x => x.RoleId, x => x.Name);
                //foreach (var r in user.Roles)
                //{
                //    u.Roles.Add(r.RoleId, roles.Where(x => x.Id == r.RoleId).First().Name);
                //}
                output.Add(u);
            }
            return output;
        } 
        //[HttpGet]
        //[Route("GetAllusers")]
        //public List<ApplicationUserModel> GetAuthApps()
        //{
        //    List<Applicatie> output = new ();

        //    var users = _context.Users.ToList();
        //    var userRoles = from ur in _context.UserRoles
        //                    join r in _context.Roles on ur.RoleId equals r.Id
        //                    select new { ur.UserId, ur.RoleId, r.Name };
        //    foreach (var user in users)
        //    {
        //        ApplicationUserModel u = new ApplicationUserModel
        //        {
        //            Id = user.Id,
        //            Email = user.Email
        //        };

        //        u.Roles = userRoles.Where(x => x.UserId == u.Id).ToDictionary(x => x.RoleId, x => x.Name);
        //        //foreach (var r in user.Roles)
        //        //{
        //        //    u.Roles.Add(r.RoleId, roles.Where(x => x.Id == r.RoleId).First().Name);
        //        //}
        //        output.Add(u);
        //    }
        //    return output;
        //}
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            var roles = _context.Roles.ToDictionary(x => x.Id, x => x.Name);

            return roles;

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/AddRole")]
        public async Task AddARole(UserRolePairModel pairing)
        {
            var LoggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var LoggedInUser = _userData.GetUserById(LoggedInUserId);

            var user = await _userManager.FindByIdAsync(pairing.UserId);
            _logger.LogInformation("Admin {Admin} added user {User} to role {Role}",
                LoggedInUserId, user.Id, pairing.RoleName);

            await _userManager.AddToRoleAsync(user, pairing.RoleName);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/RemoveRole")]
        public async Task RemoveARole(UserRolePairModel pairing)
        {
            var LoggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var LoggedInUser = _userData.GetUserById(LoggedInUserId);
            var user = await _userManager.FindByIdAsync(pairing.UserId);

            _logger.LogInformation("Admin {Admin} removed user {User} to role {Role}",
       LoggedInUserId, user.Id, pairing.RoleName);

            await _userManager.RemoveFromRoleAsync(user, pairing.RoleName);

        }
    }
}
