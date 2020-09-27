using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ArquivoExcelRepository : IArquivoExcelRepository
    {
        private readonly ExcelContext _context;

        public ArquivoExcelRepository(ExcelContext context)
        {
            this._context = context;
        }

        public async Task<IReadOnlyList<ArquivoExcel>> GetArquivosExcel()
        {
            return await _context.ArquivoExcel.ToListAsync();
        }

        public async Task<ArquivoExcel> GetArquivoExcel(int id)
        {
            return await _context.ArquivoExcel.FindAsync(id);
        }

        public async Task Insert(ArquivoExcel arquivoExcel)
        {
            await _context.ArquivoExcel.AddAsync(arquivoExcel);
        }
    }
}