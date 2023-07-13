using MarketSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSystem.Services
{
    public class SaleService
    {
        public static List<Sales> Sales;

        public SaleService() 
        {
          Sales = new List<Sales>();
        }

    }
}
