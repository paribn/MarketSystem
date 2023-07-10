using MarketSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSystem.Common.Interface
{
    public interface IMarketable
    {
        void ShowAllSales();
        void ShowAllProducts();
        void AddSale();
        void RemoveSale();
        void CommonRemoveSale();
        void RemoveByPeriod();
        void RemoveByDate();
        void RemoveByPayment();
        void RemoveByNumber();
        int AddNewProducut(string name, decimal price,
            string code, string productcategory, int count);
        void UpdateProductCategory();
        void RemoveProductByCategory();
        void RemoveProductByPrice();
        void RemoveProductByName();

    }
}
