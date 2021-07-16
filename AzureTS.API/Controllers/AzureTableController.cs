using AzureTS.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AzureTS.API.Controllers
{
    [ApiController]
    public class AzureTableController : ControllerBase
    {

        [HttpGet]
        [Route("api/[controller]/FetchData")]
        public IActionResult GetAll(string tableName, string? name, string? dateTime)
        {
            var operationService = new DataOperationService(tableName);

            var data = operationService.GetAll(name, dateTime).Take(10);
            return Ok(data);
        }
    }
}
