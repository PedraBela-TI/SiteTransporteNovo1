
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;


using SiteTransporteNovo.Models; // Garanta que este namespace esteja correto para seus modelos

namespace SiteTransporteNovo.Data // <-- ESSA LINHA É CRÍTICA!
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Agendamento> Agendamentos { get; set; } // e não SeuModelo




        // Propriedades DbSet para suas entidades
        public DbSet<Usuario> Usuarios { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Configuração para a Entidade Usuario ---
            // Exemplo: Se você tem um campo ENUM para Tipo e quer mapeá-lo
            // modelBuilder.Entity<Usuario>()
            //    .Property(u => u.Tipo)
            //    .HasConversion<string>(); // Opcional: converte ENUM para string no DB

            // --- Data Seeding para a tabela Usuarios ---
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nome = "Renato",
                    // IMPORTANTE: Em produção, use senhas hashadas.
                    // Exemplo com BCrypt (você precisaria do pacote BCrypt.Net-Next):
                    // Senha = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Senha = "123456", // Apenas para teste inicial, MUDE ISSO EM PRODUÇÃO!
                    Tipo = "Administrador"
                },
                new Usuario
                {
                    Id = 2,
                    Nome = "Marilia",
                    // Senha = BCrypt.Net.BCrypt.HashPassword("1234"),
                    Senha = "1234", // Apenas para teste inicial, MUDE ISSO EM PRODUÇÃO!
                    Tipo = "Agendador"
                },
                new Usuario
                {
                    Id = 3,
                    Nome = "Grazi",
                    // Senha = BCrypt.Net.BCrypt.HashPassword("1234"),
                    Senha = "1234", // Apenas para teste inicial, MUDE ISSO EM PRODUÇÃO!
                    Tipo = "Agendador"
                },
                new Usuario
                {
                    Id = 4,
                    Nome = "Monica",
                    // Senha = BCrypt.Net.BCrypt.HashPassword("1234"),
                    Senha = "1234", // Apenas para teste inicial, MUDE ISSO EM PRODUÇÃO!
                    Tipo = "Agendador"
                },
                new Usuario
                {
                    Id = 5,
                    Nome = "Marcia",
                    // Senha = BCrypt.Net.BCrypt.HashPassword("1234"),
                    Senha = "1234", // Apenas para teste inicial, MUDE ISSO EM PRODUÇÃO!
                    Tipo = "Agendador"
                }
            );

   
        }
    }
}