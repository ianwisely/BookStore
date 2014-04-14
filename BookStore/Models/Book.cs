using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Book
    {
        public Book()
        {
            this.Orders = new List<Order>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public byte[] CoverImage { get; set; }
        public string ISBN { get; set; }
        public int AmountInStock { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
