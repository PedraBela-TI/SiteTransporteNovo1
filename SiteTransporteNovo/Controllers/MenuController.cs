using Microsoft.AspNetCore.Mvc;

namespace SiteTransporteNovo.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
