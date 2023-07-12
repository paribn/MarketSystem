using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Console.WriteLine("1.Add new product");
                Console.WriteLine("2.Update the products");
                Console.WriteLine("3.Remove product");
                Console.WriteLine("4.Show all products");
                Console.WriteLine("5.Show products by category ");
                Console.WriteLine("6.Show products by price range");
                Console.WriteLine("7.Search products by name");
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
                        ProductsMenuService.AddNewProduct();
                            break;
                    case 2:
                        ProductsMenuService.UpdateProduct();
                        break;
                    case 3:
                        ProductsMenuService.DeleteProduct();
                        break;
                    case 4:
                        ProductsMenuService.ShowAllProduct();
                        break;
                    case 5:
                        ProductsMenuService.ShowAllCategoryProduct();
                        break;
                    case 6:
                        ProductsMenuService.ShowProductPricebyRange();
                        break;
                    case 7:
                        ProductsMenuService.ShowProductbyName();
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

