using ConsoleTables;
using MarketSystem.Common.Enum;
using MarketSystem.Common.Interface;
using MarketSystem.Common.Models;
using MarketSystem.SubMenu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            string productCategory, int count)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new FormatException("Name is empty!");

            if (price < null)
                throw new FormatException("Price is lower than 0!");


            if (count < null)
                throw new FormatException("Invalid count!");
            bool isSuccessful
                = Enum.TryParse(typeof(ProductCategory), productCategory, true, out object parsedDepartment);

            if (!isSuccessful)
            {
                throw new InvalidDataException("Department not found!");
            }

            var newProduct = new Products
            {
                ProductName = name,
                ProductPrice = price,
                Count = count,
                Category = (ProductCategory)parsedDepartment
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

        /// <summary>
        /// This method returns the list of all products
        /// </summary>
        public static void ShowAllProducts()
        {
            try
            {
                var products = GetProducts();
                var table = new ConsoleTable("ID", "Name", "Count", "Price", "Category");
                if (products.Count == default)
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


        public static List<Products> SearchByName(string productname)
        {
            var searchname = ProductService.Products.Where(x => x.ProductName.ToLower().Trim() == productname.ToLower().Trim()).ToList();
            if (searchname == null)
            { throw new Exception($"There is no {productname} product"); }

            return searchname;
        }

        public static void ShowAnyKindOfProductlistInTable(List<Products> productsList)
        {
            try
            {
                var products = productsList;
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



        public static void UpdateProduct(int Id, string name, int count, object category, decimal price)
        {
            var Update = Products.FirstOrDefault(x => x.Id == Id);
            if (Update == null)
                throw new Exception($"{Id} is invalid");

            Update.ProductName = name;
            Update.ProductPrice = price;
            Update.Count = count;

        }



        public static void ShowPriceRange(decimal minPrice, decimal maxPrice)
        {
            var range = Products.Where(x => x.ProductPrice >= minPrice && x.ProductPrice <= maxPrice).ToList();
            if (minPrice > maxPrice) throw new Exception("Minimum price cannot be greater than the maximum price.");

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
            return;
        }

        public static void ShowAllCategory(object productCategory)
        {
            foreach (var item in Enum.GetNames(typeof(ProductCategory)))
            {
                Console.WriteLine(item);
            }

            if (!Enum.IsDefined(typeof(ProductCategory), productCategory))
            {
                Console.WriteLine("Invalid category number!");

                return;

            }


            ProductCategory selectedCategory = (ProductCategory)productCategory;

            Console.WriteLine($"Showing products in the {selectedCategory} category:");

            var productsInCategory = Products.Where(x => x.Category == selectedCategory);

            foreach (var product in productsInCategory)
            {
                Console.WriteLine($"Name: {product.ProductName}");
                Console.WriteLine($"Quantity: {product.Count}");
                Console.WriteLine($"Price: {product.ProductPrice}");
                Console.WriteLine();
            }

        }
    }
}
