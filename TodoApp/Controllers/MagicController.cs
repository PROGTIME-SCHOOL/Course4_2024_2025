using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers
{
    public class MagicController : Controller
    {
        // GET: MagicController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Hello(string message)
        {
            return Content(message.ToUpper());
        }

        public IActionResult Square(int number)
        {
            int square = number * number;

            return Content(square.ToString()); 
        }
    }
}
