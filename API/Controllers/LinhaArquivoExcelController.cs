using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LinhaArquivoExcelController : BaseApiController
    {
        private readonly ILinhaArquivoExcelRepository _repository;
        
        public LinhaArquivoExcelController(ILinhaArquivoExcelRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<LinhaArquivoExcel>>> GetImportById(int id)
        {
            var linhasArquivoExcel = await _repository.GetLinhasArquivoExcel(id);

            if (linhasArquivoExcel == null)
            {
                return BadRequest();
            }

            return Ok(linhasArquivoExcel);
        }
    }
}