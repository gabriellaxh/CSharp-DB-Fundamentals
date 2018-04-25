using System;
using FastFood.Data;
using System.Linq;
using System.Text;

namespace FastFood.DataProcessor
{
    public static class Bonus
    {
        public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
        {
            var item = context.Items.SingleOrDefault(x => x.Name == itemName);

            if (item == null)
            {
                return $"Item {itemName} not found!";
            }

            var firstPrice = item.Price;
            item.Price = newPrice;
            context.SaveChanges();

            return $"{itemName} Price updated from ${firstPrice:f2} to ${item.Price:f2}";
        }
    }
}
