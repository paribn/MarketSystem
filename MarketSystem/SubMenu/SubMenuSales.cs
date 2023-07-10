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
                       
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }

            } while (option != 0);
        }

    }
}
