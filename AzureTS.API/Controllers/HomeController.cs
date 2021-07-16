using AzureTS.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AzureTS.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        [Route("api/[controller]/FetchAzureData")]
        public IActionResult GetAll(string tableName, string? name, string? dateTime)
        {
            var operationService = new DataOperationService(tableName);

            var data = operationService.GetAll(name, dateTime);
            return Ok(data);
        }
    }
}
