using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Services.Messages.Order.Request
{
    public class CancelOrderRequest
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
    }
}