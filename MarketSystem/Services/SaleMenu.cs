using MarketSystem.Common.Abstract;
using MarketSystem.Common.Enum;
using MarketSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketSystem.Services
{
    public class SaleMenu : ISalesMenu
    {
        private static SaleService saleService = new();
        private  List<Sales> sales;

        public static void AddNewSale()
        {
            try
            {
                SalesItems.ResetSaleItems();

                List<SalesItems> saleItems = new List<SalesItems>();
                bool addItems = true;

                while (addItems)
                {
                    Console.WriteLine("Enter product code:");
                    int productCode;

                    while (!int.TryParse(Console.ReadLine(), out productCode))
                    {
                        Console.WriteLine("Invalid product code! Please enter a valid integer:");
                    }

                    Console.WriteLine("Enter the quantity:");
                    int quantity;

                    while (!int.TryParse(Console.ReadLine(), out quantity))
                    {
                        Console.WriteLine("Invalid quantity! Please enter a valid integer:");
                    }

                    var product = ProductService.GetProductsiD(productCode);

                    var salesItem = new SalesItems
                    {
                        count = quantity,
                        product = product
                    };
                    saleItems.Add(salesItem);

                    Console.WriteLine("Do you want to add more items? (yes/no)");
                    string choice = Console.ReadLine();

                    if (choice.ToLower() != "yes")
                    {
                        addItems = false;
                    }

                }

                var Sale = new Sales
                {
                    Date = DateTime.Now,
                    Amount = saleItems.Sum(item => item.product.ProductPrice * item.count),
                    Items = saleItems
                };

                SaleService.AddSales(Sale);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message);
            }
        }


        public static void DeleteSale()
        {
            try
            {
                Console.WriteLine("Enter product ID :");
                int productId = int.Parse(Console.ReadLine());
                SaleService.DeleteSales(productId);
                Console.WriteLine($"Successfully deleted product with ID: {productId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message);
            }
        }

        public static void ReturnSale()
        {
            throw new NotImplementedException();
        }

        public static void SearchSalesNumber()
        {
            try
            {
                Console.WriteLine("Enter sales number");
                Console.WriteLine("<><><><><><><><><<>");
                int num = int.Parse(Console.ReadLine());
                SaleService.SearchSaleNumber(num);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message);
            }

        }


        public static void ShowAllSales()
        {
            SaleService.ShowAllSales();
        }

        public static void ShowAllSalesDatebyPeriod()
        {
            try
            {
                Console.WriteLine("Enter start date (mm/dd/yyyy): ");
                DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "mm/dd/yyyy", CultureInfo.InvariantCulture);

                Console.WriteLine("Enter end date (mm/dd/yyyy): ");
                DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "mm/dd/yyyy", CultureInfo.InvariantCulture);

               // SaleService.MyTable(SaleService.AllSalesDatebyPeriod(startDate, endDate));

                 // SaleService.MyTable(salelist, salesItemsList);
                  SaleService.AllSalesDatebyPeriod(startDate, endDate);

                // SaleService.MyTable(SaleService.AllSalesDatebyPeriod(startDate, endDate));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops! Got an error! { ex.Message } ");
            }

        }

        public static void ShowSalesbyPriceRange()  // bu ishliyir
        {
            Console.WriteLine("Enter min SalePrice");
            decimal priceSales = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Eter max SalePrice ");
            decimal priceSaless = decimal.Parse(Console.ReadLine());
            SaleService.DisplaySalesByPriceRange(priceSales, priceSaless);
        }

        public static void ShowSalesDate()
        {
            Console.WriteLine("enter Date (mm/dd/yyyy");
            DateTime times = DateTime.ParseExact(Console.ReadLine(), "mm/dd/yyyy", CultureInfo.InvariantCulture);
            SaleService.ShowSalesDate(times);
        }
    }
}
