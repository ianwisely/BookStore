using BookStore.Models;
using BookStore.Repository;
using BookStore.Repository.Repositories;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Services.SubClasses.Orders
{
    public class PriceDeductionDiscount : IOrderDiscount
    {
        public decimal GetTotalPrice(List<Book> books, int userId, int beforePoints, out int afterPoints)
        {
            decimal totalCost = 0;
            foreach (var book in books)
            {
                totalCost += book.Price;
            }
            double discount = ((double)beforePoints * .1);
            totalCost -= (decimal)discount;

            var pointsDeduction = Convert.ToInt32(Math.Ceiling(discount * 10));
            afterPoints = beforePoints - pointsDeduction;
            if (afterPoints < 0)
            {
                afterPoints = 0;
            }

            if (totalCost < 0)
            {
                return 0;
            }
            return totalCost;
        }
    }
}