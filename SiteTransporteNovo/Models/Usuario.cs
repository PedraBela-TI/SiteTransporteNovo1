using System.ComponentModel.DataAnnotations;

namespace SiteTransporteNovo.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string Tipo { get; set; }  // Ex: "Admin", "Agendador"
    }
}
