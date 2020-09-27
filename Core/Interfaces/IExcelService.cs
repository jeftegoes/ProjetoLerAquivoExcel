using System.IO;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IExcelService
    {
        Task<Retorno> Insert(MemoryStream arquivo, string nomeArquivo);
    }
}