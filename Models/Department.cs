using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Department : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
