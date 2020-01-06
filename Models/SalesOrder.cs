using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SalesOrder:BaseModel
    {
        public int Quantity { get; set; }
        public DateTime DateOFSale { get; set; }
        public string Note { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
