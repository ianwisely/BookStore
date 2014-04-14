using BookStore.Models;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Services.SubClasses.Orders
{
    public class NoOrderDiscount : IOrderDiscount
    {
        public decimal GetTotalPrice(List<Book> books, int userId, int beforePoints, out int afterPoints)
        {
            decimal totalCost = 0;
            foreach (var book in books)
            {
                totalCost += book.Price;
            }

            double totalCostAsDouble = (double)totalCost;
            afterPoints = beforePoints + Convert.ToInt32(Math.Round(totalCostAsDouble * .1));
            return totalCost;
        }
    }
}