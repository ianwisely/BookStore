using BookStore.Infrastructure;
using BookStore.Services;
using BookStore.Services.Enums;
using BookStore.Services.Messages.User.Request;
using BookStore.Services.Messages.User.Response;
using BookStore.Services.Services;
using BookStore.ViewModel.BookModel;
using BookStore.ViewModel.UserModel;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /Account/

        private UserService _userService;

        public UserController()
        {
            _userService = (UserService)ServiceFactory.GetInstance((int)ServiceTypes.AccountService);
        }

        #region Account

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                LoginRequest request = new LoginRequest
                {
                    Email = model.Email,
                    Password = model.Password
                };
                LoginResponse response = _userService.Login(request);
                if (response.Success)
                {
                    UserProfile.GetIsUserAdmin(model.Email);
                    Session.Add("UserId", response.UserId);
                    var booksModel = new ViewBooksViewModel();
                    return RedirectToAction("Index", "Book", booksModel);
                }
                else
                {
                    ViewBag.Error = "Details Incorrect";
                    return View("Index", model);
                }
            }
            return View("Index", model);
        }

        public ActionResult Logout()
        {
            Session.Remove("UserId");
            return View("Index");
        }

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                CreateUpdateUserRequest request = new CreateUpdateUserRequest
                {
                    Email = model.Email,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                };
                UpdateUserResponse response = _userService.Register(request);
                return View("Index");
            }
            else
            {
                return View("Register");
            }
        }

        #endregion

        [HttpPut]
        public ActionResult UpdateUser(UpdateUserViewModel model)
        {
            if (Session["UserId"] != null)
            {
                CreateUpdateUserRequest request = new CreateUpdateUserRequest
                {
                    UpdateUserViewModel = model
                };
                UpdateUserResponse response = _userService.UpdateUser(request);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "Account");
        }
    }
}
