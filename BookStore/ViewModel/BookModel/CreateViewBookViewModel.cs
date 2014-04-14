
namespace BookStore.ViewModel.BookModel
{
    public class CreateViewBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public byte[] CoverImage { get; set; }
        public string ISBN { get; set; }
        public int AmountInStock { get; set; }
        public int UserId { get; set; }
    }
}