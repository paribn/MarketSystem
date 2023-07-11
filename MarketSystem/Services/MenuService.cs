using ConsoleTables;
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
        private static ProductService productService= new();

        #region Product

        public static void MenuProduct()
        {
            try
            {
             var products = productService.GetProducts();
                var table = new ConsoleTable("Id", "Name", "Price", "Code", "Category");
                if (products.Count== 0)
                {
                    Console.WriteLine("NO PRODUCT YET");
                    return;
                }
                foreach ( var product in products )
                {
                    table.AddRow(product.Id, product.ProductName, product.ProductPrice,
                         product.Category);
                }
                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        public static void ProductAdd()
        {
            try
            {
                // Console.WriteLine("Choose product category :");

                //ProductCategory[] productCategories = (ProductCategory[])Enum.GetValues(typeof(ProductCategory));
                //foreach (ProductCategory productCategory in productCategories) { Console.WriteLine(productCategory); /*}*/
                Console.WriteLine("Enter your category");
                string category = Console.ReadLine().Trim();

                
                bool IsSuccessful = Enum.TryParse(typeof(ProductCategory), category, true,
                out object parsedCategory);
                if (!IsSuccessful)
                {
                    throw new InvalidDataException("not found");
                }

                Console.WriteLine("----------------");
               
                Console.WriteLine("Enter Product name:");
                string name = Console.ReadLine();
                Console.WriteLine("----------------");

                Console.WriteLine("Enter product price");
                decimal price = Decimal.Parse(Console.ReadLine());
                Console.WriteLine("----------------");


                Console.WriteLine("Enter product count");
                int count = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("----------------");

                ProductService.AddProduct(name, price, parsedCategory, count);
                
           
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error444!"); // runtime erroor
                Console.WriteLine(ex.Message);
            }
        }

       
        public static void ProductRemove()
        {
            try
            {
                Console.WriteLine("Enter product ID :");
                int productId = int.Parse(Console.ReadLine());
                productService.DeleteProduct(productId);
                Console.WriteLine($"Succesfully deleted product with ID: {productId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
            }
        }
        public static void ShowAllProductCategory()
        {
            Console.WriteLine("Enter category");
            string category = Console.ReadLine();    
            productService.ShowAllProductCategory(category);
        }
    }
}
