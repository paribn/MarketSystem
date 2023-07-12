using ConsoleTables;
using MarketSystem.Common.Models;
using MarketSystem.Services;
using MarketSystem.SubMenu;

namespace MarketSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductService productService = new ProductService();

            Console.ForegroundColor = ConsoleColor.Green;  // metod yaz 
            Console.WriteLine("Enter");
            Console.ResetColor();
                

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
                Console.WriteLine("1.Operate on products");
                Console.WriteLine("2.Operate on Sales");    
                Console.WriteLine("0. Exit");
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
                        SubMenuProduct.ProductMenu();
                        break;
                    case 2:
                        break;
                    case 0:
                        Console.WriteLine("Bye!");
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }

            } while (option != 0);
        }
    }
}