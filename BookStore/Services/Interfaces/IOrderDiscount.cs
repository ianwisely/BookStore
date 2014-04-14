using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface IOrderDiscount
    {
        decimal GetTotalPrice(List<Book> books, int userId, int beforePoints, out int afterPoints);
    }
}
