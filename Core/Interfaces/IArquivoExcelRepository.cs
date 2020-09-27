using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IArquivoExcelRepository
    {
        Task<IReadOnlyList<ArquivoExcel>> GetArquivosExcel();
        Task<ArquivoExcel> GetArquivoExcel(int id);
        Task Insert(ArquivoExcel arquivoExcel);
    }
}