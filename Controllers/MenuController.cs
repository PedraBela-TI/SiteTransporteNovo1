using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Namespace para HttpContext.Session
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace SiteTransporteNovo.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.TipoUsuario = HttpContext.Session.GetString("UsuarioTipo");
            return View();
        }
    }
}