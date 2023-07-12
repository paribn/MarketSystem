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
    public class ProductsMenuService :IProductMenu
    {
        private static ProductService productService= new();

      
        public static void AddNewProduct()
        {
            Console.Clear();
            try
            {
                ProductService.ListEnum();
                Console.WriteLine("Enter your category");
                string category = Console.ReadLine().Trim();


                bool IsSuccessful = Enum.TryParse(typeof(ProductCategory), category, true,
                out object parsedCategory);
                if (!IsSuccessful)
                {
                    throw new InvalidDataException("not found");
                }

                Console.WriteLine("----------------");

                Console.WriteLine("Enter Product name:");
                string name = Console.ReadLine();
                Console.WriteLine("----------------");

                Console.WriteLine("Enter product price");
                decimal price = Decimal.Parse(Console.ReadLine());
                Console.WriteLine("----------------");


                Console.WriteLine("Enter product count");
                int count = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("----------------");

                int id= ProductService.AddProduct(name, price, parsedCategory, count);

                Console.WriteLine( $"Successfuly added product {id}");

                ProductService.ShowAllProducts();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteProduct()
        {
            try
            {
                Console.WriteLine("Enter product ID :");
                int productId = int.Parse(Console.ReadLine());
                ProductService.DeleteProduct(productId);
                Console.WriteLine($"Successfully deleted product with ID: {productId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
            }
        }

        public static void ShowAllCategoryProduct()
        {
            throw new NotImplementedException();
        }
        
        public static void ShowAllProduct()
        {
            ProductService.ShowAllProducts();
        }

        public static void ShowProductbyName()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Search product by name");
                string name = Console.ReadLine();
                ProductService.SearchByName(name);
                
                Console.WriteLine("-------------");
                Console.WriteLine($"Successfully found:{name}");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);

            }
            
        }

        public static void ShowProductbyPrice()
        {
            throw new NotImplementedException();
        }

        public static void UpdateProduct()
        {
            
        }
    }
}
