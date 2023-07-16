using ConsoleTables;
using MarketSystem.Common.Models;
using MarketSystem.Services;
using MarketSystem.SubMenu;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace MarketSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ProductService productService = new ProductService();
            SaleService salesService = new SaleService();

            
            SaleMenu saleMenu = new SaleMenu();


            //Console.ForegroundColor = ConsoleColor.Green;  // metod yaz 
            //Console.WriteLine("Enter");
            //Console.ResetColor();


            Products products = new Products()
            {
                Id = 0,
                ProductName = "Milk",
                ProductPrice = 2,
                Category = 0,
                Count = 54
            };

            Products products1 = new Products()
            {
                Id = 5,
                ProductName = "Bread",
                ProductPrice = 7,
                Category = 0,
                Count = 232
            };
            Products products2 = new Products()
            {
                Id = 3,
                ProductName = "Apple",
                ProductPrice = 23,
                Category = 0,
                Count = 112
            };

            ProductService.Products.Add(products);
            ProductService.Products.Add(products1);
            ProductService.Products.Add(products2);

            int option;

           
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;  // metod yaz 
                var table = new ConsoleTable("Numbers", "Description");
                table.AddRow(1, "Operate on products");
                table.AddRow(2, "Operate on sales");
                table.AddRow(0, "Exit");

                


                table.Write(Format.Minimal);

                Console.WriteLine("-----------");
                Console.Write("Enter option: ");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid number!");
                    Console.WriteLine("-----------");
                    Console.Write("Enter option: ");
                }

                // Clear the console before the next iteration
                //Console.Clear();
                Console.ResetColor();

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


                }


            } while (option != 0);
        }


    }
}