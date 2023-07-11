using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSystem.Common.Abstract
{
    public interface ISalesMenu
    {
        public void AddNewSale();
        public void RemoveSale();
        public void DeleteSale();
        public void ShowAllSales();
        public void ShowAllSalesDatebyPeriod();
        public void ShowSalesbyPrice();
        public void ShowSalesDate();
        public void SearchSalesNumber();


    }
}
