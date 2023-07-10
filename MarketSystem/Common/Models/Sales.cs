using MarketSystem.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSystem.Common.Models
{
    public class Sales: BaseEntity
    {
        private static int _count = 0;
        public Sales()
        {
            Id = _count;
            _count++;
        }
        public int Number { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public List <SalesItems> Items { get; set; }

    }
}
