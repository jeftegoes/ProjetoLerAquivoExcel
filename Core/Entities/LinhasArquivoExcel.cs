using System;

namespace Core.Entities {
    public class LinhasArquivoExcel : BaseEntity {
        public DateTime DataEntrega { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}