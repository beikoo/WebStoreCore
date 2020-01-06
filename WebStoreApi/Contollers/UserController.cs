using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.CustomModels;
using Services.Implementation;
using Services.Interface;

namespace WebStoreApi.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserManager userManager;
        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
           
        }

        [HttpGet]
        public ActionResult Test()
        {
            return Ok("ddadsA");
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            string result =  userManager.Register(registerModel);

            return Ok(result);
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            string loginResult = userManager.Login(loginModel);
            if (loginResult.Length > 0)
            {
                return Ok(loginResult);
            }
            return Unauthorized("fuck you");
        }
    }
}