using System.Collections.Generic;
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
        private readonly IArquivoExcelRepository _repository;

        public ArquivoExcelController(IArquivoExcelRepository repository, IExcelService service)
        {
            this._repository = repository;
            this._service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ArquivoExcel>>> GetAllImports()
        {
            var arquivosExcel = await _repository.GetArquivosExcel();

            if (arquivosExcel == null)
            {
                return BadRequest();
            }

            return Ok(arquivosExcel);
        }

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
                    return Ok(retorno.LinhaArquivoExcel);
                }
            }
        }

    }
}