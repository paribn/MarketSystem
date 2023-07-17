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
                // Check if there are no sale items
                if (sale.Items == null || sale.Items.Count == 0)
                {
                    Console.WriteLine("No sale items found.");
                    return;
                }

                foreach (var saleItem in sale.Items)
                {
                    if (saleItem.product.Id < 0)
                        throw new FormatException("Product Code is lower than 0.");


                    // Find the product to add based on the product ID
                    var findProductToAdd = ProductService.GetProductsiD(saleItem.product.Id);

                    if (findProductToAdd == null)
                    {
                        Console.WriteLine("No products found.");
                        return;
                    }

                    // Check if there is insufficient quantity available
                    if (findProductToAdd.Count < saleItem.count)
                    {
                        throw new Exception("Insufficient quantity available! The quantity you are asking for is more than available in the market!");
                    }

                    // Update the product count
                    findProductToAdd.Count -= saleItem.count;
                }

                Sales.Add(sale);
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
            try
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
                    throw new FormatException("Sale not found");

                var saleItemToReturn = itemToRemove.Items.FirstOrDefault(saleItem => saleItem.SaleItemNum == itemCode);///sale itrm 
                if (saleItemToReturn == null)
                    throw new Exception("Sale item not found");

                var productToReturn = ProductService.GetProductsiD(saleItemToReturn.product.Id);
                if (productToReturn == null)
                    throw new Exception("Product iD not found");

                if (saleItemToReturn.count < count)
                {
                    throw new FormatException("The quantity to return cannot exceed the sold quantity");
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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }
        }


        public static void DeleteSales(int code)
        {
            try
            {

                var existingSales = Sales.Find(x => x.Id == code);
                if (existingSales == null)
                    throw new Exception($"Product with ID {code} not found");

                // Remove the sales with the specified code from the list
                Sales = Sales.Where(x => x.Id != code).ToList();
                if (code < 0)
                    throw new FormatException("Product ID is invalid!");
                Console.WriteLine($"Successfully deleted sales with ID: {code}");

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
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

                table.Write(Format.Minimal);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();

            }

        }


        public static void AllSalesDatebyPeriod(DateTime startDate, DateTime endDate)
        {
            try
            {
                endDate = endDate.AddDays(1).AddSeconds(-1);

                if (startDate > endDate)
                    throw new InvalidDataException("Start date can not be greater than end date!");

                // Retrieve the sales within the specified date range
                var period = Sales.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();

                if (startDate >= DateTime.Now || endDate >= DateTime.Now.AddDays(2).AddSeconds(-1))
                {
                    throw new Exception("Wrong date input");
                }

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
                table.Write(Format.Minimal);
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
                // Define the start and end date of the search period
                DateTime dateStart = date.AddSeconds(1);
                DateTime dateEnd = date.AddDays(1).AddSeconds(-1);

                Console.WriteLine($"StartDate {dateStart},EndDate {dateEnd}");

                // Retrieve the sales within the specified date range
                var searchDate = Sales.Where(x => x.Date >= dateStart && x.Date <= dateEnd).ToList();

                if (searchDate == null)
                    throw new Exception($"There is no {searchDate} product");


                var table = new ConsoleTable("Sales iD", "SalesItem", "Count", "Product Name", "Price", "DateTime");
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
                table.Write(Format.Minimal);
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

                table.Write(Format.Minimal);
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
            try
            {
                // Search for sales with the specified ID
                var searchname = Sales.Where(x => x.Id == id).ToList();
                if (searchname == null)
                    throw new Exception($"There is no {searchname} product");

                if (id < 0)
                    throw new FormatException("Sales ID is invalid!");

                var table = new ConsoleTable("Sales iD", "SalesItem", "Count", "Product Name", "Price", "DateTime");
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
                // Display the sales table with a minimal format
                table.Write(Format.Minimal);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }
        }



    }
}
