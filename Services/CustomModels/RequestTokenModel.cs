using System;
using System.Collections.Generic;
using System.Text;

namespace Services.CustomModels
{
    public class RequestTokenModel
    {
        public string Email { get; set; }
        public ICollection<RoleModel> Roles { get; set; }

    }
}
