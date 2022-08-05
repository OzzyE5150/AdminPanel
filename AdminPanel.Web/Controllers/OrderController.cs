using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
