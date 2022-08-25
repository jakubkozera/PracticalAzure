using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbMongo.Controllers
{
    public record Product(string Id, string Category, string Name, int Quantity, bool Sale);

    [ApiController]
    [Route("[controller]")]
    public class CosmosMongoController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            var products = GetCollection();

            products.InsertOne(product);

            return Accepted();
        }

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var products = GetCollection();

            var product = (await products.FindAsync(p => p.Id == id)).First();

            return Ok(product);
        }

        [HttpGet("query-linq")]
        public async Task<IActionResult> GetByQueryLinq()
        {
            var products = GetCollection();

            var result = products.AsQueryable()
                .Where(p => p.Category == "sport")
                .OrderByDescending(p => p.Id)
                .ToList();

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var products = GetCollection();

            await products.DeleteOneAsync(p => p.Id == id);

            return NoContent();
        }

        private IMongoCollection<Product> GetCollection()
        {
            var connectionString = "mongodb://sample1cdmongo:BsdA6UFXXnSXjJiigjB8HPj3skxi6Xv1ZqAgia6MKCPYlI0vTZfNzRni62TGoME89MDqqqXU0bU7OJltUr7raQ==@sample1cdmongo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@sample1cdmongo@";

            MongoClient mongoClient = new MongoClient(connectionString);

            IMongoDatabase database = mongoClient.GetDatabase("ProductsDb");

            return database.GetCollection<Product>("products");
        }
    }
}