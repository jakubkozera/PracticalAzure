using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableStorageSample.Models;

namespace TableStorageSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableStorageController : ControllerBase
    {
        private TableClient _tableClient;

        public TableStorageController()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=projectname1sane;AccountKey=Z7BDYxJqQgFKvJPpw7NcKPpURQkOraIPpO74njujqaWgVKBrs/qP4eHgp9r4taV1l/xQk00I4pFa+AStyjfaMw==;EndpointSuffix=core.windows.net";
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);

            _tableClient = tableServiceClient.GetTableClient("employees");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            await _tableClient.CreateIfNotExistsAsync();

            await _tableClient.AddEntityAsync(employee);

            return Accepted();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string rowKey, [FromQuery] string partitionKey)
        {
            var employee = await _tableClient.GetEntityAsync<Employee>(partitionKey, rowKey);
            return Ok(employee);
        }

        [HttpGet("query")]
        public async Task<IActionResult> Query()
        {
            var employees = _tableClient.Query<Employee>(e => e.PartitionKey == "IT");
            return Ok(employees);
        }
    }
}