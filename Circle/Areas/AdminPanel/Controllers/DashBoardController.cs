using Microsoft.AspNetCore.Mvc;

namespace Circle.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
