using Microsoft.AspNetCore.Mvc;
using SiteTransporteNovo.Data;
using SiteTransporteNovo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace SiteTransporteNovo.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly AppDbContext _context;

        public AgendamentoController(AppDbContext context)
        {
            _context = context;
        }

        private void PopularViewBags()
        {
            ViewBag.LocaisConsulta = new List<string>
            {
                "AME Atibaia", "Atibaia", "Amparo", "AME Amparo", "Americana", "Barretos",
                "Bragança Paulista", "Husf Bragança Paulista", "Campinas", "AME Campinas", "Unicamp Campinas",
                "Jundiaí", "Hospital Regional Jundiaí", "Pedra Bela", "Pinhalzinho", "Santa Bárbara", "AME Santa Barbara",
                "São Paulo", "Socorro", "Outros"
            };

            ViewBag.LocaisBusca = new List<string>
            {
                "Araras", "Bairro Maciel", "Bairro do Ribeiro", "Boca da Mata", "Campanha",
                "Campo", "Córrego Raso", "Estiva", "Limas",
                "Paiol das Telhas", "Pedra Bela", "Pitangueiras de baixo", "Pitangueiras de cima",
                "Pitangueiras do meio", "Sertãozinho", "Vargem", "Vargem do Monjolo", "Tuncuns", "Outros"
            };

            // ViewBag.Locais dinâmico - apenas locais com agendamento
            ViewBag.Locais = _context.Agendamentos
                .Select(a => a.LocalConsulta)
                .Distinct()
                .ToList();
        }

        // GET: Agendamento
        public IActionResult Index(string buscaData, string buscaHora, string buscaLocal, string buscaNome, string buscaLocalBusca)
        {
            var agendamentos = _context.Agendamentos.AsQueryable();

            if (!string.IsNullOrEmpty(buscaData) && DateTime.TryParse(buscaData, out DateTime data))
                agendamentos = agendamentos.Where(a => a.Data.Date == data.Date);

            if (!string.IsNullOrEmpty(buscaHora))
                agendamentos = agendamentos.Where(a => a.Hora.Contains(buscaHora));

            if (!string.IsNullOrEmpty(buscaLocal))
            {
                agendamentos = agendamentos.Where(a => a.LocalConsulta.Contains(buscaLocal) || a.OutroLocalConsulta.Contains(buscaLocal));
            }

            if (!string.IsNullOrEmpty(buscaNome))
                agendamentos = agendamentos.Where(a => a.Nome.Contains(buscaNome));

            if (!string.IsNullOrEmpty(buscaLocalBusca))
            {
                agendamentos = agendamentos.Where(a => a.LocalBusca.Contains(buscaLocalBusca) || a.OutroLocalBusca.Contains(buscaLocalBusca));
            }

            ViewBag.TipoUsuario = HttpContext.Session.GetString("UsuarioTipo");
            PopularViewBags();

            return View(agendamentos.ToList());
        }

        // GET: Agendamento/ImprimirAgendamentos
        public IActionResult ImprimirAgendamentos(string buscaData, string buscaHora, string buscaLocal, string buscaNome)
        {
            var agendamentos = _context.Agendamentos.AsQueryable();

            if (!string.IsNullOrEmpty(buscaData) && DateTime.TryParse(buscaData, out DateTime data))
                agendamentos = agendamentos.Where(a => a.Data.Date == data.Date);

            if (!string.IsNullOrEmpty(buscaHora))
                agendamentos = agendamentos.Where(a => a.Hora.Contains(buscaHora));

            if (!string.IsNullOrEmpty(buscaLocal))
                agendamentos = agendamentos.Where(a => a.LocalConsulta.Contains(buscaLocal) || a.LocalBusca.Contains(buscaLocal));

            if (!string.IsNullOrEmpty(buscaNome))
                agendamentos = agendamentos.Where(a => a.Nome.Contains(buscaNome));

            return View(agendamentos.ToList());
        }

        // GET: Agendamento/CancelarAgendamento
        public IActionResult CancelarAgendamento(string buscaData, string buscaHora, string buscaLocal, string buscaNome)
        {
            var agendamentos = _context.Agendamentos.AsQueryable();

            if (!string.IsNullOrEmpty(buscaData) && DateTime.TryParse(buscaData, out DateTime data))
                agendamentos = agendamentos.Where(a => a.Data.Date == data.Date);

            if (!string.IsNullOrEmpty(buscaHora))
                agendamentos = agendamentos.Where(a => a.Hora.Contains(buscaHora));

            if (!string.IsNullOrEmpty(buscaLocal))
                agendamentos = agendamentos.Where(a => a.LocalConsulta.Contains(buscaLocal) || a.LocalBusca.Contains(buscaLocal));

            if (!string.IsNullOrEmpty(buscaNome))
                agendamentos = agendamentos.Where(a => a.Nome.Contains(buscaNome));

            ViewBag.TipoUsuario = HttpContext.Session.GetString("UsuarioTipo");
            PopularViewBags();

            return View(agendamentos.ToList());
        }

        // GET: Agendamento/Cancelar/5
        public IActionResult Cancelar(int id)
        {
            var agendamento = _context.Agendamentos.Find(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            return View(agendamento);
        }

        // POST: Agendamento/Cancelar/5
        [HttpPost, ActionName("Cancelar")]
        [ValidateAntiForgeryToken]
        public IActionResult CancelarConfirmado(int id)
        {
            var agendamento = _context.Agendamentos.Find(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            _context.Agendamentos.Remove(agendamento);
            _context.SaveChanges();

            return RedirectToAction(nameof(CancelarAgendamento));
        }

        public IActionResult Create()
        {
            PopularViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Agendamento agendamento)
        {
            agendamento.Responsavel = HttpContext.Session.GetString("UsuarioNome");

            if (ModelState.IsValid)
            {
                _context.Agendamentos.Add(agendamento);
                _context.SaveChanges();

                return RedirectToAction("Confirmacao", new { id = agendamento.Id });
            }

            PopularViewBags();
            return View(agendamento);
        }

        public IActionResult Confirmacao(int id)
        {
            var agendamento = _context.Agendamentos.Find(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            return View(agendamento);
        }

        public IActionResult Edit(int id)
        {
            var agendamento = _context.Agendamentos.Find(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            PopularViewBags();
            return View(agendamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Agendamento agendamento)
        {
            if (id != agendamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                agendamento.Responsavel = HttpContext.Session.GetString("UsuarioNome");
                _context.Update(agendamento);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            PopularViewBags();
            return View(agendamento);
        }
    }
}