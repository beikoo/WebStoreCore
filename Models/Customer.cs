using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Customer : BaseModel
    {
        public string AccountNumber { get; set; }
        public int? PersonId { get; set; }
        [Required]
        public virtual Person Person { get; set; }
        public virtual ICollection<EmployeeCustomer> EmployeeCustomers { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
