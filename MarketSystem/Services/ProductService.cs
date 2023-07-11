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
    public class ProductService
    {
        static List<Products> Products;
        List<Sales> Sales;
        List<SalesItems> SalesItems;

      
        public ProductService()
        {
            Products = new();
            Sales = new();
            SalesItems = new();
        }
        public List<Products> GetProducts()
        {
            return Products;
        }
        public static int AddProduct(string name, decimal price,
            object productCategory, int count)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new FormatException("Name is empty!");

            if (price < 0)
                throw new FormatException("Price is lower than 0!");
           
            
            if (count < 0)
                throw new FormatException("Invalid count!");
            
            var newProduct = new Products
            {
                ProductName = name,
                ProductPrice = price,
                Count = count,
                Category = (ProductCategory)productCategory
            };

            ProductService.Products.Add(newProduct);    

            return newProduct.Id;
        }

        public void DeleteProduct(int productID)
        {
            var existingProduct = Products.FirstOrDefault(x => x.Id == productID);
            if (existingProduct == null)
                throw new Exception($"Product with ID {productID} not found");
            Products = Products.Where(x => x.Id != productID).ToList();
        }

        public void ShowAllProductCategory(string category)
        {
            bool IsSuccessfull = Enum.TryParse(typeof(ProductCategory), category, true, 
                out object parsedCategory);
            if (!IsSuccessfull)
            {
                throw new FormatException("Not Found");
            }
        }

        
        public void SearchByName(string productname)
        {
            var searchname = ProductService.Products.FindAll(x => x.ProductName.ToLower().Trim() == productname.ToLower()).ToList();
            foreach (var name in searchname)
            {
                Console.WriteLine(name.ProductName);
            }
        }




    }
}
