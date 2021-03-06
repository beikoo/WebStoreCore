﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class Person:BaseModel
    {
        public Person()
        {
            UserTokens = new HashSet<UserToken>();
            UserRoles = new HashSet<UserRoles>();
        }
        [Required(ErrorMessage = "First name is required!")]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required!")]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 5)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
