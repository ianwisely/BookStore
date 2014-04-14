using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Repository.Repositories
{
    public class BookRepository : RepositoryBase
    {
        private static BookStoreContext context;
        private static BookRepository _bookRepository = new BookRepository();

        private BookRepository() { }

        public new static RepositoryBase GetInstance()
        {
            context = new BookStoreContext();
            return _bookRepository;
        }
        
        public new void Dispose()
        {
            context.Dispose();
        }

        public Book GetBook(int id)
        {
            return context.Books.SingleOrDefault(b => b.Id == id);
        }

        public List<Book> GetBooks()
        {
            return context.Books.ToList();
        }

        public List<Book> SeachBooks(string seachQuery)
        {
            List<Book> books = new List<Book>();
            var titles = context.Books.Where(b => b.Title.Contains(seachQuery));
            if (titles.Count() > 0)
            {
                books.AddRange(titles);
            }
            var authorFirstNames = context.Books.Where(b => b.AuthorFirstName.Contains(seachQuery));
            if (authorFirstNames.Count() > 0)
            {
                books.AddRange(authorFirstNames);
            }
            var authorLastNames = context.Books.Where(b => b.AuthorLastName.Contains(seachQuery));
            if (authorLastNames.Count() > 0)
            {
                books.AddRange(authorLastNames);
            }
            var categories = context.Books.Where(b => b.Category.Contains(seachQuery));
            if (categories.Count() > 0)
            {
                books.AddRange(categories);
            }
            var isbns = context.Books.Where(b => b.ISBN.Contains(seachQuery));
            if (isbns.Count() > 0)
            {
                books.AddRange(isbns);
            }
            books.OrderBy(b => b.Title);
            return books;
        }

        public void InsertBook(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            context.Books.Attach(book);
            context.Entry(book).State = System.Data.EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteBook(Book book)
        {
            context.Books.Attach(book);
            context.Entry(book).State = System.Data.EntityState.Deleted;
            context.SaveChanges();
        }

        public void AddBookToShoppingCart(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            context.Orders.Attach(order);
            context.Entry(order).State = System.Data.EntityState.Modified;
            context.SaveChanges();
        }

        public void RemoveBookFromShoppingCart(Order order)
        {
            context.Orders.Attach(order);
            context.Entry(order).State = System.Data.EntityState.Modified;
            context.SaveChanges();
        }

        public List<Order> GetPurchases(int userId)
        {
            return context.Orders.Where(p => p.UserId == userId).ToList();
        }

        public bool IsShoppingCartEmpty(int userId)
        {
            var orders = context.Users.SingleOrDefault(u => u.Id == userId).Orders;
            if (orders != null)
            {
                if (orders.Count > 0)
                {
                    if (orders.Any(o => !o.IsBought))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Order GetOrderByUserId(int userId)
        {
            return context.Orders.SingleOrDefault(o => o.UserId == userId && !o.IsBought);
        }

        public Order GetOrderByOrderId(int orderId)
        {
            return context.Orders.SingleOrDefault(o => o.Id == orderId);
        }

        public Order GetOrder(int userId)
        {
            return context.Users.SingleOrDefault(u => u.Id == userId).Orders.SingleOrDefault(o => !o.IsBought);
        }

        public void DeleteOrder(Order order)
        {
            context.Orders.Attach(order);
            context.Entry(order).State = System.Data.EntityState.Deleted;
            context.SaveChanges();
        }

        public int GetUserPoints(int userId)
        {
            return context.Users.SingleOrDefault(u => u.Id == userId).LoyaltyCardPoints;
        }

        public void PurchaseBooks(Order order, int userId, int points)
        {
            //needed?
            foreach (var book in order.Books.ToList())
            {
                var currentBook = context.Books.SingleOrDefault(b => b.Id == book.Id);
                context.Books.Attach(book);
                context.Entry(book).State = System.Data.EntityState.Modified;
            }
            context.Orders.Attach(order);
            context.Entry(order).State = System.Data.EntityState.Modified;
            var user = context.Users.SingleOrDefault(u => u.Id == userId);
            user.LoyaltyCardPoints = points;
            context.Users.Attach(user);
            context.Entry(user).State = System.Data.EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Order> GetUserOrders(int userId)
        {
            return context.Users.SingleOrDefault(u => u.Id == userId).Orders;
        }
    }
}