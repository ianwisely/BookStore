using BookStore.Models;
using BookStore.Repository.Repositories;
using BookStore.Services.Messages.Order.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Services.Interfaces
{
    public interface IOrderState
    {
        bool CanCancel(Order order);
        CancelOrderResponse CancelOrder(Order order, BookRepository repo);
    }
}