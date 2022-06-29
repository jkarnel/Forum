using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
