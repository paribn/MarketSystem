using ConsoleTables;
using MarketSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
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
                        throw new Exception("Insufficient quantity available! The quantity you are asking for is more than available in the market!");
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();

            }

        }

        public static void DeleteSales(int code)  // isliyr
        {
            var existingSales = Sales.Find(x => x.Id == code);
            if (existingSales == null)
                throw new Exception($"Product with ID {code} not found");
            Sales = Sales.Where(x => x.Id != code).ToList();


        }

        public static void AllSalesDatebyPeriod(DateTime startDate, DateTime endDate)
        {
            try
            {
                endDate = endDate.AddDays(1).AddSeconds(-1);

                if (startDate > endDate)
                    throw new InvalidDataException("Start date can not be greater than end date!");
                var period = Sales.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();

                if (startDate >= DateTime.Now || endDate >= DateTime.Now.AddDays(2).AddSeconds(-1))
                {
                    throw new Exception("Wrong date input");
                }
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
                }
                table.Write();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }

        }

        public static void DisplaySalesByPriceRange(decimal minSalesPrice, decimal maxSalesPrice)
        {
            try
            {
                // Filter sales within the given price range
                var range = Sales.Where(x => x.Amount >= minSalesPrice && x.Amount <= maxSalesPrice).ToList();

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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }

        }

        public static void ShowSalesDate(DateTime date)
        {
            try
            {
                DateTime dateStart = date.AddSeconds(1);
                DateTime dateEnd = date.AddDays(1).AddSeconds(-1);

                Console.WriteLine($"StartDate {dateStart},EndDate {dateEnd}");

                var searchDate = Sales.Where(x => x.Date >= dateStart && x.Date <= dateEnd).ToList();

                if (searchDate == null)
                    throw new Exception($"There is no {searchDate} product");

                
                var table = new ConsoleTable("SalesCode", "SalesItem", "Count", "Product Name", "Price", "DateTime");
                if (searchDate.Count > 0)
                {
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
               }
                table.Write();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }
        }

        public static void SearchSaleNumber(int id)
        {
            var searchname = Sales.Where(x => x.Id == id).ToList();
            if (searchname == null)
                throw new Exception($"There is no {searchname} product");

            if (id < 0)
                throw new FormatException("Sales ID is invalid!");
            var table = new ConsoleTable("SalesCode", "SalesItem", "Count", "Product Name", "Price", "DateTime");
            if (searchname.Count > 0)
            {
                foreach (var sale in searchname)
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
            }
            table.Write();

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

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }
        }

        public static void ReturnSales(int salesNum, int itemCode, int count)
        {
            if (itemCode < 0) 
            {
                throw new FormatException("Sale item Number is lower than 0.");
            }

            if (salesNum < 0) 
            {
                throw new FormatException("Sale Number is lower than 0.");
            }
          
            if (count <= 0)
                throw new FormatException("Product quantity is lower than 0 / or equals to zero.");

            var itemToRemove = Sales.SingleOrDefault(saleCode => saleCode.Id == salesNum);
            if (itemToRemove == null)
                throw new FormatException("Sale not found1 .");

            var saleItemToReturn = itemToRemove.Items.FirstOrDefault(saleItem => saleItem.SaleItemNum == itemCode);///sale itrm 
            if (saleItemToReturn == null)
                throw new Exception("Sale item not found2 .");

            var productToReturn = ProductService.GetProductsiD(saleItemToReturn.product.Id);
            if (productToReturn == null)
                throw new Exception("Product ginot found 3");

            if (saleItemToReturn.count < count)
            {
                throw new FormatException("Quantity to return exceeds the sold quantity.");
            }
            // Calculate the amount for the product being returned
            decimal amountForReturnedProduct = productToReturn.ProductPrice * count;

            // Update the Sales with the new total Amount (before returning)

            itemToRemove.Amount -= amountForReturnedProduct;

            if (saleItemToReturn.count == count)
            {
                // Remove the sale item from the list of sale items
                itemToRemove.Items.Remove(saleItemToReturn);
            }
            // Update the quantities
            saleItemToReturn.count -= count;
            productToReturn.Count += count;

            // Recalculate the new total Amount 
            decimal newTotalAmount = itemToRemove.Items.Sum(item => item.product.ProductPrice * item.count);

            // Update the Sales
            itemToRemove.Amount = newTotalAmount;
        }


    }
}
