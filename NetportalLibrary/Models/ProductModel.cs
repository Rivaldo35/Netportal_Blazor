using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netportal.Library.Models
{
    public class ProductModel
    {
        //select Id, ProductName, Description, RetailPrice, QuantityInStock
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal RetailPrice { get; set; }
        public int QuantityInStock { get; set; }
        public bool IsTaxable { get; set; }

    }
}
