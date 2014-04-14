using BookStore.Models;
using BookStore.Repository.Repositories;
using BookStore.Services.Interfaces;
using BookStore.Services.Messages.Order.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Services.SubClasses.Orders
{
    public class OrderCantCancelState : IOrderState
    {
        public static OrderCantCancelState _orderCantCancelState = new OrderCantCancelState();

        private OrderCantCancelState() { }

        public static OrderCantCancelState GetInstance()
        {
            return _orderCantCancelState;
        }

        public bool CanCancel(Order purchase)
        {
            return false;
        }

        public CancelOrderResponse CancelOrder(Order purchase, BookRepository repo)
        {
            throw new InvalidOperationException();
        }
    }
}