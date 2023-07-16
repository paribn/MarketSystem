using ConsoleTables;
using MarketSystem.Common.Enum;
using MarketSystem.Common.Interface;
using MarketSystem.Common.Models;
using MarketSystem.SubMenu;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketSystem.Services
{
    public class ProductService
    {
        public static List<Products> Products;

        public ProductService()
        {
            Products = new List<Products>();
        }
        public static List<Products> GetProducts()
        {
            return Products;
        }

        /// this method takes me to the SaleService and returns the iD of the product
        public static Products GetProductsiD(int code)
        {
            return Products.FirstOrDefault(x => x.Id == code);
        }


        public static int AddProduct(string name, decimal price,
            string productCategory, int count)
        {
            // Validate the name
            if (string.IsNullOrWhiteSpace(name))
                throw new FormatException("Name is empty!");

            if (price <= 0)
                throw new FormatException("Price is lower than 0!");

            // Check if the name contains only letters
            bool isString = false;
            for (int i = 0; i < name.Length; i++)
            {
                if (char.IsLetter(name[i]))
                {
                    isString = true;
                }
            }
            if (!isString)
            {
                throw new Exception("Name must be string");
            }
            if (count <= 0)
                throw new FormatException("Invalid count!");

            // Parse the product category
            bool isSuccessful
                = Enum.TryParse(typeof(ProductCategory), productCategory, true, out object parsedDepartment);
            if (!isSuccessful)
            {
                throw new InvalidDataException("Category not found!");
            }

            // Create a new product with the provided details
            var newProduct = new Products
            {
                ProductName = name,
                ProductPrice = price,
                Count = count,
                Category = (ProductCategory)parsedDepartment
            };

            // Add the new product to the list of products
            Products.Add(newProduct);

            return newProduct.Id;
        }


        public static void DeleteProduct(int productID)
        {
            try
            {
                // Find the existing product with the specified ID
                var existingProduct = Products.FirstOrDefault(x => x.Id == productID);

                if (existingProduct == null)
                    throw new Exception($"Product with ID {productID} not found");

                // Remove the product from the list of products
                Products = Products.Where(x => x.Id != productID).ToList();
                if (productID < 0)
                    throw new FormatException("Product ID is invalid!");

                Console.WriteLine($"Successfully deleted product with ID: {productID}");
                ProductsMenu.ShowAllProduct();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }

        }


        public static void ShowAllProducts()
        {
            try
            {
                // Retrieve the products
                var products = GetProducts();

                var table = new ConsoleTable("ID", "Name", "Product Count", "Price", "Category");

                // If there are no products, display a message and return
                if (products.Count == default)
                {
                    Console.WriteLine("NO PRODUCT YEET");
                    return;
                }

                // Add each product to the console table
                foreach (var product in products)
                {
                    table.AddRow(product.Id, product.ProductName, product.Count, product.ProductPrice,
                         product.Category);
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


        public static List<Products> SearchByName(string productname)
        {
            try
            {
                // Perform case-insensitive search for products matching the provided name
                var searchname = Products.Where(x => x.ProductName.ToLower().Trim() == productname.ToLower().Trim()).ToList();
                if (string.IsNullOrWhiteSpace(productname))
                {
                    throw new Exception("Product name cannot be empty or whitespace.");
                }

                // Check if the product name contains only letters
                bool isString = false;
                for (int i = 0; i < productname.Length; i++)
                {
                    if (char.IsLetter(productname[i]))
                    {
                        isString = true;
                    }
                }
                if (!isString)
                {
                    throw new Exception("Name must be string");
                }

                // If no products match the search name, display a message
                if (searchname.Count == 0)
                {
                    Console.WriteLine($"There are no products with the name '{productname}'.");
                }

                else
                {
                    // Display the matching products in a console table
                    var table = new ConsoleTable("ID", "Name", "Count", "Price", "Category");
                    foreach (var product in searchname)
                    {
                        table.AddRow(product.Id, product.ProductName, product.Count, product.ProductPrice,
                             product.Category);
                    }
                    table.Write(Format.Minimal);
                }

                return searchname;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching for products by name.{ex.Message} ");
            }
        }


        public static void UpdateProduct(int Id, string name, int count, object category, decimal price)
        {
            try
            {
                // Find the product to update
                var Update = Products.FirstOrDefault(x => x.Id == Id);

                if (Update == null)
                    throw new Exception($"{Id} is invalid");

                bool isString = false;
                for (int i = 0; i < name.Length; i++)
                {
                    if (char.IsLetter(name[i]))
                    {
                        isString = true;
                    }
                }
                if (!isString)
                {
                    throw new Exception("Name must be string");
                }

                if (price < 0)
                    throw new FormatException("Price is lower than 0!");

                if (count < 0)
                    throw new FormatException("Invalid count!");

                Update.ProductName = name;
                Update.ProductPrice = price;
                Update.Count = count;
                Update.Category = (ProductCategory)category;
                ShowAllProducts();

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while processing.Error message : {ex.Message} ");
                Console.ResetColor();
            }

        }


        public static void ShowPriceRange(decimal minPrice, decimal maxPrice)
        {
            try
            {
                // Validate the price range values
                if (maxPrice < 0 || minPrice < 0)
                { throw new FormatException("The value cannot be negative"); }

                // Filter products within the given price range
                var range = Products.FindAll(x => x.ProductPrice >= minPrice && x.ProductPrice <= maxPrice);


                var table = new ConsoleTable("ID", "Name", "Count", "Price", "Category");

                // Check if the minimum price is greater than the maximum price
                if (minPrice > maxPrice)
                    throw new FormatException("Minimum is greater than maximum");

                // Check if there are no products within the price range
                if (range.Count == 0)
                {
                    throw new FormatException("No product yet");
                }

                foreach (var product in range)
                {
                    table.AddRow(product.Id, product.ProductName, product.Count, product.ProductPrice,
                         product.Category);
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


        public static void ShowAllCategory(object productCategory)
        {
            try
            {
                // Display all available product categories
                foreach (var item in Enum.GetNames(typeof(ProductCategory)))
                {
                    Console.WriteLine(item);
                }

                // Validate the provided product category
                if (!Enum.IsDefined(typeof(ProductCategory), productCategory))
                {
                    Console.WriteLine("Invalid category number!");

                    return;
                }

                // Convert the product category to the appropriate enum value
                ProductCategory selectedCategory = (ProductCategory)productCategory;

                var productsInCategory = Products.Where(x => x.Category == selectedCategory);

                // Check if there are no products in the selected category
                if (!productsInCategory.Any())
                {
                    Console.WriteLine("No products found for this category.");
                    return;
                }

                Console.WriteLine($"Showing products in the {selectedCategory} category:");
                Console.WriteLine("><><><><><><<><><>>>><><><><><><><><><><><");

                var table = new ConsoleTable("ID", "Name", "Count", "Price", "Category");
                foreach (var product in productsInCategory)
                {
                    table.AddRow(product.Id, product.ProductName, product.Count, product.ProductPrice,
                         product.Category);
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


    }
}
