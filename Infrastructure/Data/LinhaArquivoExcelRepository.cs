using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class LinhaArquivoExcelRepository : ILinhaArquivoExcelRepository
    {
        private readonly ExcelContext _context;

        public LinhaArquivoExcelRepository(ExcelContext context)
        {
            this._context = context;
        }

        public async Task<IReadOnlyList<LinhaArquivoExcel>> GetLinhasArquivoExcel(int id)
        {
            return await _context.LinhaArquivoExcel.Where(c => c.IdArquivoExcel == id).ToListAsync();
        }

        public async Task Insert(LinhaArquivoExcel linhaArquivoExcel)
        {
            await _context.LinhaArquivoExcel.AddAsync(linhaArquivoExcel);
        }
    }
}