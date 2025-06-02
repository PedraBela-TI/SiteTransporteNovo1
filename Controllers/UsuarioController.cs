using Microsoft.AspNetCore.Mvc;
using SiteTransporteNovo.Models;
using SiteTransporteNovo.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace SiteTransporteNovo.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string nome, string senha)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Nome == nome && u.Senha == senha);

            if (usuario != null)
            {
                HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
                HttpContext.Session.SetString("UsuarioTipo", usuario.Tipo);
                return RedirectToAction("Index", "Menu");

            }

            ViewBag.Mensagem = "Usuário ou senha inválidos.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }
    }
}

