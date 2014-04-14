using BookStore.Services;
using BookStore.Services.Enums;
using BookStore.Services.Messages.Order.Request;
using BookStore.Services.Messages.Order.Response;
using BookStore.Services.Services;
using BookStore.ViewModel.OrderModel;
using System;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        private OrderService _orderService;

        public OrderController()
        {
            _orderService = (OrderService)ServiceFactory.GetInstance((int)ServiceTypes.OrderService);
        }

        [HttpPost]
        public ActionResult AddBookToShoppingCart(int id)
        {
            if (Session["UserId"] != null)
            {
                PurchaseAddBookToShoppingCartRequest request = new PurchaseAddBookToShoppingCartRequest
                {
                    BookId = id,
                    UserId = (int)Session["UserId"],
                    DateAdded = DateTime.Now
                };
                AddBookToShoppingCartResponse response = _orderService.AddBookToShoppingCart(request);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPut]
        public ActionResult RemoveFromShoppingCart(int id)
        {
            if (Session["UserId"] != null)
            {
                RemoveBookFromShoppingCartRequest request = new RemoveBookFromShoppingCartRequest
                {
                    BookId = id,
                    UserId = (int)Session["UserId"]
                };
                RemoveBookFromShoppingCartResponse response = _orderService.RemoveBookFromShoppingCart(request);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult BuyBooks()
        {
            if (Session["UserId"] != null)
            {
                GetOrderResponse response = _orderService.GetOrder((int)Session["UserId"]);
                if (response.Success)
                {
                    return View("Checkout", response.PurchaseOrderViewModel);
                }
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public ActionResult BuyBooks(PurchaseOrderViewModel model)
        {
            if (Session["UserId"] != null)
            {
                PurchaseAddBookToShoppingCartRequest request = new PurchaseAddBookToShoppingCartRequest
                {
                    UserId = (int)Session["UserId"],
                    DateAdded = DateTime.Now,
                    PaymentType = model.PaymentType,
                    UserPoints = model.UserPoints
                };
                PurchaseBooksResponse response = _orderService.PurchaseBooks(request);
                return View();
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult GetMyOrders()
        {
            if (Session["UserId"] != null)
            {
                CancelOrderResponse response = _orderService.GetUserOrders((int)Session["UserId"]);
                if (response.Success)
                {
                    return View("MyOrders", response.MyOrdersViewModel);
                }
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public ActionResult CancelOrder(int orderId)
        {
            if (Session["UserId"] != null)
            {
                CancelOrderRequest reqeust = new CancelOrderRequest
                {
                    UserId = (int)Session["UserId"],
                    OrderId = orderId
                };
                CancelOrderResponse response = _orderService.CancelOrder(reqeust);
                if (response.Success)
                {
                    return RedirectToAction("GetMyOrders");
                }
            }
            return RedirectToAction("Index", "User");
        }
    }
}
