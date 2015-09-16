using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI.WebControls.Expressions;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductsShop.Data;
using ProductsShop.Models;
using Formatting = System.Xml.Formatting;


namespace ProductsShop.ConsoleClient
{
    internal class SeedDatabase_Queries
    {
        private static void Main()
        {
            //SeedUsers();
            //SeedCategories();
            //SeedProducts();
            // SeedProductCategories();
            //ProductsInRange();
            //SuccessfullySoldProducts();
            // CategoriesByProductsCount();
            //UsersAndProducts();
        }


        private static void SeedUsers()
        {
            var context = new ShopEntities();
            var path = "..\\..\\..\\users.xml";
            var doc = XDocument.Load(path);
            string json = JsonConvert.SerializeXNode(doc, Newtonsoft.Json.Formatting.Indented);
            var jsonObj = JObject.Parse(json);
            foreach (var u in jsonObj["users"]["user"])
            {
                string lname = u["@last-name"].ToString();
                string fname = null;
                if (u["@first-name"] != null)
                {
                    fname = u["@first-name"].ToString();
                }

                if (!Object.ReferenceEquals(null, u["@age"]))
                {
                    int age = int.Parse((u["@age"].ToString()));

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Database.ExecuteSqlCommand(
                                "INSERT INTO Users(FirstName, LastName, Age) VALUES ('" + fname + "','" + lname + "'," +
                                age + ")");
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                        }
                    }
                }
                else
                {
                    using (var dbContextTransaction2 = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Database.ExecuteSqlCommand(
                                "INSERT INTO Users(FirstName, LastName) VALUES (" + fname + "," + lname + ")");
                            context.SaveChanges();
                            dbContextTransaction2.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction2.Rollback();
                        }
                    }
                }
            }

        }


        private static void SeedCategories()
        {
            var context = new ShopEntities();
            using (StreamReader file = File.OpenText("../../../categories.json"))
            {
                Category[] categories = JsonConvert.DeserializeObject<Category[]>(file.ReadToEnd());

                foreach (var category in categories)
                {
                    context.Categories.AddOrUpdate(category);
                }

                context.SaveChanges();
            }
        }


        private static void SeedProducts()
        {
            var context = new ShopEntities();
            using (StreamReader file = File.OpenText("../../../products.json"))
            {
                Product[] products = JsonConvert.DeserializeObject<Product[]>(file.ReadToEnd());
                Random rnd = new Random();
                int buyer = rnd.Next(0, 50);
                int seller = 46;
                foreach (var product in products)
                {
                    double price = Double.Parse((product.Price).ToString());
                    string name = product.Name.ToString();
                    if (buyer >= 3 && buyer <= 46)
                    {
                        using (var dbContextTransaction3 = context.Database.BeginTransaction())
                        {
                            try
                            {
                                context.Database.ExecuteSqlCommand(
                                    "INSERT INTO Products(Name, Price, BuyerId, SellerId) VALUES ('" + name + "'," +
                                    price + "," + buyer + "," + seller + ")");
                                context.SaveChanges();
                                dbContextTransaction3.Commit();
                            }
                            catch (Exception)
                            {
                                dbContextTransaction3.Rollback();
                            }
                        }
                    }
                    else
                    {
                        using (var dbContextTransaction5 = context.Database.BeginTransaction())
                        {
                            try
                            {
                                context.Database.ExecuteSqlCommand(
                                    "INSERT INTO Products(Name, Price, SellerId) VALUES ('" + name + "'," +
                                    price + "," + seller + ")");
                                context.SaveChanges();
                                dbContextTransaction5.Commit();
                            }
                            catch (Exception)
                            {
                                dbContextTransaction5.Rollback();
                            }
                        }
                    }
                    buyer = rnd.Next(0, 50);
                    seller--;
                }
            }

        }

        private static void SeedProductCategories()
        {
            var context = new ShopEntities();
            for (int i = 3; i < 47; i++)
            {
                Random rnd = new Random();
                using (var dbContextTransaction4 = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Database.ExecuteSqlCommand(
                            "INSERT INTO ProductCategories(Product_Id, Category_Id) VALUES (" + i + "," +
                            rnd.Next(1, 11) + ")");
                        context.SaveChanges();
                        dbContextTransaction4.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction4.Rollback();
                    }
                }
            }
        }


        private static void ProductsInRange()
        {
            var db = new ShopEntities();
            var products = db.Products.Where(p => p.Price >= 500 && p.Price <= 1000 && p.BuyerId == null)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    Product = p.Name,
                    p.Price,
                    SellerNames = p.Seller.FirstName + " " + p.Seller.LastName
                });
            var json = JsonConvert.SerializeObject(products.ToArray());
            Console.WriteLine(json);
        }

        private static void CategoriesByProductsCount()
        {
            var db = new ShopEntities();
            var categories = from c in db.Categories
                orderby c.Products.Count
                select new
                {
                    category = c.Name,
                    productsCount = c.Products.Count,
                    averagePrice = db.Products.AsEnumerable().Average(a => a.Price),
                    totalRevenue = db.Products.AsEnumerable().Sum(a => a.Price)
                };
            var json = JsonConvert.SerializeObject(categories.ToArray());
            Console.WriteLine(json);
        }

        private static void SuccessfullySoldProducts()
        {
            var db = new ShopEntities();
            var sellers = from s in (db.Users.Where(u => u.SoldProducts.Any())
                .Select(u => new
                {
                    firstName = u.FirstName ?? "-",
                    lastName = u.LastName,
                    soldProducts = u.SoldProducts.Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Buyer.FirstName ?? "-",
                        buyerLastName = p.Buyer.LastName
                    }).Where(p => p.buyerFirstName != null && p.buyerLastName != null)
                }))
                where s.soldProducts.Any()
                orderby s.lastName
                orderby s.firstName
                select new
                {
                    s.firstName,
                    s.lastName,
                    s.soldProducts
                };

            var json = JsonConvert.SerializeObject(sellers.ToArray());
            Console.WriteLine(json);
        }

        private static void UsersAndProducts()
        {
            var db = new ShopEntities();
            var users = db.Users.Where(u => u.SoldProducts.Any()).Select(u => new
            {
                u.FirstName,
                u.LastName,
                u.Age,
                products = u.SoldProducts.Select(p => new
                {
                    p.Name,
                    p.Price
                })
            });
            string fileName = "../../users.xml";
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            using (XmlTextWriter writer = new XmlTextWriter(fileName, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("users");
                writer.WriteAttributeString("count", users.Count().ToString());
                foreach (var user in users)
                {
                    writer.WriteStartElement("user");
                    if (user.FirstName != null)
                    {
                        writer.WriteAttributeString("first-name", user.FirstName);
                    }
                    writer.WriteAttributeString("last-name", user.LastName);
                    if (user.Age != null)
                    {
                        writer.WriteAttributeString("age", user.Age.ToString());
                    }
                    writer.WriteStartElement("sold-products");
                    writer.WriteAttributeString("count", user.products.Count().ToString());
                    foreach (var product in user.products)
                    {
                        writer.WriteStartElement("product");
                        writer.WriteAttributeString("name", product.Name);
                        writer.WriteAttributeString("price", product.Price.ToString()); 
                    }
                }

                writer.WriteEndDocument();
            }
            Console.WriteLine("Document {0} created.", fileName);
        }
    }
}


