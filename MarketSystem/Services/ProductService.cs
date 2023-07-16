using ConsoleTables;
using MarketSystem.Common.Enum;
using MarketSystem.Common.Interface;
using MarketSystem.Common.Models;
using MarketSystem.SubMenu;
using System;
using System.Collections.Generic;
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

        //// this method takes me to the SaleService and returns the id of the product
        public static Products GetProductsiD(int code)    
        {
            return Products.FirstOrDefault(x => x.Id == code);
        }  


        /// <summary>
        /// adds a new product
        /// </summary>
        public static int AddProduct(string name, decimal price,
            string productCategory, int count)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new FormatException("Name is empty!");

            if (price < 0)
                throw new FormatException("Price is lower than 0!");
            bool isString = false;
            for (int i = 0;  i<name.Length;i ++)
            {
                if (char.IsLetter(name[i]))
                {
                    isString = true;
                }
            }
            if (!isString)
            {
                throw new Exception( "Name must be string");
            }

            if (count < 0)
                throw new FormatException("Invalid count!");

            bool isSuccessful
                = Enum.TryParse(typeof(ProductCategory), productCategory, true, out object parsedDepartment);
            if (!isSuccessful)
            {
                throw new InvalidDataException("Category not found!");
            }

            var newProduct = new Products
            {
                ProductName = name,
                ProductPrice = price,
                Count = count,
                Category = (ProductCategory)parsedDepartment
            };

            Products.Add(newProduct);

            return newProduct.Id;
        }



        /// <summary>
        /// deletes by product Id
        /// </summary>
        public static void DeleteProduct(int productID)  
        {
            var existingProduct = Products.FirstOrDefault(x => x.Id == productID);
            if (existingProduct == null)
                throw new Exception($"Product with ID {productID} not found");
            Products = Products.Where(x => x.Id != productID).ToList();
        }


        /// <summary>
        /// This method returns the list of all products
        /// </summary>
        public static void ShowAllProducts()
        {
            try
            {
                var products = GetProducts();
                var table = new ConsoleTable("ID", "Name", "Count", "Price", "Category");
                if (products.Count == default)
                {
                    Console.WriteLine("NO PRODUCT YEET");
                    return;
                }
                foreach (var product in products)
                {
                    table.AddRow(product.Id, product.ProductName, product.Count, product.ProductPrice,
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

        public static void ListEnum() 
        {
            foreach (var item in Enum.GetNames(typeof(ProductCategory)))
            {
                Console.WriteLine(item);
            }
        }


        /// <summary>
        /// Searches by the name of the entered product
        /// </summary>
        public static List<Products> SearchByName(string productname)
        {
            var searchname = Products.Where(x => x.ProductName.ToLower().Trim() == productname.ToLower().Trim()).ToList();
            if (searchname == null)
                throw new Exception($"There is no {searchname} product");

            return searchname;
        }


        /// <summary>
        /// this table method
        /// </summary>
        public static void ShowAnyKindOfProductlistInTable(List<Products> productsList)
        {
            try
            {
                var products = productsList;
                var table = new ConsoleTable("ID", "Name", "Count", "Price", "Category");
                if (products.Count == 0)
                {
                    Console.WriteLine("NO PRODUCT YEET");
                    return;
                }
                foreach (var product in products)
                {
                    table.AddRow(product.Id, product.ProductName, product.Count, product.ProductPrice,
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


        /// <summary>
        /// this method finds and adjusts the product by its iD
        /// </summary>
        public static void UpdateProduct(int Id, string name, int count, object category, decimal price)
        {
            // Find the product to update
            var Update = Products.FirstOrDefault(x => x.Id == Id);
            if (Update == null)
                throw new Exception($"{Id} is invalid");
            if (price < 0)
                throw new FormatException("Price is lower than 0!");
            if (count < 0)
                throw new FormatException("Invalid count!");
            Update.ProductName = name;
            Update.ProductPrice = price;
            Update.Count = count;

        }

       
        /// <summary>
        /// Finds the price range of the given products
        /// </summary>
        public static void ShowPriceRange(decimal minPrice, decimal maxPrice)
        {
            // Filter sales within the given price range
            var range = Products.FindAll(x => x.ProductPrice >= minPrice && x.ProductPrice <= maxPrice);

            var products = GetProducts();
            var table = new ConsoleTable("ID", "Name", "Count", "Price", "Category");
            if (range.Count == 0)
            {
                Console.WriteLine("NO PRODUCT YET");
                return;
            }
            foreach (var product in range)
            {
                table.AddRow(product.Id, product.ProductName, product.Count, product.ProductPrice,
                     product.Category);
            }
            table.Write();
           // return;
        }


        /// <summary>
        /// Shows the whole category
        /// </summary>
        public static void ShowAllCategory(object productCategory)
        {
            foreach (var item in Enum.GetNames(typeof(ProductCategory)))
            {
                Console.WriteLine(item);
            }

            if (!Enum.IsDefined(typeof(ProductCategory), productCategory))
            {
                Console.WriteLine("Invalid category number!");

                return;
            }

            ProductCategory selectedCategory = (ProductCategory)productCategory;

            Console.WriteLine($"Showing products in the {selectedCategory} category:");

            var productsInCategory = Products.Where(x => x.Category == selectedCategory);

            foreach (var product in productsInCategory)
            {
                Console.WriteLine($"Name: {product.ProductName}");
                Console.WriteLine($"Quantity: {product.Count}");
                Console.WriteLine($"Price: {product.ProductPrice}");
            }

        }
    }
}
