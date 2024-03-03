using Microsoft.AspNetCore.Mvc;

namespace WebAppSocialNetwork.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
