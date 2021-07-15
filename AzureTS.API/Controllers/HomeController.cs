using AzureTS.API.Additonal;
using AzureTS.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureTS.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly DataOperationService _dataOperationService;

        public HomeController()
        {
            _dataOperationService = new DataOperationService(Constants.TableName);
        }

        [HttpGet]
        [Route("api/[controller]/FetchAzureData")]
        public IActionResult GetAll(string? name)
        {
            var data = _dataOperationService.GetAll(name);
            return Ok(data);
        }
    }
}
