using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class User
    {
        public User()
        {
            this.Orders = new List<Order>();
            this.Reviews = new List<Review>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public int LoyaltyCardPoints { get; set; }
        public Nullable<int> CreditCardNumber { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
