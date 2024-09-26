using System;
using System.Collections.Generic;
using admin_panel2.Data;
using admin_panel2.Helpers;
using admin_panel2.Models;
using Microsoft.EntityFrameworkCore;

namespace admin_panel
{
    internal class Program
    {
        static MarketContext context = new MarketContext();
        static List<Category> categories = new List<Category>();

        public static void LoginPage(out string email, out string password)
        {
            Console.WriteLine("Login");
            Console.Write("Email: ");
            email = Console.ReadLine();
            Console.Write("Password: ");
            password = Console.ReadLine();
        }
        public static void MenuPage()
        {
            Console.WriteLine("MENU");
            Console.WriteLine("1. Categories");
            Console.WriteLine("2. Add product");
            Console.WriteLine("3. Add category");
            Console.WriteLine("4. Report");
            Console.WriteLine("5. Exit");
        }
        public static void ProdsInStock()
        {
            using (var context = new MarketContext())
            {
                if (!context.Categories.Any())
                {
                    var categories = new List<Category>
            {
                new Category
                {
                    Name = "Dairy products",
                    Products = new List<Product>
                    {
                        new Product { Name = "Cheese", Price = 13.99, Quantity = 20, Description = "NOVA GAUDA yellow cheese" },
                        new Product { Name = "Butter", Price = 16.99, Quantity = 20, Description = "VIOLETTO 100% natural butter" },
                        new Product { Name = "Yoghurt", Price = 3.49, Quantity = 20, Description = "YAYLA Sour Village Natural Yoghurt" }
                    }
                },
                new Category
                {
                    Name = "Fruit, vegetable",
                    Products = new List<Product>
                    {
                        new Product { Name = "Apple", Price = 2.19, Quantity = 50, Description = "Red apple FUJI" },
                        new Product { Name = "Pepper", Price = 3.59, Quantity = 40, Description = "Green pepper" },
                        new Product { Name = "Cabbage", Price = 1.15, Quantity = 20, Description = "White cabbage" }
                    }
                },
                new Category
                {
                    Name = "Flour products",
                    Products = new List<Product>
                    {
                        new Product { Name = "Factory bread", Price = 0.95, Quantity = 25, Description = "Sliced white bread" },
                        new Product { Name = "Diabetic bread", Price = 2.30, Quantity = 25, Description = "IVANOVKA 100% Natural" }
                    }
                },
                new Category
                {
                    Name = "Beverage",
                    Products = new List<Product>
                    {
                        new Product { Name = "Water", Price = 0.95, Quantity = 50, Description = "SIRAB Still water" },
                        new Product { Name = "Juice", Price = 2.20, Quantity = 50, Description = "NATURA MULTIVITAMIN no sugar mixed fruits" },
                        new Product { Name = "Lemonade", Price = 2.05, Quantity = 50, Description = "NATAKHTARI Pear lemonade" }
                    }
                }
            };

                    context.Categories.AddRange(categories);
                    context.SaveChanges();
                }
            }
        }

        public static void ShowCategories()
        {
            using (var context = new MarketContext())
            {
                var categories = context.Categories.ToList();
                Console.WriteLine("Categories");
                for (int i = 0; i < categories.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {categories[i].Name}");
                }
                
            }
        }
        public static void AddProduct(string categoryChoice)
        {
            if (!int.TryParse(categoryChoice, out int categoryIndex))
            {
                Console.WriteLine("Invalid choice");
                return;
            }

            using (var context = new MarketContext())
            {
                var categories = context.Categories.ToList();
                if (categoryIndex < 1 || categoryIndex > categories.Count)
                {
                    Console.WriteLine("Invalid choice");
                    return;
                }

                var category = categories[categoryIndex - 1];
                Console.Write("Product name: ");
                string productName = Console.ReadLine();
                Console.Write("Product price: ");
                double productPrice;
                while (!double.TryParse(Console.ReadLine(), out productPrice))
                {
                    Console.WriteLine("Invalid price");
                    Console.Write("Product price: ");
                }
                Console.Write("Product quantity: ");
                int productQuantity;
                while (!int.TryParse(Console.ReadLine(), out productQuantity))
                {
                    Console.WriteLine("Invalid quantity");
                    Console.Write("Product quantity: ");
                }
                Console.Write("Product description: ");
                string productDesc = Console.ReadLine();

                var product = new Product
                {
                    Name = productName,
                    Price = productPrice,
                    Quantity = productQuantity,
                    Description = productDesc,
                    CategoryId = category.Id
                };

                context.Products.Add(product);
                context.SaveChanges();
                Console.WriteLine("Product added");
                Console.WriteLine("Press any key to go back..");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public static void AddCategory()
        {
            Console.Write("New category name: ");
            string categoryName = Console.ReadLine();

            using (var context = new MarketContext())
            {
                if (context.Categories.Any(c => c.Name.ToLower() == categoryName.ToLower()))
                {
                    Console.WriteLine("Category already exist");
                    return;
                }

                var category = new Category { Name = categoryName };
                context.Categories.Add(category);
                context.SaveChanges();
                Console.WriteLine("Category added");
            }
        }

        public static void ShowReport()
        {
            using (var context = new MarketContext())
            {
                var report = context.Boughts.AsEnumerable().GroupBy(b => b.Date.Date)
                    .Select(result => new
                    {
                        Date = result.Key.ToString("dd.MM.yyyy"),  
                        TotalAmount = result.Sum(sum => sum.TotalAmount)
                    }).ToList();

                if (report.Count == 0)
                {
                    Console.WriteLine("No report");
                }
                else
                {
                    foreach (var details in report)
                    {
                        Console.WriteLine($"Date: {details.Date}, Amount: {details.TotalAmount} AZN");
                    }
                }
            }

            Console.WriteLine("Press any key to return ..");
            Console.ReadKey();
        }


        static void Main(string[] args)
        {
            AdminManager adminManager = new AdminManager();

            ProdsInStock();
            

            string email, password;
            LoginPage(out email, out password);
            bool login = adminManager.Login(email.ToLower().Trim(), password);
            Console.WriteLine(login ? "Login successful!" : "Login failed");
            Console.Clear();
            if (login)
            {
                while (true)
                {
                    MenuPage();
                    Console.Write("Make choice: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            ShowCategories();
                            Console.WriteLine("Press any key to go back..");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "2":
                            Console.Clear();
                            ShowCategories();
                            Console.Write("Choose category to add product: ");
                            var addCategoryChoice = Console.ReadLine();
                            AddProduct(addCategoryChoice);
                            break;
                        case "3":
                            Console.Clear();
                            AddCategory();
                            break;
                        case "4":
                            Console.Clear();
                            ShowReport();
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
            }
        }
    }
}
