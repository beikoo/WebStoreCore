using Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories.Interfaces;
using Services.Common;
using Services.CustomModels;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Services.Implementation
{
    public class UserManager : IUserManager
    {
        private readonly IUserTokensRepository tokenRepository;
        private readonly IUsersRepository repository;
        private readonly TokenModel _tokenManagement;
        private Person User;
        public UserManager(IUserTokensRepository tokenRepository, IUsersRepository data, IOptions<TokenModel> tokenManagement)
        {
            this.tokenRepository = tokenRepository;
            this.repository = data;
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
                var token = GenerateUserToken(new RequestTokenModel() { Email = loginModel.Email });
                if (token.Length > 0)
                {
                    var usertoken = new UserToken() { Token = token, User = User };
                    tokenRepository.Add(usertoken);
                    tokenRepository.SaveChanges();
                    return token;
                }
            }
            return string.Empty;
        }

        private bool IsValidUser(LoginModel loginModel)
        {
            var currentUser = this.repository.GetByEmail(loginModel.Email);
            if (currentUser != null)
            {
                var res = this.VerifyHashedPassword(currentUser.Password, loginModel.Password);
                User = currentUser;
                return res;
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

        public string Register(RegisterModel registerModel)
        {
            using (WebStoreDbContext context = new WebStoreDbContext())
            {
                try
                {
                    Person user = context.People.Where(x => x.Email == registerModel.Email).SingleOrDefault();
                    if (user == null)
                    {
                        Person person = new Person()
                        {
                            Email = registerModel.Email,
                            Password = PasswordHash.GenerateHash(registerModel.Password), // PasswordHash
                            FirstName = registerModel.FirstName,
                            LastName = registerModel.LastName,
                            CreatedAt = DateTime.Now,
                            IsDeleted = false

                        };
                        context.People.Add(person);
                        context.SaveChanges();

                        string token = GenerateUserToken(new RequestTokenModel() { Email = registerModel.Email });
                        return token;
                    }
                    return "Not Registered";
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
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
              new Claim(ClaimTypes.Email, request.Email)
            };

            //for (int i = 0; i < User.UserRoles.Count; i++)
            //{
            //    claim.Add(new Claim(ClaimTypes.Role, User.UserRoles.ToList()[i].Role.RoleName));
            //}
                
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MessageAndVariables.salt));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwttoken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );

            token = new JwtSecurityTokenHandler().WriteToken(jwttoken);
            return token;
        }
    }
}
