using Microsoft.EntityFrameworkCore;
using SiteTransporteNovo.Models;

namespace SiteTransporteNovo.Data
{
    public class AppDbContext : DbContext


    {
        public DbSet<Usuario> Usuarios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Agendamento> Agendamentos { get; set; }
       
    }
}
