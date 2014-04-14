using BookStore.Models;
using BookStore.Repository;
using BookStore.Repository.Repositories;
using BookStore.Services.Interfaces;
using BookStore.Services.Messages.Order.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Services.SubClasses.Orders
{
    public class OrderCancelState : IOrderState
    {
        private BookRepository _bookRepository;

        private static OrderCancelState _orderCancelState = new OrderCancelState();

        private OrderCancelState() { }

        public static OrderCancelState GetInstance()
        {
            return _orderCancelState;
        }

        public bool CanCancel(Order order)
        {
            return true;
        }

        public CancelOrderResponse CancelOrder(Order order, BookRepository repo)
        {
            CancelOrderResponse response = new CancelOrderResponse();
            _bookRepository = repo;
            try
            {
                foreach (var book in order.Books)
                {
                    book.AmountInStock += 1;
                    _bookRepository.UpdateBook(book);
                }
                _bookRepository.DeleteOrder(order);
            }
            catch (Exception)
            {
                response.Success = false;
            }
            return response;
        }
    }
}