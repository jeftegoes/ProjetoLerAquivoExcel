using System.Collections.Generic;

namespace Core.Entities
{
    public class Retorno
    {
        public string StatusCode { get; set; }
        public List<LinhaArquivoExcel> LinhaArquivoExcel { get; set; } = new List<LinhaArquivoExcel>();
        public List<ErroArquivo> ErrosArquivo { get; set; } = new List<ErroArquivo>();
    }
}