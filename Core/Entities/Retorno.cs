using System.Collections.Generic;

namespace Core.Entities
{
    /* Essa classe agrupa todas as informações nescessárias para tratar em qualquer front-end. */
    public class Retorno
    {
        public string StatusCode { get; set; }
        public List<LinhaArquivoExcel> LinhaArquivoExcel { get; set; } = new List<LinhaArquivoExcel>();
        public List<ErroArquivo> ErrosArquivo { get; set; } = new List<ErroArquivo>();
    }
}