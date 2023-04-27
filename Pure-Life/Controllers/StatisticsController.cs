using Microsoft.AspNetCore.Mvc;

namespace Pure_Life.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
