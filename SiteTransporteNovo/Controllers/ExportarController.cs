using Microsoft.AspNetCore.Mvc;
using SiteTransporteNovo.Data;

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
                a.Horario,
                a.Local,
                a.Paciente,
                a.Acompanhante,
                a.LocalDeBusca,
                a.Motivo,
                a.Telefone
            })
            .ToList();

        return View(dados);
    }
}
