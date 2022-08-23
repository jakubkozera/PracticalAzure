using AzureSqlSample.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSqlSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomController : ControllerBase
    {
        private readonly ProjectNameDbContext _dbContext;

        public CustomController(ProjectNameDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = _dbContext.Customers
                .Take(50)
                .ToList();

            return Ok(customers);
        }
    }
}