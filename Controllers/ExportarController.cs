using Microsoft.AspNetCore.Mvc;
using SiteTransporteNovo.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


public class ExportarController : Controller
{
    private readonly AppDbContext _context;

    public ExportarController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var dados = _context.Agendamentos
            .Select(a => new
            {
                a.Hora,
                a.LocalConsulta,
                a.Nome,
                a.PrecisaAcompanhante,
                a.LocalBusca,
                a.Motivo,
                a.Telefone
            })
            .ToList();

        return View(dados);
    }
}
