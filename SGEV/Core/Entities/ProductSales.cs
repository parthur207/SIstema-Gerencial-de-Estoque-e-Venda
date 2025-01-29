using SGEV.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEV.Core.Entities
{
    internal class ProductSales
    {
        public int Id { get; private set; }
        public string ProductName { get; private set; }
        public int Year { get; private set; }
        public Month month { get; private set; }
        public decimal Price { get; private set; }
        public decimal TotalValue { get; private set; }
        public int SalesQuantity { get; private set; }
        


    }
}
