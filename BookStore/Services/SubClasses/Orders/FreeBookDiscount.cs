using BookStore.Models;
using BookStore.Repository;
using BookStore.Repository.Repositories;
using BookStore.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Services.SubClasses.Orders
{
    public class FreeBookDiscount : IOrderDiscount
    {
        private UserRepository _userRepository;

        public decimal GetTotalPrice(List<Book> books, int userId, int beforePoints, out int afterPoints)
        {
            books.OrderBy(b => b.Price);
            decimal totalCost = 0;
            foreach (var book in books.Skip(1))
            {
                totalCost += book.Price;
            }

            _userRepository = (UserRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.UserRepository);
            int userPoints = _userRepository.GetUser(userId).LoyaltyCardPoints;
            afterPoints = userPoints - 100;

            if (totalCost < 0)
            {
                return 0;
            }
            return totalCost;
        }
    }
}