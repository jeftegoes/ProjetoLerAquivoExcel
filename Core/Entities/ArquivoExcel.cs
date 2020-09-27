using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ArquivoExcel : BaseEntity
    {
        [Required]
        public string NomeArquivo { get; set; }

        [Required]
        public DateTime DataImportacao { get; set; }

        [Required]
        public int QuantidadeTotalItens { get; set; }

        [Required]
        public DateTime MenorDataImportada { get; set; }

        [Required]
        public decimal ValorTotalImportado { get; set; }
    }
}