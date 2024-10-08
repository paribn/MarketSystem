﻿using MarketSystem.Common.Abstract;
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
        private List<Sales> sales;

        public static void AddNewSale()
        {
            try
            {
                ProductService.ShowAllProducts();
                SalesItems.ResetSaleItems();

                List<SalesItems> saleItems = new List<SalesItems>();
                bool addItems = true;

                while (addItems)
                {
                    Console.WriteLine("Enter product code:");
                    int productCode;
                    Console.WriteLine("-----------------");
                    while (!int.TryParse(Console.ReadLine(), out productCode))
                    {
                        Console.WriteLine("Invalid product code! Please enter a valid integer:");

                    }

                    var existingProduct = ProductService.Products.FirstOrDefault(x => x.Id == productCode);

                    if (existingProduct == null)
                    {
                        throw new Exception($"Product with Id: {productCode}");
                    }

                    Console.WriteLine("Enter the product count:");
                    int quantity;
                    Console.WriteLine("-----------------");

                    while (!int.TryParse(Console.ReadLine(), out quantity))
                    {
                        Console.WriteLine("Invalid quantity! Please enter a valid integer:");
                    }
                    if (quantity <= 0)
                    {
                        throw new FormatException("count cannot be less than zero");

                    }

                    var product = ProductService.GetProductsiD(productCode);


                    var salesItem = new SalesItems
                    {
                        count = quantity,
                        product = product
                    };
                    saleItems.Add(salesItem);

                    Console.WriteLine("Do you want to add more items? (Y-yes/N-no)");
                    Console.WriteLine("-----------------");

                Start:
                    var input = Console.ReadKey();

                    switch (input.Key) //Switch on Key enum
                    {
                        case ConsoleKey.Y:
                            Console.WriteLine("\n");
                            addItems = true;
                            ProductService.ShowAllProducts();
                            break;
                        case ConsoleKey.N:
                            Console.WriteLine("\n");
                            addItems = false;
                            break;
                        default:
                            goto Start;
                            break;
                    }

                    var Sale = new Sales
                    {
                        Date = DateTime.Now,
                        Amount = saleItems.Sum(item => item.product.ProductPrice * item.count),
                        Items = saleItems
                    };

                    SaleService.AddSales(Sale);
                    SaleService.ShowAllSales();
                }

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }
        }


        public static void ReturnSale()
        {
            try
            {
                SaleService.ShowAllSales();
                Console.WriteLine("Enter Sale iD: ");
                int saleCode;
                while (!int.TryParse(Console.ReadLine(), out saleCode))
                {
                    Console.WriteLine("Invalid Sale code! Please enter a valid integer:");
                }

                Console.WriteLine("Enter Sale Item iD: ");
                int saleItemCode;
                while (!int.TryParse(Console.ReadLine(), out saleItemCode))
                {
                    Console.WriteLine("Invalid Sale item number! Please enter a valid integer:");
                }

                Console.WriteLine("Enter the count:");
                int productQuantityForReturn;

                while (!int.TryParse(Console.ReadLine(), out productQuantityForReturn))
                {
                    Console.WriteLine("Invalid quantity! Please enter a valid integer:");
                }

                SaleService.ReturnSales(saleCode, saleItemCode, productQuantityForReturn);
                SaleService.ShowAllSales();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }

        }

        public static void DeleteSale()
        {
            try
            {
                SaleService.ShowAllSales();
                Console.WriteLine("Enter sales iD :");
                Console.WriteLine("<><><><><><><><><<>");
                int salesiD = int.Parse(Console.ReadLine());
                SaleService.DeleteSales(salesiD);

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
            SaleService.ShowAllSales();
        }


        public static void ShowAllSalesDatebyPeriod()
        {
            try
            {
                SaleService.ShowAllSales();
                Console.WriteLine("Enter start date this format -> MM/dd/yyyy HH:mm:ss");
                Console.WriteLine("<><><><><><><><><<>");
                DateTime startDate = DateTime.ParseExact(Console.ReadLine().Trim(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                Console.WriteLine("Enter end date this format -> dd/MM/yyyy HH:mm:ss");
                Console.WriteLine("<><><><><><><><><<>");
                DateTime endDate = DateTime.ParseExact(Console.ReadLine().Trim(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);


                SaleService.AllSalesDatebyPeriod(startDate, endDate);

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }

        }


        public static void ShowSalesbyPriceRange()  
        {
            try
            {

                Console.WriteLine("Enter min SalePrice");
                Console.WriteLine("<><><><><><><><><<>");
                decimal priceSales = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Eter max SalePrice ");
                Console.WriteLine("<><><><><><><><><<>");
                decimal priceSaless = decimal.Parse(Console.ReadLine());
                SaleService.DisplaySalesByPriceRange(priceSales, priceSaless);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }
        }


        public static void ShowSalesDate()
        {
            try
            {
                SaleService.ShowAllSales();
                Console.WriteLine("Enter Date  this format -> MM/dd/yyyy HH:mm:ss");
                DateTime times = DateTime.ParseExact(Console.ReadLine().Trim(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                SaleService.ShowSalesDate(times);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }
        }


        public static void SearchSalesNumber()
        {
            try
            {
                SaleService.ShowAllSales();
                Console.WriteLine("Enter sales iD");
                Console.WriteLine("<><><><><><><><><<>");
                int num = int.Parse(Console.ReadLine());
                SaleService.SearchSaleNumber(num);
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
