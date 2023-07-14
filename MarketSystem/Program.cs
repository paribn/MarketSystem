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
            SaleService sealeService = new SaleService();

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
            Sales sales = new Sales()
            {
                Amount = 1,
                Id = 1,
                Date = DateTime.Now,


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

            SaleService.Sales.Add(sales);
            int option;

           
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;  // metod yaz 
                var table = new ConsoleTable("Numbers", "Description");
                table.AddRow(1, "Operate on products");
                table.AddRow(2, "Operate on sales");
                table.AddRow(0, "Exit");

                


                table.Write();

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