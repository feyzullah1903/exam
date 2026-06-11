using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Circle.Controllers
{
    public class HomeController : Controller
    {
     

        public IActionResult Index()
        {
            return View();
        }

     
    }
}
