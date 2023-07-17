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
            SaleService saleService = new SaleService();
            ProductsMenu menu = new ProductsMenu();
            SaleMenu saleMenu = new SaleMenu();

            int option;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan; 
                var table = new ConsoleTable("Numbers", "Description");
                table.AddRow(1, "Operate on products");
                table.AddRow(2, "Operate on sales");
                table.AddRow(0, "Exit..");


                table.Write(Format.Minimal);

                Console.WriteLine("-----------");
                Console.Write("Enter option: ");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid number!");
                    Console.WriteLine("-----------");
                    Console.Write("Enter option: ");
                }

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
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(new ConsoleTable("Bye!"));
                        Console.ResetColor();
                        break;
                    default:
                        Console.WriteLine(new ConsoleTable("No such option!"));
                        break;

                }
            } while (option != 0);

            Console.ReadLine();
        }
    }
}