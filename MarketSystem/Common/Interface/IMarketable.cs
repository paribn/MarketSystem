using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSystem.Common.Interface
{
    public interface IMarketable
    {
        void Sales();
        void Product();
        void AddSale();
        void RemoveSale();
        void CommonRemoveSale();
        void RemoveByPeriod();
        void RemoveByDate();
        void RemoveByPayment();
        void RemoveByNumber();
        void AddNewProducut();
        void UpdateProductCategory();
        void RemoveProductByCategory();
        void RemoveProductByPrice();
        void RemoveProductByName();

    }
}
