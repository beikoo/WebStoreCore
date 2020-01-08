using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories.Interfaces;
using Services.Common;
using Services.CustomModels;
using Services.CustomModels.MapperSettings;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Implementation
{
    public class UserManager : IUserManager
    {
        private readonly TokenModel _tokenManagement;
        private WebStoreDbContext dbContext;
        private Person User;

        public UserManager(WebStoreDbContext data, IOptions<TokenModel> tokenManagement)
        {
            this.dbContext = data;
            this._tokenManagement = tokenManagement.Value;
        }

        public string DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public string DeleteUser(string email)
        {
            throw new NotImplementedException();
        }

        public string Login(LoginModel loginModel)
        {
            if (this.IsValidUser(loginModel))
            {
                var token = this.GenerateUserToken(new RequestTokenModel() { Email = loginModel.Email, Roles = MapperConfigurator.Mapper.Map<List<RoleModel>>(User.UserRoles.Select(x => x.Role).ToList()) });
                if (token.Length > 0)
                {
                    dbContext.UserTokens.Add(new UserToken() { Token = token, User = User });
                    dbContext.SaveChanges();

                    return token;
                }
            }

            return "";
        }

        private bool IsValidUser(LoginModel loginModel)
        {
            var currentUser = this.dbContext.Users
               .Include(x => x.UserRoles)
               .ThenInclude(x => x.Role)
               .SingleOrDefault(x => x.Email == loginModel.Email);

            if (currentUser != null)
            {
                var res = this.VerifyHashedPassword(currentUser.Password, loginModel.Password);
                User = currentUser;
                return res;
            }

            return false;
        }
        private bool isRegistered(string email)
        {
            var check = this.dbContext.Users.SingleOrDefault(x => x.Email == email);
            if (check != null)
            {
                return true;
            }
            return false;
        }

        private bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            string pass = PasswordHash.GenerateHash(password);
            if (hashedPassword == pass)
            {
                return true;
            }

            return false;
        }

        public string Register(RegisterModel model)
        {
            if (this.isRegistered(model.Email) == false)
            {
                User = new Person();

                var token = GenerateUserToken(new RequestTokenModel() { Email = model.Email, Roles = MapperConfigurator.Mapper.Map<List<RoleModel>>(model.Roles) });

                User = MapperConfigurator.Mapper.Map<Person>(model);

                User.Password = HashPassword(model.Password);

                var userToken = new UserToken() { Token = token, User = User };
                if (model.Roles != null)
                {

                    var getRolesFromDb =
                    this.dbContext
                    .Roles
                    .Where(x => model.Roles.Select(z => z.RoleName).Contains(x.RoleName)).ToList();

                    foreach (var role in getRolesFromDb)
                    {
                        User.UserRoles.Add(new UserRoles() { Role = role });

                    }
                }

                this.dbContext.Users.Add(User);
                this.dbContext.UserTokens.Add(userToken);
                this.dbContext.SaveChanges();

                return token;

            }
            return "";
        }

        public string UpdateUser(UpdateModel updateModel)
        {
            throw new NotImplementedException();
        }

        private string GenerateUserToken(RequestTokenModel request)
        {
            string token = string.Empty;

            var claim = new List<Claim>()
            {
              new Claim(ClaimTypes.Email, request.Email),

            };

            if (request.Roles != null)
            {
                for (int i = 0; i < request.Roles.Count; i++)
                {
                    claim.Add(new Claim(ClaimTypes.Role, request.Roles.ToList()[i].RoleName));
                }
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("Password is empty");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

    }
}
