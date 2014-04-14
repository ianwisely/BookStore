using BookStore.ViewModel.BookModel;

namespace BookStore.Services.Messages.Book.Request
{
    public class CreateUpdateBookRequest
    {
        public CreateViewBookViewModel CreateBookViewModel { get; set; }
    }
}