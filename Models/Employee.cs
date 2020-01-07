using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Employee : BaseModel
    {
        public string EmployeeNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int? PersonId { get; set; }
        
        public virtual Person Person { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<EmployeeCustomer> EmployeeCustomers { get; set; }
    }
}
