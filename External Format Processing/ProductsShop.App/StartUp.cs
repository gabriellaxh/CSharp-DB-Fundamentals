namespace ProductsShop.App
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using ProductsShop.Data;
    using ProductsShop.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class StartUp
    {
        static void Main()
        {
            GetUsersAndProductsXML();
        }
        //XML Processing

        static void GetUsersAndProductsXML()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(x => x.ProductsSold.Any(z => z.BuyerId != null))
                    .OrderByDescending(x => x.ProductsSold.Count())
                    .ThenBy(x => x.LastName)
                    .Select(x => new
                    {
                        x.FirstName,
                        x.LastName,
                        x.Age,
                        soldProdsCount = x.ProductsSold.Count,
                        prods = x.ProductsSold.Select(z => new
                        {
                            z.Name,
                            z.Price
                        })
                    });

                var xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("users",
                    new XAttribute("count", users.Count())));

                foreach (var user in users)
                {
                    xmlDoc.Root.Add(new XElement("user",
                       user.FirstName == null ? null : new XAttribute("first-name", user.FirstName),
                                                       new XAttribute("last-name", user.LastName),
                       user.Age == null ? null : new XAttribute("age", user.Age),
                                    new XElement("sold-products",
                                    new XAttribute("count", user.soldProdsCount)),
                                    from prod in user.prods
                                    select
                                    new XElement("product",
                                    new XAttribute("name", prod.Name),
                                    new XAttribute("price", prod.Price)
                       )));
                }
                xmlDoc.Save("Outputs/GetUsersAndProductsXML.xml");
            }
        }

        static void GetCategoriesByProductsCountXML()
        {
            using (var db = new ProductsShopContext())
            {
                var categories = db.Categories
                    .OrderBy(x => x.Products.Count())
                    .Select(x => new
                    {
                        x.Name,
                        numOfProducts = x.Products.Count(),
                        avgPrice = x.Products.Sum(z => z.Product.Price) / x.Products.Count(),
                        totalRevenue = x.Products.Sum(z => z.Product.Price)
                    });


                var xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("categories"));

                foreach (var category in categories)
                {
                    xmlDoc.Root.Add(new XElement("category",
                        new XAttribute("name", category.Name),
                        new XElement("products-count", category.numOfProducts),
                        new XElement("average-price", category.avgPrice),
                        new XElement("total-revenue", category.totalRevenue)));
                }

                xmlDoc.Save("Outputs/GetCategoriesByProductsCountXML.xml");
            }
        }

        static void GetSuccessfullySoldProductsXML()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(x => x.ProductsSold.Any(z => z.BuyerId != null))
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .Select(x => new
                    {
                        x.FirstName,
                        x.LastName,
                        soldProd = x.ProductsSold.Select(z => new
                        {
                            z.Name,
                            z.Price
                        })
                    });

                var xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("users"));

                foreach (var user in users)
                {
                    xmlDoc.Root.Add(new XElement("user",
                                                user.FirstName == null ? null : new XAttribute("first-name", $"{user.FirstName}"),
                                                                                new XAttribute("last-name", $"{user.LastName}"),
                                                                                    new XElement("sold-products",
                                                                                    from soldPr in user.soldProd
                                                                                    select
                                                                                          new XElement("product",
                                                                                          new XElement("name", soldPr.Name),
                                                                                          new XElement("price", soldPr.Price)
                                                                                          ))));
                }

                xmlDoc.Save("Outputs/GetSuccessfullySoldProductsXML.xml");
            }
        }

        static void GetProductsInRangeXML()
        {
            using (var db = new ProductsShopContext())
            {
                var products = db.Products
                    .Where(x => x.Price >= 1000 && x.Price <= 2000)
                    .OrderBy(x => x.Price)
                    .Select(x => new
                    {
                        x.Name,
                        x.Price,
                        BuyerFullName = $"{x.Buyer.FirstName} {x.Buyer.LastName}"
                    })
                    .ToArray();


                var xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("products"));

                foreach (var prod in products)
                {
                    xmlDoc.Root.Add(new XElement("product",
                                                 new XAttribute("name", $"{prod.Name}"),
                                                 new XAttribute("price", $"{prod.Price}"),
                                                 new XAttribute("buyer", $"{prod.BuyerFullName}")));
                }

                xmlDoc.Save("Outputs/GetProductsInRangeXML.xml");
            }
        }


        static string SetCategoriesXML()
        {
            int catPrCount = 0;

            using (var db = new ProductsShopContext())
            {
                var productsIds = db.Products
                    .Select(x => x.Id)
                    .ToArray();

                var categoryIds = db.Categories
                    .Select(x => x.Id)
                    .ToArray();

                var rnd = new Random();

                var categoryProducts = new List<CategoryProduct>();

                foreach (var prod in productsIds)
                {
                    var index = rnd.Next(0, categoryIds.Length);

                    var categoryId = categoryIds[index];

                    var categoryProduct = new CategoryProduct()
                    {
                        ProductId = prod,
                        CategoryId = categoryId
                    };

                    categoryProducts.Add(categoryProduct);
                }

                catPrCount = categoryProducts.Count();

                db.CategoryProducts.AddRange(categoryProducts);
                db.SaveChanges();
            }

            return $"{catPrCount} products were imported from XML file.";

        }


        internal static string ImportProductsFromXml()
        {
            var path = "Inputs/products.xml";
            var importedProducts = ImportXml<Product>(path);

            var products = new List<Product>();

            using (var db = new ProductsShopContext())
            {
                var userIds = db.Users
                    .Select(x => x.Id)
                    .ToArray();

                var rnd = new Random();

                foreach (var impProds in importedProducts)
                {
                    string name = impProds.Element("name").Value;
                    decimal price = decimal.Parse(impProds.Element("price").Value);

                    int randomSeller = rnd.Next(0, userIds.Length);
                    int sellerId = userIds[randomSeller];

                    int? buyerId = sellerId;
                    while (buyerId == sellerId)
                    {
                        int randomBuyer = rnd.Next(0, userIds.Length);
                        buyerId = userIds[randomBuyer];

                        if (buyerId % 4 == 0 || buyerId % 5 == 0)
                        {
                            buyerId = null;
                        }
                    }

                    var newProduct = new Product()
                    {
                        Name = name,
                        Price = price,
                        SellerId = sellerId,
                        BuyerId = buyerId
                    };

                    products.Add(newProduct);
                }

                db.Products.AddRange(products);
                db.SaveChanges();
            }

            return $"{products.Count} products were imported from XML file.";

        }

        internal static string ImportCategoriesFromXml()
        {
            var path = "Inputs/categories.xml";
            var importedCategories = ImportXml<Category>(path);

            var categories = new List<Category>();

            foreach (var impCategory in importedCategories)
            {
                var name = impCategory.Element("name").Value;

                var category = new Category()
                {
                    Name = name
                };

                categories.Add(category);
            }

            using (var db = new ProductsShopContext())
            {
                db.Categories.AddRange(categories);
                db.SaveChanges();
            }
            return $"{categories.Count} categories was imported from XML file: {path}";
        }

        internal static string ImportUsersFromXml()
        {
            var path = "Inputs/users.xml";
            var importedUsers = ImportXml<User>(path);

            var users = new List<User>();

            foreach (var impUser in importedUsers)
            {
                string firstName = impUser.Attribute("firstName")?.Value;
                string lastName = impUser.Attribute("lastName").Value;

                int? age = null;
                if (impUser.Attribute("age") != null)
                {
                    age = int.Parse(impUser.Attribute("age").Value);
                }

                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };

                users.Add(user);
            }
            using (var db = new ProductsShopContext())
            {
                db.Users.AddRange(users);

                db.SaveChanges();
            }

            return $"{users.Count} users were imported from XML file.";
        }


        internal static IEnumerable<XElement> ImportXml<T>(string path)
        {
            string xml = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xml);
            var elements = xmlDoc.Root.Elements();

            return elements;
        }



        //JSON Processing
        static void GetUsersAndProductsJSON()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(x => x.ProductsSold.Any(z => z.BuyerId != null))
                    .OrderByDescending(x => x.ProductsSold.Count)
                    .ThenBy(x => x.LastName)
                    .Select(x => new
                    {
                        x.FirstName,
                        x.LastName,
                        x.Age,
                        soldProdsCount = x.ProductsSold.Select(j => new
                        {
                            count = x.ProductsSold.Count(),
                            prods = x.ProductsSold
                                 .Select(pp => new
                                 {
                                     pp.Name,
                                     pp.Price
                                 })
                        })
                    })
                    .ToArray();

                var usersCount = new
                {
                    usersCount = users.Count(),
                    users
                };

                var jsonExport = JsonConvert.SerializeObject(usersCount, Formatting.Indented);
                File.WriteAllText("Outputs/UsersAndProducts.json", jsonExport);
            }
        }

        static void GetCategoriesByProductsCountJSON()
        {
            using (var db = new ProductsShopContext())
            {
                var categories = db.Categories
                    .OrderBy(x => x.Name)
                    .Select(z => new
                    {
                        z.Name,
                        numOfProducts = z.Products.Count,
                        avgPrice = z.Products.Sum(p => p.Product.Price) / z.Products.Count,
                        totalRevenue = z.Products.Sum(p => p.Product.Price)
                    })
                    .ToArray();

                var jsonExport = JsonConvert.SerializeObject(categories, Formatting.Indented);
                File.WriteAllText("Outputs/CategoriesByProductsCount.json", jsonExport);

            }
        }

        static void GetSuccessfullySoldProductsJSON()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(x => x.ProductsSold.Any(z => z.BuyerId != null))
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .Select(x => new
                    {
                        x.FirstName,
                        x.LastName,
                        ProdctsSold = x.ProductsSold
                        .Select(z => new
                        {
                            z.Name,
                            z.Price,
                            BuyerFN = z.Buyer.FirstName,
                            BuyerLN = z.Buyer.LastName
                        })
                    }).ToArray();

                var jsonExport = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText("Outputs/SuccessfullySoldProducts.json", jsonExport);
            }
        }

        static void GetProductsInRangeJSON()
        {
            using (var db = new ProductsShopContext())
            {
                var products = db.Products
                    .Where(x => x.Price >= 500 && x.Price <= 1000)
                    .OrderBy(x => x.Price)
                    .Select(x => new
                    {
                        x.Name,
                        x.Price,
                        Seller = $"{x.Seller.FirstName} {x.Seller.LastName}"
                    })
                    .ToArray();

                var jsonExport = JsonConvert.SerializeObject(products, Formatting.Indented);

                File.WriteAllText("Outputs/ProductsInRange.json", jsonExport);
            }
        }


        static string SetCategoriesJSON()
        {
            var catCount = 0;

            using (var db = new ProductsShopContext())
            {
                var productsIds = db.Products
                    .Select(x => x.Id)
                    .ToArray();

                var categoryIds = db.Categories
                    .Select(x => x.Id)
                    .ToArray();

                var rnd = new Random();

                var categoryProductsList = new List<CategoryProduct>();

                foreach (var prod in productsIds)
                {
                    var index = rnd.Next(0, categoryIds.Length);

                    var category = categoryIds[index];

                    var categoryProduct = new CategoryProduct()
                    {
                        ProductId = prod,
                        CategoryId = category
                    };

                    categoryProductsList.Add(categoryProduct);
                }

                catCount = categoryProductsList.Count();

                db.CategoryProducts.AddRange(categoryProductsList);
                db.SaveChanges();
            }
            return $"{catCount} categories were added to products.";
        }


        internal static string ImportProductsFromJson()
        {
            string path = "Inputs/products.json";
            var products = ImportJson<Product>(path);


            var rnd = new Random();
            using (var db = new ProductsShopContext())
            {
                var userIds = db.Users
                    .Select(x => x.Id)
                    .ToArray();

                foreach (var prod in products)
                {
                    var index = rnd.Next(0, userIds.Length);
                    var sellerId = userIds[index];

                    prod.SellerId = sellerId;

                    var buyerId = sellerId;

                    while (buyerId == sellerId)
                    {
                        index = rnd.Next(0, userIds.Length);
                        buyerId = userIds[index];
                        prod.BuyerId = buyerId;
                    }
                }

                //make some of them null
                foreach (var product in products)
                {
                    if (product.BuyerId % 4 == 0 || product.BuyerId % 4 == 0)
                    {
                        product.BuyerId = null;
                    }
                }

                db.Products.AddRange(products);
                db.SaveChanges();
            }
            return $"{products.Length} products were imported from file: {path}.";
        }

        internal static string ImportCategoriesFromJson()
        {
            string path = "Inputs/categories.json";

            var categories = ImportJson<Category>(path);

            using (var db = new ProductsShopContext())
            {
                db.Categories.AddRange(categories);

                db.SaveChanges();
            }
            return $"{categories.Length} users were imported from file: {path}.";
        }

        internal static string ImportUsersFromJson()
        {
            var path = "Inputs/users.json";

            var users = ImportJson<User>(path);


            using (var db = new ProductsShopContext())
            {
                db.Users.AddRange(users);

                db.SaveChanges();
            }

            return $"{users.Length} users were imported from file: {path}.";
        }


        internal static T[] ImportJson<T>(string path)
        {
            string jsonString = File.ReadAllText(path);

            var objects = JsonConvert.DeserializeObject<T[]>(jsonString);

            return objects;
        }





        internal static void InitializeDatabase()
        {
            try
            {
                using (var db = new ProductsShopContext())
                {
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    Console.WriteLine("Database successfully created/migrated!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }




    }
}




