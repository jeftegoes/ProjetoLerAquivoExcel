using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ILinhaArquivoExcelRepository
    {
        Task<IReadOnlyList<LinhaArquivoExcel>> GetLinhasArquivoExcel(int id);
        Task Insert(LinhaArquivoExcel linhaArquivoExcel);
    }
}