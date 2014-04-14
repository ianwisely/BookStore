using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Order
    {
        public Order()
        {
            this.Books = new List<Book>();
        }

        public int Id { get; set; }
        public System.DateTime DateAdded { get; set; }
        public int PaymentType { get; set; }
        public bool IsBought { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
