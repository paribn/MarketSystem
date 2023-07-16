using ConsoleTables;
using MarketSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

                var table = new ConsoleTable("SalesiD", "SalesItem", "Count", "Product Name", " Total Price", "DateTime");
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
                Console.WriteLine($"Oops! Got an error!{ex.Message}");

            }

        }

        public static void DeleteSales(int code)  // isliyr
        {
            var existingSales = Sales.Find(x => x.Id == code);
            if (existingSales == null)
                throw new Exception($"Product with ID {code} not found");
            Sales = Sales.Where(x => x.Id != code).ToList();
            //if ()
        }

        public static void AllSalesDatebyPeriod(DateTime startDate, DateTime endDate)
        {
            try
            {
                endDate = endDate.AddDays(1).AddSeconds(-1);
                if (startDate > endDate)
                    throw new InvalidDataException("Start date can not be greater than end date!");
                var period = Sales.FindAll(x => x.Date >= startDate && x.Date <= endDate).ToList();


                //if (endDate.Date > DateTime.Now.AddDays(1).Date);
                //    throw new Exception("End date cannot be greater than today's day!");

                var table = new ConsoleTable("SalesCode", "SalesItem", "Count", "Product Name", "Price", "DateTime");
                if (period.Count > 0)
                {
                    foreach (var sale in period)
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
                    //Console.WriteLine("NO PRODUCT YET");
                    //return;
                }
                foreach (var sale in period)
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
                Console.WriteLine($"Oops! Got an error!{ex.Message}");
            }

        }

        public static void DisplaySalesByPriceRange(decimal minSalesPrice, decimal maxSalesPrice)
        {
            try
            {
                // Filter sales within the given price range
                var range = Sales.Where(x => x.Amount >= minSalesPrice && x.Amount <= maxSalesPrice).ToList();

                //var sales = GetSales();
                if (minSalesPrice > maxSalesPrice)
                    throw new ArgumentOutOfRangeException("Max price cannot be less than min price!");

                var table = new ConsoleTable("Sales", "SalesItem", "Count", "Product Name", "Total Price", "DateTime");
                if (range.Count == 0)
                {
                    Console.WriteLine("NO PRODUCT YET");
                    return;
                }
                foreach (var sale in range)
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
                Console.WriteLine($"Oops! Got an error!{ex.Message}");
            }

        }

        public static void ShowSalesDate(DateTime date)
        {
            var searchDate = Sales.Where(x => x.Date == date).ToList();
            if (searchDate == null)
                throw new Exception($"There is no {searchDate} product");
            var table = new ConsoleTable("SalesCode", "SalesItem", "Count", "Product Name", "Price", "DateTime");
            if (searchDate.Count > 0)
            {

                Console.WriteLine("NO PRODUCT YET");
                return;
            }
            foreach (var sale in searchDate)
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
            //if (date <= DateTime.Now)
            //    throw new InvalidDataException("");
        }

        public static void SearchSaleNumber(int id)
        {
            var searchname = Sales.Where(x => x.Id == id).ToList();
            if (searchname == null)
                throw new Exception($"There is no {searchname} product");

            if (id < 0)
                throw new FormatException("Sales ID is invalid!");
           

        }

        public static void MyTable(List<Sales> SalesList, List<SalesItems> salesItemList)
        {

            try
            {
                var salestable = SalesList;
                var salesitemtable = salesItemList;
                var table = new ConsoleTable("Sales", "SalesItem", "Count", "Product Name", "Price", "DateTime");
                if (salestable.Count == 0 && salesitemtable.Count == 0)
                {
                    Console.WriteLine("No produtc yet");
                    return;
                }
                foreach (var saless in salestable)
                {
                    foreach (var item in salesitemtable)
                    {
                        table.AddRow(saless.Id, item.SaleItemNum, item.count,
                            item.product.ProductName, saless.Amount, saless.Date);
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


    }
}
