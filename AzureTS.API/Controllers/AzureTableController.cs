﻿using AzureTS.API.Services;
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

            var data = operationService.GetAll(name, time);
            return Ok(data);
        }

        [HttpGet]
        [Route("api/[controller]/FetchNames")]
        public IActionResult GetAll(string tableName)
        {
            var operationService = new DataOperationService(tableName);

            var data = operationService.GetAll(null, null).Select(x=> x.Name);
            return Ok(data);
        }

        [HttpGet]
        [Route("api/[controller]/FetchTableNames")]
        public IActionResult GetAll()
        {
            var operationService = new DataOperationService();

            var data = operationService.GetAllTableNames();
            return Ok(data);
        }
    }
}
