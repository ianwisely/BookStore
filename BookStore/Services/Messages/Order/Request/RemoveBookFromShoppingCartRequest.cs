using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Services.Messages.Order.Request
{
    public class RemoveBookFromShoppingCartRequest
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}