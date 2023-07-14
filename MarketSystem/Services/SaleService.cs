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
          //  ProductService.ShowAnyKindOfProductlistInTable(Sales.Add;

        }

        public static void ShowAllSales()
        {
            try
            {
                var sales1 = GetSales();
                var table = new ConsoleTable("ID", "SalesItem", "Price", "DateTime");
                if (sales1.Count == default)
                {
                    Console.WriteLine("NO PRODUCT YET");
                    return;
                }
                foreach (var sales in sales1)
                {
                    table.AddRow(sales.Id,sales.Items,sales.Amount,sales.Date);
                }
                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
            }
            SaleMenu.AddNewSale();
        }
    }
}
