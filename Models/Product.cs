using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
