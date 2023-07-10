using MarketSystem.SubMenu;

namespace MarketSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
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