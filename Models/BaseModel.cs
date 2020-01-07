using Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BaseModel : IAuditInfo, IBaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
       
    }
}
