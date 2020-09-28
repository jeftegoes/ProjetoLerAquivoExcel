using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using OfficeOpenXml;

namespace Infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        private readonly IArquivoExcelRepository _arquivoExcelRepository;
        private readonly ILinhaArquivoExcelRepository _linhaArquivoExcel;
        private readonly IUnitOfWork _unitOfWork;

        public ExcelService(
            IArquivoExcelRepository arquivoExcelRepository,
            ILinhaArquivoExcelRepository linhaArquivoExcel,
            IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._linhaArquivoExcel = linhaArquivoExcel;
            this._arquivoExcelRepository = arquivoExcelRepository;
        }

        /* Verifica se é uma data válida. */
        private bool VerificaData(string data)
        {
            DateTime date;

            if (DateTime.TryParse(data, out date))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /* Verifica se é um inteiro válido. */
        private bool VerificaInteiro(string valor)
        {
            int integer;

            if (Int32.TryParse(valor, out integer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /* Verifica se é um decimal válido. */
        private bool VerificaDecimal(string valor)
        {
            Decimal dec;

            if (Decimal.TryParse(valor, out dec))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /* Método responsável por persistir os dados no banco de dados,
        se não existir nenhum erro E existir linhas para serem importadas, 
        insere no banco de dados. */
        private async Task<Retorno> Persiste(Retorno retornoAPI, string nomeArquivo)
        {
            // Se nenhuma foi linha foi válida.
            if (retornoAPI.LinhaArquivoExcel.Count <= 0)
            {
                retornoAPI.StatusCode = "400";
            }
            // Se existirem erros.
            else
            if (retornoAPI.ErrosArquivo.Count > 0)
            {
                retornoAPI.StatusCode = "400";
            }
            // Se deu tudo certo, persiste as informações.
            else
            if (retornoAPI.ErrosArquivo.Count <= 0 && retornoAPI.LinhaArquivoExcel.Count > 0)
            {
                retornoAPI.StatusCode = "200";

                // Monta o cabeçalho
                var arquivoExcel = new ArquivoExcel
                {
                    NomeArquivo = nomeArquivo,
                    DataImportacao = DateTime.Now,
                    QuantidadeTotalItens = retornoAPI.LinhaArquivoExcel.Count(),
                    ValorTotalImportado = retornoAPI.LinhaArquivoExcel.Sum(x => x.ValorUnitario),
                    MenorDataImportada = retornoAPI.LinhaArquivoExcel.Min(x => x.DataEntrega)
                };

                await _arquivoExcelRepository.Insert(arquivoExcel);

                // Atualiza a referência dos itens com o cabeçalho.
                foreach (var item in retornoAPI.LinhaArquivoExcel)
                {
                    item.ArquivoExcel = arquivoExcel;
                    await _linhaArquivoExcel.Insert(item);
                }

                // Comita a transação.
                await _unitOfWork.CompleteAsync();
            }

            return retornoAPI;
        }

        /* Método responsável pelo tratamento do arquivo excel importado,
        nesse método é feito tanto o tratamento de integridade dos dados quanto de
        validação de acordo com as regras definidas. */
        public async Task<Retorno> Insert(MemoryStream arquivo, string nomeArquivo)
        {
            using(var package = new ExcelPackage(arquivo))
            {
                var retornoAPI = new Retorno();

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
                                        if (linhaArquivoExcel.DataEntrega < DateTime.Now)
                                        {
                                            retornoAPI.ErrosArquivo.Add(new ErroArquivo(j, k, "O campo data de entrega não pode ser menor ou igual que o dia atual."));
                                        }
                                    }
                                    else
                                    {
                                        retornoAPI.ErrosArquivo.Add(new ErroArquivo(j, k, "Na célula da linha referente a data é um tipo inválido ou não foi informado."));
                                    }
                                    break;
                                    // Coluna: Nome do Produto.
                                case 2:
                                    if (!string.IsNullOrEmpty(celula))
                                    {
                                        linhaArquivoExcel.NomeProduto = celula;
                                        if (linhaArquivoExcel.NomeProduto.Length > 50)
                                        {
                                            retornoAPI.ErrosArquivo.Add(new ErroArquivo(j, k, "O campo descrição precisa ter o tamanho máximo de 50 caracteres."));
                                        }
                                    }
                                    else
                                    {
                                        retornoAPI.ErrosArquivo.Add(new ErroArquivo(j, k, "Na célula da linha referente ao nome do produto não foi informado."));
                                    }
                                    break;
                                    // Coluna: Quantidade.
                                case 3:
                                    if (VerificaInteiro(celula))
                                    {
                                        linhaArquivoExcel.Quantidade = Int32.Parse(celula);
                                        if (linhaArquivoExcel.Quantidade <= 0)
                                        {
                                            retornoAPI.ErrosArquivo.Add(new ErroArquivo(j, k, "O campo quantidade tem que ser maior do que zero."));
                                        }
                                    }
                                    else
                                    {
                                        retornoAPI.ErrosArquivo.Add(new ErroArquivo(j, k, "Na célula da linha referente a quantidade é um tipo inválido ou não foi informado."));
                                    }
                                    break;
                                case 4:
                                    // Coluna: Valor Unitário.
                                    if (VerificaDecimal(celula))
                                    {
                                        linhaArquivoExcel.ValorUnitario = Decimal.Parse(celula);
                                        if (linhaArquivoExcel.ValorUnitario <= 0)
                                        {
                                            retornoAPI.ErrosArquivo.Add(new ErroArquivo(j, k, "O campo valor unitário tem que ser maior do que zero."));
                                        }
                                    }
                                    else
                                    {
                                        retornoAPI.ErrosArquivo.Add(new ErroArquivo(j, k, "Na célula da linha referente a valor unitário é um tipo inválido ou não foi informado."));
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }

                        retornoAPI.LinhaArquivoExcel.Add(linhaArquivoExcel);
                    }
                }

                return await Persiste(retornoAPI, nomeArquivo);
            }
        }

    }
}