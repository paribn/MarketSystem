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
         void Sales();
         void Products();
         void AddSale();
    
         void DeleteProduct();
         void CommonDeleteSale();
         void DeleteByDateSale();
         void DeleteByPeriodSale();
         void DeleteByPaymentSale();
        
        void DeleteByIdSale();
        void AddProduct();
        void UpdateProductName();
        void UpdateProductQuantity();
        void UpdateProductPrice();
        void UpdateProductCategory();
        void RemoveProductByCategory();
        void RemoveProductByPrice();
        void RemoveProductByName();

    }
}
