using BookStore.ViewModel.BookModel;

namespace BookStore.Services.Messages.Order.Response
{
    public class PurchaseBooksResponse : ResponseBase
    {
        public PurchaseBooksResponse()
        {
            ViewBooksViewModel = new ViewBooksViewModel();
        }

        public ViewBooksViewModel ViewBooksViewModel { get; set; }
        public int Points { get; set; }
    }
}