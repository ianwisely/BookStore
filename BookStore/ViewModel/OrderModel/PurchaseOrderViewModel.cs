using BookStore.ViewModel.BookModel;
using System;

namespace BookStore.ViewModel.OrderModel
{
    public class PurchaseOrderViewModel
    {
        public PurchaseOrderViewModel()
        {
            ShoppingCartViewModel = new ShoppingCartViewModel();
        }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime DateAdded { get; set; }
        public int PaymentType { get; set; }
        //public ViewBooksViewModel Books { get; set; }
        public ShoppingCartViewModel ShoppingCartViewModel;
        public int UserPoints { get; set; }
        public decimal TotalPrice { get; set; }
        public int AfterPointsNoDiscount { get; set; }
        public decimal FreeBookPrice { get; set; }
        public int AfterPointsFreeBook { get; set; }
        public decimal DiscountPrice { get; set; }
        public int AfterPointsDiscount { get; set; }
        public bool CanCancel { get; set; }
        public bool IsBought { get; set; }
    }
}