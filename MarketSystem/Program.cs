using ConsoleTables;
using MarketSystem.Common.Models;
using MarketSystem.Services;
using MarketSystem.SubMenu;
using System.Collections.Generic;

namespace MarketSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ProductService productService = new ProductService();

            //Console.ForegroundColor = ConsoleColor.Green;  // metod yaz 
            //Console.WriteLine("Enter");
            //Console.ResetColor();


            Products products = new Products()
            {
                Id = 0,
                ProductName = "Milk",
                ProductPrice = 2,
                Category = 0,
                Count = 3
            };


            Products products1 = new Products()
            {
                Id = 5,
                ProductName = "Bread",
                ProductPrice = 7,
                Category = 0,
                Count = 1
            };
            Products products2 = new Products()
            {
                Id = 3,
                ProductName = "Apple",
                ProductPrice = 23,
                Category = 0,
                Count = 4
            };

            ProductService.Products.Add(products);
            ProductService.Products.Add(products1);
            ProductService.Products.Add(products2);


            int option;

            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;  // metod yaz 
                var table = new ConsoleTable("Numbers", "Description");
                // Add rows to the table
                table.AddRow(1, "Operate on products");
                table.AddRow(2, "Operate on sales");
                table.AddRow(0, "Exit");

                // Print the table
                table.Write();

                // Prompt the user for input
                Console.WriteLine("-----------");
                Console.Write("Enter option: ");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid number!");
                    Console.WriteLine("-----------");
                    Console.Write("Enter option: ");
                }

                // Clear the console before the next iteration
                Console.Clear();


                switch (option)
                {
                    case 1:
                        SubMenuProduct.ProductMenu();
                        break;
                    case 2:
                        SubMenuSales.SalesMenu();
                        break;
                    case 0:
                        Console.WriteLine(new ConsoleTable("Bye!"));
                        break;
                    default:
                        Console.WriteLine(new ConsoleTable("No such option!"));
                        break;


                }   Console.ResetColor();
                


            } while (option != 0);
        }
    }
}