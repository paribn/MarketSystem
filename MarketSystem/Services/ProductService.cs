using ConsoleTables;
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
        public static List<Products> Products;


        public ProductService()
        {
            Products = new List<Products>();
        }
        public static List<Products> GetProducts()
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

        public static void DeleteProduct(int productID)
        {
            var existingProduct = Products.FirstOrDefault(x => x.Id == productID);
            if (existingProduct == null)
                throw new Exception($"Product with ID {productID} not found");
            Products = Products.Where(x => x.Id != productID).ToList();
        }
        public static void ShowAllProducts()
        {
            try
            {
                var products = GetProducts();
                var table = new ConsoleTable("ID", "Name", "Count", "Price", "Category");
                if (products.Count == 0)
                {
                    Console.WriteLine("NO PRODUCT YET");
                    return;
                }
                foreach (var product in products)
                {
                    table.AddRow(product.Id, product.ProductName, product.Count, product.ProductPrice,
                         product.Category);
                }
                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
            }
        }

        public static void ListEnum()
        {
            foreach (var item in Enum.GetNames(typeof(ProductCategory)))
            {
                Console.WriteLine(item);
            }
        }


        public static void SearchByName(string productname)
        {
            var searchname = ProductService.Products.Find(x => x.ProductName.ToLower().Trim() == productname.ToLower());
            if (searchname == null)
            { throw new Exception($"There is no {productname} product"); }
             Products = Products.Where(x => x.ProductName != productname).ToList();
           
        }


        public static void UpdateProduct(string name,string name1)
        {
            var upDate = Products.FirstOrDefault(x => x.ProductName == name);
            upDate.ProductName= name1;
            Console.WriteLine(upDate);
        }
    }
}
