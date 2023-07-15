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
    public class ProductsMenu : IProductMenu
    {
        private static ProductService productService = new();

        ///<summary>
        /// This method returns the list of all add new products
        /// </summary>
        public static void AddNewProduct()
        {
            // Console.Clear();
            try
            {
                ProductService.ListEnum();
                Console.WriteLine("Enter your category");

                Console.WriteLine("----------------");
                string category = Console.ReadLine().Trim().ToLower();

                 
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

                int iD = ProductService.AddProduct(name, price, category, count);

                Console.WriteLine($"Successfully added product with code {iD}");

                ProductService.ShowAllProducts();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops! Got an error!{ex.Message}");
            }
        }

        public static void UpdateProduct()
        {
            try
            {

                Console.WriteLine("Enter the code:");
                int code = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the new name:");
                string newName = Console.ReadLine();

                Console.WriteLine("Enter the new quantity:");
                int newQuantity = int.Parse(Console.ReadLine());

                Console.WriteLine("Available categories:");
                foreach (ProductCategory category in Enum.GetValues(typeof(ProductCategory)))
                {
                    Console.WriteLine($"{(int)category}. {category}");
                }
                Console.WriteLine("Enter the category (number) of the new product:");
                int categoryNumber = int.Parse(Console.ReadLine());

                if (!Enum.IsDefined(typeof(ProductCategory), categoryNumber))
                {
                    Console.WriteLine("Invalid category number!");
                    return;
                }
                ProductCategory newCategory = (ProductCategory)categoryNumber;

                Console.WriteLine("Enter the new price:");
                decimal newPrice = decimal.Parse(Console.ReadLine());
                ProductService.UpdateProduct(code, newName, newQuantity, categoryNumber, newPrice);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");

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

                //  ProductService.ShowAnyKindOfProductlistInTable(ProductService.ShowAllCategory(categoryNumber));

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Oops! Got an error!{ex.Message}");

            }

        }

        public static void ShowProductPricebyRange()
        {
            Console.WriteLine("Enter min price");

            decimal price = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Eter max price ");
            decimal price1 = decimal.Parse(Console.ReadLine());
            ProductService.ShowPriceRange(price,price1);
        }

        public static void ShowProductbyName()
        {

            try
            {
                Console.WriteLine("Search product by name");
                string name = Console.ReadLine();
                ProductService.SearchByName(name);

                Console.WriteLine("-------------");
                ProductService.SearchByName(name);
                ProductService.ShowAnyKindOfProductlistInTable(ProductService.SearchByName(name));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

        }


    }
}

