using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using NuGet.Protocol;
using Schools.Helpers;
using Schools.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Schools.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SchoolDbContext _context;
        //private readonly JWT _jwt;
        public AuthService(UserManager<IdentityUser> UserManager, SchoolDbContext context)
        {
            _userManager = UserManager;
            _context = context;
            // _jwt = JWT.Value;
        }
        public async Task<AuthModel> Registeration(RegisterModel model)
        {
            AuthModel authModel = new();
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                authModel.Message = "Email is already exist";
                return authModel;
            }
            else if (await _userManager.FindByNameAsync(model.UserName) is not null)
            {
                authModel.Message = "UserName is already exist";
                return authModel;
            }
            var user = new ApplicationUsers
            {
                Email = model.Email,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                JoinedDate = model.JoinedDate,
                UserType = model.UserType
            };
            var Result = await _userManager.CreateAsync(user, model.Password);

            if (Result.Succeeded)
            {
                switch (model.UserType)
                {
                    case UserType.Student:
                        await _userManager.AddToRoleAsync(user, "User");
                        Students student = new()
                        {
                            SchoolUserId= user.Id,
                            
                        };
                        _context.Add(student);
                        _context.SaveChanges();
                        break;
                    case UserType.Teacher:
                        await _userManager.AddToRoleAsync(user, "Admin");
                        Teachers teacher = new()
                        {
                            SchoolUserId = user.Id,
                        };
                        _context.Add(teacher);
                        _context.SaveChanges();
                        break;
                    default:
                        authModel.Message = "Please select the user type!";
                        break;
                }
                var Token = await CreateJwtToken(user);
                authModel.Username = user.UserName;
                authModel.UserType = user.UserType;
                authModel.IsAuthinticate = true;
                authModel.Expiration = Token.ValidTo;
                authModel.Token = new JwtSecurityTokenHandler().WriteToken(Token);
                return authModel;
            }
            else
            {
                var Errors = "Please check the following errors \n";
                foreach (var item in Result.Errors)
                {
                    Errors += $"{item.Description} \n";
                }
                return authModel;
            }
        }

        private async Task<JwtSecurityToken> CreateJwtToken(IdentityUser user)
        {

            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("oIqM4uz46E5Jno3wxx2nBnoWs8Qhz5GYf+ena7dZEAc="));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<AuthModel> SignIn(SignInModel model)
        {
            AuthModel authModel = new();
            if (model.UserName is null && model.Email is null)
            {
                authModel.Message = "Please enter username or Email";
                return authModel;
            }
            var user = model.UserName is not null ? await _userManager.FindByNameAsync(model.UserName) : await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                authModel.Message = "Incorrect username or password";
                return authModel;
            }
            else if (await _userManager.CheckPasswordAsync(user, model.Password) is false)
            {
                authModel.Message = "Incorrect username or password";
                return authModel;
            }
            else
            {
                var Token = await CreateJwtToken(user);
                authModel.Username = user.UserName;
                authModel.Email = user.Email;
                authModel.IsAuthinticate = true;
                authModel.Expiration = Token.ValidTo;
                authModel.Token = new JwtSecurityTokenHandler().WriteToken(Token);
                await _userManager.SetAuthenticationTokenAsync(user, "JWT", $"{authModel.Username}Token", authModel.Token);
                return authModel;
            }


        }
    }
}