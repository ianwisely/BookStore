
using System;
namespace BookStore.Services.Messages.Order.Request
{
    public class PurchaseAddBookToShoppingCartRequest
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime DateAdded { get; set; }
        public int PaymentType { get; set; }
        public int UserPoints { get; set; }
        public decimal Price { get; set; }
    }
}