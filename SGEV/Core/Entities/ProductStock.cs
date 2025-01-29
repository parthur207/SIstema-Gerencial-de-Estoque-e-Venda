using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEV.Core.Entities
{
    internal class ProductStock
    {
        public int Id{ get; private set; }

        public string ProductName { get; private set; }

        public string Category { get; private set; }    

        public int Quantity { get; private set; }

        public decimal Price { get; private set; }

    }
}
