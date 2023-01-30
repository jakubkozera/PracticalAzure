using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedisCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisCacheController : ControllerBase
    {
        private DatabaseMock _databaseMock;
        private IDatabase _redisDb;
        private const string COUNTRIES_KEY = "countries";

        public RedisCacheController()
        {
            _databaseMock = new DatabaseMock();
            var connectionString = "projectname2redisne.redis.cache.windows.net:6380,password=Zdohncsq4NR8ffYhNULmzqqZfXXSEy2AAzCaLIbeaA=,ssl=True,abortConnect=False";
            var redis = ConnectionMultiplexer.Connect(connectionString);
            _redisDb = redis.GetDatabase();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (_redisDb.KeyExists(COUNTRIES_KEY))
            {
                RedisValue countriesValue = await _redisDb.StringGetAsync(COUNTRIES_KEY);
                var countires = JsonSerializer.Deserialize<Dictionary<string, string>>(countriesValue);
                return Ok(countires);
            }
            else
            {
                var countriesFromDb = await _databaseMock.GetAllCountriesAsync();
                var json = JsonSerializer.Serialize(countriesFromDb);
                _redisDb.StringSet(COUNTRIES_KEY, json);
                _redisDb.KeyExpire(COUNTRIES_KEY, new TimeSpan(1, 0, 0));

                return Ok(countriesFromDb);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(string code, string name)
        {
            _databaseMock.AddCountry(code, name);
            _redisDb.KeyDelete(COUNTRIES_KEY);

            return NoContent();
        }
    }
}