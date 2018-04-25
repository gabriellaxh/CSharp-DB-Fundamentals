using System;
using FastFood.Data;
using System.Text;
using Newtonsoft.Json;
using FastFood.DataProcessor.Dto.Export;
using FastFood.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace FastFood.DataProcessor
{
    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var employees = JsonConvert.DeserializeObject<EmployeeDtoImport[]>(jsonString);

            var employeesList = new List<Employee>();
            var positionList = new List<Position>();

            foreach (var employee in employees)
            {
                if (!IsValid(employee) || employeesList.Any(x => x.Name == employee.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var position = positionList.SingleOrDefault(x => x.Name == employee.Position);
                if (position == null)
                {
                    position = new Position()
                    {
                        Name = employee.Position
                    };
                    positionList.Add(position);

                }
                var newEmployee = new Employee()
                {
                    Name = employee.Name,
                    Age = employee.Age,
                    Position = position
                };

                employeesList.Add(newEmployee);
                sb.AppendLine(String.Format(SuccessMessage, employee.Name));
            }
            context.Employees.AddRange(employeesList);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var items = JsonConvert.DeserializeObject<ItemDtoImport[]>(jsonString);

            var itemsList = new List<Item>();
            var categoriesList = new List<Category>();

            foreach (var item in items)
            {
                if (!IsValid(item) || itemsList.Any(x => x.Name == item.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var category = categoriesList.SingleOrDefault(x => x.Name == item.Category);

                if (category == null)
                {
                    category = new Category()
                    {
                        Name = item.Category
                    };
                    categoriesList.Add(category);
                }
                var newItem = new Item()
                {
                    Name = item.Name,
                    Price = item.Price,
                    Category = category
                };

                itemsList.Add(newItem);
                sb.AppendLine(string.Format(SuccessMessage, item.Name));
            }
            context.Items.AddRange(itemsList);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var xmlDoc = XDocument.Parse(xmlString);

            var orders = xmlDoc.Element("Orders").Elements();

            var ordersList = new List<Order>();
            foreach (var order in orders)
            {
                string customer = order.Element("Customer").Value;
                string employeeName = order.Element("Employee").Value;
                var dateTime = DateTime.ParseExact(order.Element("DateTime").Value, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var type = Enum.Parse<OrderType>(order.Element("Type").Value);

                var employee = context.Employees.SingleOrDefault(x => x.Name == employeeName);
                if (!IsValid(order) || employee == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var newOrder = new Order()
                {
                    Customer = customer,
                    Employee = employee,
                    DateTime = dateTime,
                    Type = type
                };
                ordersList.Add(newOrder);


                var items = order.Element("Items").Elements();

                var orderItems = new List<OrderItem>();
                foreach (var item in items)
                {
                    var itemName = item.Element("Name").Value;
                    int quantity = int.Parse(item.Element("Quantity").Value);

                    var itemCheck = context.Items.SingleOrDefault(x => x.Name == itemName);

                    if (itemCheck == null)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }

                    var newOrderItem = new OrderItem()
                    {
                        Item = itemCheck,
                        Quantity = quantity
                    };
                    orderItems.Add(newOrderItem);
                }
                newOrder.OrderItems = orderItems;

                sb.AppendLine(string.Format($"Order for {newOrder.Customer} on {newOrder.DateTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)} added"));
            }

            context.Orders.AddRange(ordersList);
            context.SaveChanges();

            var result = sb.ToString();
            return result;

        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }
    }
}