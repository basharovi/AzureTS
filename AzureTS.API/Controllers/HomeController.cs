using AzureTS.API.Additonal;
using AzureTS.API.Models;
using AzureTS.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IEnumerable<SoloEntity> GetAll() => _dataOperationService.GetAll();

        [HttpGet]
        [Route("api/[controller]/GetFiltered")]
        public dynamic GetFiltered(string name) => _dataOperationService.GetFiltered(name);
    }
}
