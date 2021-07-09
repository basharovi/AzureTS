using AzureTS.API.Models;
using AzureTS.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AzureTS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<SoloEntity> GetAll()
        {
            var dataOperation = new DataOperationService("VTSolo");

            return dataOperation.GetAll();
        }
    }
}
