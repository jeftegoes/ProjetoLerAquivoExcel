using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class LinhaArquivoExcel : BaseEntity
    {
        [Required]
        public int IdArquivoExcel { get; set; }

        [ForeignKey("IdArquivoExcel")]
        public ArquivoExcel ArquivoExcel { get; set; }

        [Required]
        public DateTime DataEntrega { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeProduto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public decimal ValorUnitario { get; set; }
    }
}