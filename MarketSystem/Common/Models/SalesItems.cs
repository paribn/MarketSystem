using MarketSystem.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSystem.Common.Models
{
    public class SalesItems :BaseEntity
    {
        private static int _count = 0;
        public SalesItems()
        {
            Id = _count;
            _count++;
        }
        

        public int SaleItemNum { get; set; }
        public int Number { get; set; }
        public Products product { get; set; }
        public int count { get; set; }

        public static void ResetSaleItems()
        {
            _count = 1;
        }
        
    }
}
