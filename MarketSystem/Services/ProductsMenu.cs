using ConsoleTables;
using MarketSystem.Common.Enum;
using MarketSystem.Common.Interface;
using MarketSystem.Common.Models;
using MarketSystem.SubMenu;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketSystem.Services
{
    public class ProductsMenu : IProductMenu
    {
        private static ProductService productService = new();

       
        public static void AddNewProduct()
        {
            try
            {
                Console.WriteLine("Enter your category");
                Console.WriteLine("----------------");

                ProductCategory selectedCategory = GetCategory();
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

                int iD = ProductService.AddProduct(name, price, selectedCategory.ToString(),count);

                Console.WriteLine($"Successfully added product with code {iD}");

                ProductService.ShowAllProducts();

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }
        }


        public static void UpdateProduct()
        {
            try
            {
                // Prompt for and read the code of the product to update
                Console.WriteLine("Enter the code:");
                int code = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the new product's name:");
                string newName = Console.ReadLine();

                Console.WriteLine("Enter the new count:");
                int newCount = int.Parse(Console.ReadLine());

                // Display the available categories
                Console.WriteLine("Available categories:");
                foreach (ProductCategory category in Enum.GetValues(typeof(ProductCategory)))
                {
                    Console.WriteLine($"{(int)category}. {category}");
                }
                Console.WriteLine("Enter the category of the new product (number):");
                int categoryNumber = int.Parse(Console.ReadLine());

                // Validate the entered category number
                if (!Enum.IsDefined(typeof(ProductCategory), categoryNumber))
                {
                    Console.WriteLine("Invalid category number!");
                    return;
                }
                ProductCategory newCategory = (ProductCategory)categoryNumber;

                Console.WriteLine("Enter the new price:");
                decimal newPrice = decimal.Parse(Console.ReadLine());

                // Call the ProductService to update the product
                ProductService.UpdateProduct(code, newName, newCount, categoryNumber, newPrice);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }
        }

        public static void DeleteProduct()
        {
           
            Console.WriteLine("Enter product ID:");
            string input = Console.ReadLine();

            if(!int.TryParse(input, out int productId))
            {
                Console.WriteLine("Invalid product ID format.");
                return;
            }

            ProductService.DeleteProduct(productId);
        }


        public static void ShowAllProduct()
        {
            ProductService.ShowAllProducts();
        }


        public static void ShowAllCategoryProduct()
        {
            try
            {

                Console.WriteLine("All categories:");
                foreach (ProductCategory category in Enum.GetValues(typeof(ProductCategory)))
                {
                    Console.WriteLine($"{(int)category}. {category}");
                }

                Console.WriteLine("Enter the category (number):");
                int categoryNumber = Convert.ToInt32(Console.ReadLine());

                ProductService.ShowAllCategory(categoryNumber);

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }

        }


        public static void ShowProductPricebyRange()
        {
            try
            {
                Console.WriteLine("Enter min price");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Eter max price ");
                decimal price1 = decimal.Parse(Console.ReadLine());
                ProductService.ShowPriceRange(price, price1);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }
        }


        public static void ShowProductbyName()
        {

            try
            {
                Console.WriteLine("Search product by name");
                Console.WriteLine("<><><><><><><>><><><><>");
                string name = Console.ReadLine().Trim().ToLower();

                ProductService.SearchByName(name);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }

        }


        private static ProductCategory GetCategory()
        {
            foreach (ProductCategory category in Enum.GetValues(typeof(ProductCategory)))
            {
                Console.WriteLine($"{(int)category}. {category}");
            }

            int selectedCategoryNumber;
            while (!int.TryParse(Console.ReadLine(), out selectedCategoryNumber) || !Enum.IsDefined(typeof(ProductCategory), selectedCategoryNumber))
            {
                Console.WriteLine("Please choose a valid category number:");
            }

            return (ProductCategory)selectedCategoryNumber;
        }


    }
}

