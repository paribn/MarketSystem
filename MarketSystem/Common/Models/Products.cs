using MarketSystem.Common.Base;
using MarketSystem.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSystem.Common.Models
{
    public class Products : BaseEntity
    {
        private static int _count = 0;
        public Products()
        {
            Id = _count;
            _count++;
        }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        public int Count { get; set; }
        public ProductCategory Category { get; set; }


    }
}
