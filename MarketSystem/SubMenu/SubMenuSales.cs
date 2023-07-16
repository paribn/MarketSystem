using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using MarketSystem.Services;

namespace MarketSystem.SubMenu
{
    public class SubMenuSales
    {
        public static void SalesMenu()
        {
            Console.Clear();
            int option;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;  // metod yaz 

                var table = new ConsoleTable("Numbers", "Description");
                table.AddRow(1, "Add new sales");
                table.AddRow(2, "Remove the sale");
                table.AddRow(3, "Delete sale in code");
                table.AddRow(4, "Show all sales");
                table.AddRow(5, "Display sales date range");
                table.AddRow(6, "Display sales amount range");
                table.AddRow(7, "Showing sales on a given date");
                table.AddRow(8, "Showing sales on a given number");
                table.AddRow(0, "Go back");
               
                Console.WriteLine("-----------");
                Console.WriteLine("Enter option:");
                table.Write(Format.Minimal);
                Console.ResetColor();

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid number!");
                    Console.WriteLine("-----------");
                    Console.WriteLine("Enter option:");
                }

                switch (option)
                {
                    case 1:
                        SaleMenu.AddNewSale();
                        break;
                    case 2:
                        SaleMenu.ReturnSale();
                        break;
                    case 3:
                        SaleMenu.DeleteSale();
                        break;
                    case 4:
                        SaleMenu.ShowAllSales();
                        break;
                    case 5:
                        SaleMenu.ShowAllSalesDatebyPeriod();
                        break;
                    case 6:
                        SaleMenu.ShowSalesbyPriceRange();
                        break;
                    case 7:
                        SaleMenu.ShowSalesDate();
                        break;
                    case 8:
                        SaleMenu.SearchSalesNumber();
                        break;
                    case 0:
                        Console.WriteLine("Return to main menu");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

            } while (option != 0);
        }

    }
}
