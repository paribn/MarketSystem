using MarketSystem.Common.Enum;
using MarketSystem.Common.Interface;
using MarketSystem.Common.Models;
using MarketSystem.SubMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSystem.Services
{
    public class MarketService : IMarketable
    {
        List<Products> Products;
        List<Sales> Sales;
        List<SalesItems> SalesItems;

        public MarketService()
        {
            Products = new();
            Sales = new();
            SalesItems = new();
        }
        public List<Products> GetProducts()
        {
            return Products;
        }
        public int AddNewProducut(string name, decimal price,
            string code, string productcategory, int count)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new FormatException("Name is empty!");
            if (price < 0)
                throw new FormatException("Price is lower than 0!");
            if (string.IsNullOrWhiteSpace(code))
                throw new FormatException("Invalid code");
            if (string.IsNullOrWhiteSpace(productcategory))
                throw new FormatException("Choose category!");
            if (count < 0)
                throw new FormatException("Invalid count");
            bool isSuccessful = Enum.TryParse(typeof(ProductCategory), productcategory, true, out object parsedCategory);
            if (!isSuccessful)
            {
                throw new InvalidDataException("Category not found");
            }
            var newProduct = new Products
            {
                ProductName = name,
                ProductPrice = price,
                Code = code,
                Count = count,
                Category = (ProductCategory)parsedCategory
            };
            Products.Add(newProduct);
            return newProduct.Id;
        }

        public void AddSale()
        {
            throw new NotImplementedException();
        }

        public void CommonRemoveSale()
        {
            throw new NotImplementedException();
        }

        public void ShowAllProducts()
        {
            List<Products> GetProducts()
            {
                return Products;
            }
        }

        public void RemoveByDate()
        {
            throw new NotImplementedException();
        }

        public void RemoveByNumber()
        {
            throw new NotImplementedException();
        }

        public void RemoveByPayment()
        {
            throw new NotImplementedException();
        }

        public void RemoveByPeriod()
        {
            throw new NotImplementedException();
        }

        public void RemoveProductByCategory()
        {
            throw new NotImplementedException();
        }

        public void RemoveProductByName()
        {
            throw new NotImplementedException();
        }

        public void RemoveProductByPrice()
        {
            throw new NotImplementedException();
        }

        public void RemoveSale()
        {
            throw new NotImplementedException();
        }

        public void UpdateProductCategory()
        {
            throw new NotImplementedException();
        }

        public void ShowAllSales()
        {
            throw new NotImplementedException();
        }
    }
}
