using BookStore.ViewModel.BookModel;
using BookStore.ViewModel.OrderModel;
using System.Collections.Generic;

namespace BookStore.Services.Messages.Book.Response
{
    public class ViewBooksResponse : ResponseBase
    {
        public ViewBooksResponse()
        {
            ViewBooksViewModel = new ViewBooksViewModel();
            CreateViewBookViewModel = new CreateViewBookViewModel();
            ShoppingCartViewModel = new ShoppingCartViewModel();
        }

        public ViewBooksViewModel ViewBooksViewModel { get; set; }
        public CreateViewBookViewModel CreateViewBookViewModel { get; set; }
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
    }
}