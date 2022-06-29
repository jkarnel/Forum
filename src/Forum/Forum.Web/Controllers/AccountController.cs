using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
