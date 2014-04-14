
using BookStore.Models;
using BookStore.Repository;
using BookStore.Repository.Repositories;
using BookStore.Services.Messages.Book.Request;
using BookStore.Services.Messages.Book.Response;
using System.Collections.Generic;
using System;
using BookStore.ViewModel.BookModel;
using System.IO;
using System.Drawing;
using System.Web.Mvc;
namespace BookStore.Services.Services
{
    public class BookService : ServiceBase
    {
        private static BookService _bookService = new BookService();
        private BookRepository _bookRepository;

        private BookService() { }

        public new static ServiceBase GetInstance()
        {
            return _bookService;
        }

        public ViewBooksResponse ViewBooks(int userId)
        {
            ViewBooksResponse response = new ViewBooksResponse();
            _bookRepository = (BookRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.BookRepository);
            try
            {
                List<Book> books = _bookRepository.GetBooks();
                var order = _bookRepository.GetOrderByUserId(userId);
                if (order != null)
                {
                    foreach (var book in order.Books)
                    {
                        CreateViewBookViewModel model = new CreateViewBookViewModel
                        {
                            Title = book.Title,
                            Price = book.Price
                        };
                        response.ViewBooksViewModel.ShoppingCartViewModel.Books.Add(model);
                    }
                }
                foreach (var book in books)
                {
                    CreateViewBookViewModel model = new CreateViewBookViewModel
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Name = book.AuthorFirstName + " " + book.AuthorLastName,
                        Category = book.Category,
                        Price = book.Price,
                        ISBN = book.ISBN,
                        AmountInStock = book.AmountInStock
                    };
                    model.CoverImage = ViewImage(book.CoverImage);
                    response.ViewBooksViewModel.CreateViewBookViewModels.Add(model);
                }
                response.Success = true;
            }
            catch (Exception)
            {
                response.Success = false;
            }
            finally
            {
                _bookRepository.Dispose();
            }
            return response;
        }

        public ViewBooksResponse SearchForBooks(string searchQuery)
        {
            ViewBooksResponse response = new ViewBooksResponse();
            _bookRepository = (BookRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.BookRepository);
            try
            {
                List<Book> books = _bookRepository.SeachBooks(searchQuery);
                foreach (var book in books)
                {
                    CreateViewBookViewModel model = new CreateViewBookViewModel
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Name = book.AuthorFirstName + " " + book.AuthorLastName,
                        Category = book.Category,
                        Price = book.Price,
                        ISBN = book.ISBN,
                        AmountInStock = book.AmountInStock
                        //cover image
                    };
                }
                response.Success = true;
            }
            catch (Exception)
            {
                response.Success = false;
            }
            finally
            {
                _bookRepository.Dispose();
            }
            return response;
        }

        public CreateUpdateBookResponse CreateBook(CreateUpdateBookRequest request)
        {
            CreateUpdateBookResponse response = new CreateUpdateBookResponse();
            _bookRepository = (BookRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.BookRepository);
            try
            {
                Book book = new Book
                {
                    Title = request.CreateBookViewModel.Title,
                    AuthorFirstName = request.CreateBookViewModel.AuthorFirstName,
                    AuthorLastName = request.CreateBookViewModel.AuthorLastName,
                    Price = request.CreateBookViewModel.Price,
                    Category = request.CreateBookViewModel.Category,
                    CoverImage = request.CreateBookViewModel.CoverImage,
                    ISBN = request.CreateBookViewModel.ISBN,
                    AmountInStock = request.CreateBookViewModel.AmountInStock
                };
                _bookRepository.InsertBook(book);
                response.ViewBooksViewModel.CreateViewBookViewModel = request.CreateBookViewModel;
                response.ViewBooksViewModel.CreateViewBookViewModel.CoverImage = ViewImage(book.CoverImage);
                response.Success = true;
            }
            catch (Exception)
            {
                response.Success = false;
            }
            finally
            {
                _bookRepository.Dispose();
            }
            return response;
        }

        public GetBookResponse GetBook(int bookId)
        {
            GetBookResponse response = new GetBookResponse();
            _bookRepository = (BookRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.BookRepository);
            try
            {
                Book book = _bookRepository.GetBook(bookId);
                response.CreateViewBookViewModel = new CreateViewBookViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Name = book.AuthorFirstName + " " + book.AuthorLastName,
                    Category = book.Category,
                    Price = book.Price,
                    ISBN = book.ISBN,
                    AmountInStock = book.AmountInStock,
                    CoverImage = ViewImage(book.CoverImage)
                };
                response.Success = true;
            }
            catch (Exception)
            {
                response.Success = false;
            }
            finally
            {
                _bookRepository.Dispose();
            }
            return response;
        }

        public CreateUpdateBookResponse UpdateBook(CreateUpdateBookRequest request)
        {
            CreateUpdateBookResponse response = new CreateUpdateBookResponse();
            _bookRepository = (BookRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.BookRepository);
            try
            {
                Book book = _bookRepository.GetBook(request.CreateBookViewModel.Id);
                book.AmountInStock = request.CreateBookViewModel.AmountInStock;
                book.Title = request.CreateBookViewModel.Title;
                book.Price = request.CreateBookViewModel.Price;
                book.ISBN = request.CreateBookViewModel.ISBN;
                book.AuthorFirstName = request.CreateBookViewModel.AuthorFirstName;
                book.AuthorLastName = request.CreateBookViewModel.AuthorLastName;
                book.Category = request.CreateBookViewModel.Category;
                _bookRepository.UpdateBook(book);
                response.ViewBooksViewModel.CreateViewBookViewModel = request.CreateBookViewModel;
                response.Success = true;
            }
            catch (Exception)
            {
                response.Success = false;
            }
            finally
            {
                _bookRepository.Dispose();
            }
            return response;
        }

        public DeleteBookResponse DeleteBook(int id)
        {
            DeleteBookResponse response = new DeleteBookResponse();
            _bookRepository = (BookRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.BookRepository);
            try
            {
                var book = _bookRepository.GetBook(id);
                _bookRepository.DeleteBook(book);
                response.Success = true;
            }
            catch (Exception)
            {
                response.Success = false;
            }
            finally
            {
                _bookRepository.Dispose();
            }
            return response;
        }

        private byte[] ViewImage(byte[] requestedImage)
        {
            MemoryStream stream = new MemoryStream(requestedImage);
            BinaryReader reader = new BinaryReader(stream);
            byte[] image = reader.ReadBytes((int)stream.Length);
            reader.Close();
            stream.Close();
            return image;
        }
    }
}