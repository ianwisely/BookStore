using BookStore.Models;
using BookStore.Repository;
using BookStore.Repository.Repositories;
using BookStore.Services.Enums;
using BookStore.Services.Factories;
using BookStore.Services.Interfaces;
using BookStore.Services.Messages.Order.Request;
using BookStore.Services.Messages.Order.Response;
using BookStore.Services.SubClasses.Orders;
using BookStore.ViewModel.BookModel;
using BookStore.ViewModel.OrderModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore.Services.Services
{
	public class OrderService : ServiceBase
	{
		private static OrderService _orderService = new OrderService();
		private BookRepository _bookRepository;
		private UserRepository _userRepository;

		private IOrderState _orderState;
		private IOrderDiscount _orderDiscount;


		private OrderService() { }

		public new static ServiceBase GetInstance()
		{
			return _orderService;
		}

        public CancelOrderResponse GetUserOrders(int userId)
        {
            CancelOrderResponse response = new CancelOrderResponse();
            _bookRepository = (BookRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.BookRepository);
			try
			{
                IEnumerable<Order> orders = _bookRepository.GetUserOrders(userId);
                foreach (var order in orders)
                {
                    if (order.DateAdded > DateTime.Now.AddHours(24))
                    {
                        _orderState = OrderCantCancelState.GetInstance();
                    }
                    else
                    {
                        _orderState = OrderCancelState.GetInstance();
                    }
                    PurchaseOrderViewModel model = new PurchaseOrderViewModel
                    {
                        OrderId = order.Id,
                        CanCancel = _orderState.CanCancel(order),
                        DateAdded = order.DateAdded,
                        IsBought = order.IsBought
                        
                    };
                    foreach (var book in order.Books)
                    {
                        CreateViewBookViewModel bookModel = new CreateViewBookViewModel
                        {
                            Id = book.Id,
                            Title = book.Title
                        };
                        model.ShoppingCartViewModel.Books.Add(bookModel);
                    }
                    response.MyOrdersViewModel.PurchaseOrderViewModel.Add(model);
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

        public CancelOrderResponse CancelOrder(CancelOrderRequest request)
        {
            CancelOrderResponse response = new CancelOrderResponse();
            _bookRepository = (BookRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.BookRepository);
			try
			{
                var deletedOrder = _bookRepository.GetOrderByOrderId(request.OrderId);
                _orderState = OrderCancelState.GetInstance();
                Cancel(deletedOrder);
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

		public bool CanCancel(Order order)
		{
			return _orderState.CanCancel(order);
		}

		public void Cancel(Order order)
		{
			if (CanCancel(order))
			{
				_orderState.CancelOrder(order, _bookRepository);
			}
		}

		public PurchaseBooksResponse PurchaseBooks(PurchaseAddBookToShoppingCartRequest request)
		{
			PurchaseBooksResponse response = new PurchaseBooksResponse();
			_bookRepository = (BookRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.BookRepository);
			try
			{
				int afterPoints = 0;
				Order order = _bookRepository.GetOrderByUserId(request.UserId);
				if (order.Books.Any(b => b.AmountInStock == 0))
				{
					response.Success = false;
				}
				else
				{
					_orderDiscount = OrderDiscountFactory.GetDiscountAndRemainingLoyaltyPoints(request.PaymentType);
					order.PriceAfterDiscount = _orderDiscount.GetTotalPrice(order.Books.ToList(), request.UserId, request.UserPoints, out afterPoints);
					order.IsBought = true;
					order.PaymentType = request.PaymentType;
					order.DateAdded = request.DateAdded;
					_bookRepository.PurchaseBooks(order, request.UserId, afterPoints);
					response.Points = afterPoints;
					response.Success = true;
				}
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

		public AddBookToShoppingCartResponse AddBookToShoppingCart(PurchaseAddBookToShoppingCartRequest request)
		{
			AddBookToShoppingCartResponse response = new AddBookToShoppingCartResponse();
			_bookRepository = (BookRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.BookRepository);
			try
			{
				bool isShoppingCartEmpty = _bookRepository.IsShoppingCartEmpty(request.UserId);
				var book = _bookRepository.GetBook(request.BookId);
				if (book.AmountInStock > 0)
				{
					Order order;
					if (isShoppingCartEmpty)
					{
						order = new Order
						{
							DateAdded = request.DateAdded,
							IsBought = false,
							PriceAfterDiscount = book.Price,
							UserId = request.UserId
						};
						order.Books.Add(book);
						_bookRepository.AddBookToShoppingCart(order);
					}
					else
					{
						order = _bookRepository.GetOrder(request.UserId);
						if (!order.Books.Contains(book))
						{
							order.PriceAfterDiscount += book.Price;
							order.Books.Add(book);
							_bookRepository.UpdateOrder(order);
						}
						else
						{
							throw new InvalidOperationException();
						}
					}
					book.AmountInStock -= 1;
					_bookRepository.UpdateBook(book);
					response.CreateViewBookViewModel.Id = book.Id;
					response.CreateViewBookViewModel.Title = book.Title;
					response.CreateViewBookViewModel.Price = book.Price;
					response.CreateViewBookViewModel.AmountInStock = book.AmountInStock;
					response.TotalPrice = order.PriceAfterDiscount;
					response.Success = true;
				}
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

		public RemoveBookFromShoppingCartResponse RemoveBookFromShoppingCart(RemoveBookFromShoppingCartRequest request)
		{
			RemoveBookFromShoppingCartResponse response = new RemoveBookFromShoppingCartResponse();
			_bookRepository = (BookRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.BookRepository);
			try
			{
				var order = _bookRepository.GetOrderByUserId(request.UserId);
				var book = _bookRepository.GetBook(request.BookId);
				order.PriceAfterDiscount -= book.Price;
				order.Books.Remove(book);
				_bookRepository.RemoveBookFromShoppingCart(order);
				book.AmountInStock += 1;
				_bookRepository.UpdateBook(book);
                response.CreateViewBookViewModel.Id = book.Id;
                response.CreateViewBookViewModel.AmountInStock = book.AmountInStock;
                response.TotalPrice = order.PriceAfterDiscount;
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

		public GetOrderResponse GetOrder(int userId)
		{
			GetOrderResponse response = new GetOrderResponse();
			_bookRepository = (BookRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.BookRepository);
			try
			{
                
				var order = _bookRepository.GetOrderByUserId(userId);
				response.PurchaseOrderViewModel.OrderId = order.Id;
				response.PurchaseOrderViewModel.PaymentType = order.PaymentType;
                var userPoints = _bookRepository.GetUserPoints(userId);
				response.PurchaseOrderViewModel.UserPoints = userPoints;
				response.PurchaseOrderViewModel.DateAdded = order.DateAdded;
                var afterPointsNoDiscount = 0;
                response.PurchaseOrderViewModel.TotalPrice = OrderDiscountFactory.GetDiscountAndRemainingLoyaltyPoints((int)DiscountType.NoDiscount).GetTotalPrice(order.Books.ToList(), userId, userPoints, out afterPointsNoDiscount);
                response.PurchaseOrderViewModel.AfterPointsNoDiscount = afterPointsNoDiscount;
                var afterPointsFreeBook = 0;
                response.PurchaseOrderViewModel.FreeBookPrice = OrderDiscountFactory.GetDiscountAndRemainingLoyaltyPoints((int)DiscountType.FreeBook).GetTotalPrice(order.Books.ToList(), userId, userPoints, out afterPointsFreeBook);
                response.PurchaseOrderViewModel.AfterPointsFreeBook = afterPointsFreeBook;
                var afterPointsDiscount = 0;
                response.PurchaseOrderViewModel.DiscountPrice = OrderDiscountFactory.GetDiscountAndRemainingLoyaltyPoints((int)DiscountType.PriceDiscount).GetTotalPrice(order.Books.ToList(), userId, userPoints, out afterPointsDiscount);
                response.PurchaseOrderViewModel.AfterPointsDiscount = afterPointsDiscount;
                foreach (var book in order.Books)
				{
					CreateViewBookViewModel model = new CreateViewBookViewModel
					{
						Title = book.Title,
						Name = book.AuthorFirstName + " " + book.AuthorLastName,
						Category = book.Category,
						Price = book.Price,
						ISBN = book.ISBN,
						AmountInStock = book.AmountInStock
					};
					MemoryStream stream = new MemoryStream(book.CoverImage);
					BinaryReader reader = new BinaryReader(stream);
					
					byte[] image = reader.ReadBytes((int)stream.Length);
					reader.Close();
					stream.Close();
					model.CoverImage = image;
					response.PurchaseOrderViewModel.ShoppingCartViewModel.Books.Add(model);
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
	}
}