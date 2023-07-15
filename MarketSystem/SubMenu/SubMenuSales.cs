using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Console.WriteLine("1.Add new sales");
                Console.WriteLine("2.Remove the sale");
                Console.WriteLine("3.Delete sale in number");
                Console.WriteLine("4.Show all sales");
                Console.WriteLine("5.Show sales in period");
                Console.WriteLine("6.Show amount period in sales");
                Console.WriteLine("7.Showing sales on a given date");
                Console.WriteLine("8.Showing sales on a given number");
                Console.WriteLine("0.Go back");

                Console.WriteLine("-----------");
                Console.WriteLine("Enter option:");


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
