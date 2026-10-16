using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class AdminWebController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
