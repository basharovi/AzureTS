using AzureTS.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureTS.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        [Route("api/[controller]/FetchAzureData")]
        public IActionResult GetAll(string tableName, string? name)
        {
            var operationService = new DataOperationService(tableName);

            var data = operationService.GetAll(name);
            return Ok(data);
        }
    }
}
