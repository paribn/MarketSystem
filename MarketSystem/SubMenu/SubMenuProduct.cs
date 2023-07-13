using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using MarketSystem.Common.Models;
using MarketSystem.Services;

namespace MarketSystem.SubMenu
{
    public class SubMenuProduct
    {
        public static void ProductMenu()
        {
            Console.Clear();
            int option;
            do
            {
               
                var table = new ConsoleTable("Numbers", "Description");
                table.AddRow(1, "Add new product");
                table.AddRow(2, "Update the products");
                table.AddRow(3, "Remove product");
                table.AddRow(4, "Show all products");
                table.AddRow(5, "Show products by category");
                table.AddRow(6, "Show products by price range");
                table.AddRow(7, "Search products by name");
                table.AddRow(0,"Go back");
                //Console.WriteLine("1.Add new product");
                //Console.WriteLine("2.Update the products");
                //Console.WriteLine("3.Remove product");
                //Console.WriteLine("4.Show all products");
                //Console.WriteLine("5.Show products by category ");
                //Console.WriteLine("6.Show products by price range");
                //Console.WriteLine("7.Search products by name");
                //Console.WriteLine("0.Go back");
                table.Write();
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
                        ProductsMenu.AddNewProduct();
                            break;
                    case 2:
                        ProductsMenu.UpdateProduct();
                        break;
                    case 3:
                        ProductsMenu.DeleteProduct();
                        break;
                    case 4:
                        ProductsMenu.ShowAllProduct();
                        break;
                    case 5:
                        ProductsMenu.ShowAllCategoryProduct();
                        break;
                    case 6:
                        ProductsMenu.ShowProductPricebyRange();
                        break;
                    case 7:
                        ProductsMenu.ShowProductbyName();
                        break;
                    case 0:
                        Console.WriteLine("BYE!");
                        break;
                    default:
                        break;
                }

            } while (option != 0);
        }

    }
}

