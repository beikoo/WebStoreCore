using Models;
using Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.CustomModels
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage = MessageAndVariables.passRequired)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,16}$", ErrorMessage = MessageAndVariables.passError)]
        public string Password { get; set; }
        [Required(ErrorMessage = MessageAndVariables.passError)]
        [Compare("Password", ErrorMessage = MessageAndVariables.passError)]
        public string ConfirmPassword { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        public string LastName { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
