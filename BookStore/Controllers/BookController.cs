using BookStore.Services;
using BookStore.Services.Messages.Book.Response;
using BookStore.Services.Messages.Book.Request;
using BookStore.Services.Services;
using System.Web.Mvc;
using BookStore.Infrastructure;
using BookStore.Services.Enums;
using BookStore.ViewModel.BookModel;
using System.Web;
using System.IO;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        //
        // GET: /Book/

        private BookService _bookService;

        public BookController()
        {
            _bookService = (BookService)ServiceFactory.GetInstance((int)ServiceTypes.BookService);
        }

        public ActionResult Index(ViewBooksViewModel model)
        {
            model.CreateViewBookViewModel = new CreateViewBookViewModel();
            if (Session["UserId"] != null)
            {
                HomePageRequest request = new HomePageRequest()
                {
                    UserId = (int)Session["UserId"]
                };
                ViewBooksResponse response = _bookService.ViewBooks(request.UserId);

                response.CreateViewBookViewModel = model.CreateViewBookViewModel;
                return View(response.ViewBooksViewModel);
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult ViewBooks()
        {
            if (Session["UserId"] != null)
            {
                ViewBooksResponse response = _bookService.ViewBooks((int)Session["UserId"]);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult SearchBooks(string searchQuery)
        {
            if (Session["UserId"] != null)
            {
                ViewBooksResponse response = _bookService.SearchForBooks(searchQuery);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public ActionResult CreateBook(CreateViewBookViewModel model, HttpPostedFileBase file)
        {
            if (Session["UserId"] != null)
            {
                CreateUpdateBookRequest request = new CreateUpdateBookRequest
                {
                    CreateBookViewModel = model
                };
                foreach (string upload in Request.Files)
                {
                    //if (!Request.Files[upload].HasFile()) continue;

                    string mimeType = Request.Files[upload].ContentType;
                    Stream fileStream = Request.Files[upload].InputStream;
                    string fileName = Path.GetFileName(Request.Files[upload].FileName);
                    int fileLength = Request.Files[upload].ContentLength;
                    byte[] fileData = new byte[fileLength];
                    fileStream.Read(fileData, 0, fileLength);
                    request.CreateBookViewModel.CoverImage = fileData;
                }
                CreateUpdateBookResponse response = _bookService.CreateBook(request);
                if (response.Success)
                {
                    return RedirectToAction("Index", "Book", response.ViewBooksViewModel);
                }
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult UpdateBook(int id)
        {
            if (Session["UserId"] != null)
            {
                if (UserProfile.IsUserAdmin)
                {
                    GetBookResponse response = _bookService.GetBook(id);
                    if (response.Success)
                    {
                        return View("UpdateBook", response.CreateViewBookViewModel);
                    }
                }
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public ActionResult UpdateBook(CreateViewBookViewModel model)
        {
            if (Session["UserId"] != null)
            {
                if (UserProfile.IsUserAdmin)
                {
                    CreateUpdateBookRequest request = new CreateUpdateBookRequest
                    {
                        CreateBookViewModel = model
                    };
                    CreateUpdateBookResponse response = _bookService.UpdateBook(request);
                    return RedirectToAction("Index", "Book", response.ViewBooksViewModel.CreateViewBookViewModel);
                }
            }
            return RedirectToAction("Index", "User");
        }

        [HttpDelete]
        public ActionResult DeleteBook(int id)
        {
            if (Session["UserId"] != null)
            {
                if (UserProfile.IsUserAdmin)
                {
                    DeleteBookResponse response = _bookService.DeleteBook(id);
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("Index", "User");
        }
    }
}
