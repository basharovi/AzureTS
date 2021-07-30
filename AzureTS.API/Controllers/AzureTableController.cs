using AzureTS.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AzureTS.API.Controllers
{
    [ApiController]
    public class AzureTableController : ControllerBase
    {
        [HttpGet]
        [Route("api/[controller]/FetchData")]
        public IActionResult GetAll(string tableName, string? name, string? time)
        {
            var operationService = new DataOperationService(tableName);

            var data = operationService.GetAll(name, time).Take(10);
            return Ok(data);
        }

        [HttpGet]
        [Route("api/[controller]/FetchNames")]
        public IActionResult GetAll(string tableName)
        {
            var operationService = new DataOperationService(tableName);

            var data = operationService.GetAll(null, null).Select(x=> x.Name).Take(20).ToList();
            return Ok(data);
        }
    }
}
