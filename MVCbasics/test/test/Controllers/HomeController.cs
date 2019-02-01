using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkBasics.Controllers
{
    //Homecontroller = .net will look for the Home folder and index html in it
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}