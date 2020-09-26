using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data {
    public class ExcelContext : DbContext {
        public DbSet<ArquivoExcel> ArquivoExcel { get; set; }
        public DbSet<LinhasArquivoExcel> LinhasArquivoExcel { get; set; }

        public ExcelContext (DbContextOptions<ExcelContext> options) : base (options) {

        }
    }
}