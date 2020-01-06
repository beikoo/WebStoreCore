using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class EmployeeCustomer:BaseModel
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
