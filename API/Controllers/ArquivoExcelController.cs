using System.IO;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class ArquivoExcelController : BaseApiController
    {
        private readonly IExcelService _service;

        public ArquivoExcelController(IExcelService service)
        {
            this._service = service;

        }

        [HttpGet]
        private void GetAllImports()
        {

        }

        [HttpGet]
        private void GetImportById(int id)
        {

        }

        /* private async Task<RetornoAPI> ProcessaArquivo(IFormFile arquivo)
         {
             using(var memoryStream = new MemoryStream())
             {
                 await arquivo.CopyToAsync(memoryStream).ConfigureAwait(false);

                 using(var package = new ExcelPackage(memoryStream))
                 {
                     var retornoAPI = new RetornoAPI();

                     // Varre as planilhas de dentor do arquivo.
                     for (int i = 1; i <= package.Workbook.Worksheets.Count; i++)
                     {
                         var totalRows = package.Workbook.Worksheets[i].Dimension?.Rows;
                         var totalCollumns = package.Workbook.Worksheets[i].Dimension?.Columns;

                         // Começa sempre da 2ª linha, isso é nescessário para desconsiderar o cabeçalho da planilha.
                         for (int j = 2; j <= totalRows.Value; j++)
                         {
                             var linhaArquivoExcel = new LinhaArquivoExcel();

                             // Percorre todas colunas da linha, e faz suas respectivas validações, no que se diz respeito
                             // a integridade dos dados das células.
                             for (int k = 1; k <= totalCollumns.Value; k++)
                             {
                                 string celula = package.Workbook.Worksheets[i].Cells[j, k].Value.ToString();

                                 switch (k)
                                 {
                                     // Coluna: Data Entrega.
                                     case 1:
                                         if (VerificaData(celula))
                                         {
                                             linhaArquivoExcel.DataEntrega = DateTime.Parse(celula);
                                         }
                                         else
                                         {
                                             retornoAPI.ErrosArquivo.Add(new ErroArquivo { Linha = j, Coluna = k, Erro = "Na célula da linha referente a data é um tipo inválido." });
                                         }
                                         break;
                                         // Coluna: Nome do Produto.
                                     case 2:
                                         linhaArquivoExcel.NomeProduto = package.Workbook.Worksheets[i].Cells[j, k].Value.ToString();
                                         break;
                                         // Coluna: Quantidade.
                                     case 3:
                                         if (VerificaInteiro(celula))
                                         {
                                             linhaArquivoExcel.Quantidade = Int32.Parse(celula);
                                         }
                                         else
                                         {
                                             retornoAPI.ErrosArquivo.Add(new ErroArquivo { Linha = j, Coluna = k, Erro = "Na célula da linha referente a quantidade é um tipo inválido." });
                                         }
                                         break;
                                     case 4:
                                         // Coluna: Valor Unitário.
                                         if (VerificaDecimal(celula))
                                         {
                                             linhaArquivoExcel.ValorUnitario = Decimal.Parse(celula);
                                         }
                                         else
                                         {
                                             retornoAPI.ErrosArquivo.Add(new ErroArquivo { Linha = j, Coluna = k, Erro = "Na célula da linha referente a valor unitário é um tipo inválido." });
                                         }
                                         break;
                                     default:
                                         break;
                                 }
                             }
                         }
                     }

                     return retornoAPI;
                 }
             }
         }*/

        [HttpPost]
        public async Task<ActionResult<Retorno>> Insert([FromForm] IFormFile arquivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (arquivo == null || arquivo.Length == 0)
            {
                return BadRequest();
            }

            using(var ms = new MemoryStream())
            {
                await arquivo.CopyToAsync(ms);

                var retorno = await _service.Insert(ms, arquivo.FileName);

                if (retorno.StatusCode == "400")
                {
                    return BadRequest(retorno.ErrosArquivo);
                }
                else
                {
                    return Ok();
                }
            }
        }

    }
}