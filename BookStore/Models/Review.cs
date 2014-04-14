using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int NumberOfLikes { get; set; }
        public System.DateTime DateAdded { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
