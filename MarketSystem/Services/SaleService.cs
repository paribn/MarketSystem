using ConsoleTables;
using MarketSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSystem.Services
{
    public class SaleService
    {
        public static List<Sales> Sales;


        public SaleService() 
        {
          Sales = new List<Sales>();
        }
        public static List<Sales> GetSales()
        {
            return Sales;
        }
        public static void AddSales(Sales sale)
        {
            try
            {
                if (sale.Items == null || sale.Items.Count == 0)
                {
                    Console.WriteLine("No sale items found.");
                    return;
                }

                foreach (var saleItem in sale.Items)
                {
                    if (saleItem.product.Id < 0)
                        throw new FormatException("Product Code is lower than 0.");


                    var findProductToAdd = ProductService.GetProductsiD(saleItem.product.Id);

                    if (findProductToAdd == null)
                    {
                        Console.WriteLine("No products found.");
                        return;
                    }

                    if (findProductToAdd.Count < saleItem.count)
                    {
                        Console.WriteLine("Insufficient quantity available! The quantity you are asking for is more than available in the market!");
                        return;
                    }

                    findProductToAdd.Count -= saleItem.count;
                }

                Sales.Add(sale);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops! Got an error!{ex.Message}");
            }


        }

        public static void ShowAllSales()
        {
            try
            {
                var sales = GetSales();

                var table = new ConsoleTable("Sales", "SalesItem", "Count", "Product Name", "Price", "DateTime");
                if (sales.Count == 0)
                {
                    Console.WriteLine("NO PRODUCT YET");
                    return;
                }
                foreach (var sale in sales)
                {
                    if (sale.Items != null && sale.Items.Count > 0)
                    {
                        foreach (var saleitem in sale.Items)
                        {
                            var productName = saleitem.product != null ? saleitem.product.ProductName : string.Empty;
                            table.AddRow(sale.Id, saleitem.SaleItemNum, saleitem.count, productName, sale.Amount, sale.Date);
                        }
                    }
                }
               
                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
            }
          
        }

        public static void DeleteSales(int code)
        {
            var existingProduct = Sales.Find(x => x.Id == code);
            if (existingProduct == null)
                throw new Exception($"Product with ID {code} not found");
            Sales = Sales.Where(x => x.Id != code).ToList();
        }
    }
}
