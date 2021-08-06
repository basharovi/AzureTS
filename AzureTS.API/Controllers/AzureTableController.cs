using AzureTS.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureTS.API.Controllers
{
    [ApiController]
    public class AzureTableController : ControllerBase
    {
        [HttpGet]
        [Route("api/[controller]/FetchData")]
        public IActionResult GetAll(string tableName, string? name, string? fromDate, string? toDate)
        {
            var operationService = new DataOperationService(tableName);

            var data = operationService.GetAll(name, fromDate, toDate);

            return Ok(data);
        }

        [HttpGet]
        [Route("api/[controller]/FetchNames")]
        public IActionResult GetAll(string tableName)
        {
            var operationService = new DataOperationService(tableName);

            var data = operationService.GetAllNames();

            return Ok(data);
        }
        
    }
}
