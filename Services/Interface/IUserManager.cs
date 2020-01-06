using Services.CustomModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interface
{
    public interface IUserManager
    {
        string Register(RegisterModel registerModel);
        string Login(LoginModel loginModel);
        string UpdateUser(UpdateModel updateModel);
        string DeleteUser(int id);
        string DeleteUser(string email);

    }
}
