using AzureTS.API.Additonal;
using AzureTS.API.Models;
using AzureTS.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [Route("api/[controller]/GetAll")]
        public IActionResult GetAll()
        {
            var data = _dataOperationService.GetAll();
            return Ok(data);
        }

        [HttpGet]
        [Route("api/[controller]/GetFiltered")]
        public dynamic GetFiltered(string name) => _dataOperationService.GetFiltered(name);
    }
}
