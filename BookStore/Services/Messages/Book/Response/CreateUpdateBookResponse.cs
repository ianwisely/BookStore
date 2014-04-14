using BookStore.ViewModel.BookModel;

namespace BookStore.Services.Messages.Book.Response
{
    public class CreateUpdateBookResponse : ResponseBase
    {
        public CreateUpdateBookResponse()
        {
            ViewBooksViewModel = new ViewBooksViewModel();
        }

        public ViewBooksViewModel ViewBooksViewModel { get; set; }
    }
}