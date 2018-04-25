using System;
using FastFood.Data;
using System.Linq;
using FastFood.Models;
using Newtonsoft.Json;
using System.Xml.Linq;
using FastFood.DataProcessor.Dto.Export;

namespace FastFood.DataProcessor
{
    public class Serializer
    {
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            var type = Enum.Parse<OrderType>(orderType);

            var employee = context
                .Employees
                .Where(e => e.Name == employeeName)
                .Select(e => new
                {
                    e.Name,
                    Orders = e.Orders.Where(o => o.Type == type).Select(o => new
                    {
                        o.Customer,
                        Items = o.OrderItems.Select(oi => new
                        {
                            oi.Item.Name,
                            oi.Item.Price,
                            oi.Quantity
                        }).ToArray(),
                        TotalPrice = o.OrderItems.Sum(oi => oi.Item.Price * oi.Quantity)
                    })
                        .OrderByDescending(o => o.TotalPrice)
                        .ThenByDescending(o => o.Items.Length)
                        .ToArray(),
                    TotalMade = e.Orders
                        .Where(o => o.Type == type)
                        .Sum(o => o.OrderItems.Sum(oi => oi.Item.Price * oi.Quantity))
                })
                .SingleOrDefault();

            var json = JsonConvert.SerializeObject(employee, Formatting.Indented);

            return json;
        }


        public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
        {
            var categoriesNames = categoriesString.Split(',');

            var categories = context.Categories
                .Where(x => categoriesNames.Any(z => z == x.Name))
                .Select(x => new
                {
                    x.Name,
                    MostPopularItem = x.Items
                    .OrderByDescending(z => z.OrderItems.Sum(s => s.Item.Price * s.Quantity))
                    .FirstOrDefault()
                })
                .Select(x => new CategoryDtoExport()
                {
                    Name = x.Name,
                    MostPopularItem = new ItemDtoExport()
                    {
                        Name = x.MostPopularItem.Name,
                        TotalMade = x.MostPopularItem.OrderItems.Sum(z => z.Item.Price * z.Quantity),
                        TimesSold = x.MostPopularItem.OrderItems.Sum(z => z.Quantity)
                    }
                })
                .OrderByDescending(c => c.MostPopularItem.TotalMade)
            .ThenByDescending(c => c.MostPopularItem.TimesSold)
            .ToArray();

            var xmlDoc = new XDocument(new XElement("Categories",
                                                     from c in categories
                                                     select
                                                     new XElement("Category",
                                                     new XElement("Name", c.Name),
                                                     new XElement("MostPopularItem",
                                                     new XElement("Name", c.MostPopularItem.Name),
                                                     new XElement("TotalMade", c.MostPopularItem.TotalMade),
                                                     new XElement("TimesSold", c.MostPopularItem.TimesSold))
                )));
            var result = xmlDoc.ToString();
            return result;
        }
    }
}

