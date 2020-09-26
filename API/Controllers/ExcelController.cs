using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ExcelController : BaseApiController
    {
        [HttpGet]
        private void GetAllImports()
        {

        }

        [HttpGet]
        private void GetImportById(int id)
        {

        }

        [HttpPost]
        public ActionResult Insert([FromForm] IFormFile arquivo)
        {
            return Ok();
        }
    }
}