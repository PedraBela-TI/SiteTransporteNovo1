using System.ComponentModel.DataAnnotations;

namespace SiteTransporteNovo.Models
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class Agendamento
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string Hora { get; set; }

        public string LocalConsulta { get; set; }
        public string? OutroLocalConsulta { get; set; }

        public string LocalBusca { get; set; }
        public string? OutroLocalBusca { get; set; }

        [Required]
        public string PontoReferencia { get; set; }

        [Required]
        public string Motivo { get; set; }
        public string? Observacao { get; set; }

        [Required]
        public bool PrecisaAcompanhante { get; set; }

        [BindNever]
        public string? Responsavel { get; set; }

    }
}