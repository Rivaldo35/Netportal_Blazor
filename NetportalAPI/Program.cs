using DataAccess.Data;
using DataAccess.DbAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetportalAPI.Data;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy", opt =>
    opt.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IUserData, UserData>();
builder.Services.AddAuthentication(Options =>
{
    Options.DefaultAuthenticateScheme = "JwtBearer";
    Options.DefaultChallengeScheme = "JwtBearer";
})
    .AddJwtBearer("JwtBearer", jwtBearerOptions =>
    {
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Secrets:SecurityKey"))),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)

        };
    });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
