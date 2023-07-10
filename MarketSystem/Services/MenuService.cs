using MarketSystem.Common.Enum;
using MarketSystem.SubMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSystem.Services
{
    public class MenuService
    {
        private static MarketService marketService= new();

        public static void ProductAdd()
        {
            try
            {
                Console.WriteLine("Choose product category :");
                ProductCategory[] productCategories = (ProductCategory[])Enum.GetValues(typeof(ProductCategory));
                foreach (ProductCategory productCategory in productCategories) { Console.WriteLine(productCategory); }
                Console.WriteLine("----------------");
                string category = Console.ReadLine();
                Console.WriteLine("Enter Product name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter product price");
                decimal price = Decimal.Parse(Console.ReadLine());
                Console.WriteLine("Enter product count");
                int count = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter product code");
                string code = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
            }
        }
        public static void ProductRemove()
        {
            try
            {
                Console.WriteLine("Enter product ID :");
                int productId = int.Parse(Console.ReadLine());
                //
                Console.WriteLine($"Succesfully deleted product with ID: {productId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
            }
        }
        public static void MakeProduct()
        {

        }
    }
}
