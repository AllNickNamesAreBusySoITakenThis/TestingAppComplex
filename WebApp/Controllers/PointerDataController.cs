using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointerDataController : Controller
    {
        private readonly ILogger<PointerDataController> _logger;

        public PointerDataController(ILogger<PointerDataController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<PointerData>> GetPoints()
        {
            try
            {
                List<PointerData> result = new List<PointerData>();
                string connectionString = "mongodb://localhost:27017";
                MongoClient client = new MongoClient(connectionString);
                IMongoDatabase database = client.GetDatabase("pointersDB");
                var collection = database.GetCollection<BsonDocument>("pointersData");
                var filter = new BsonDocument();
                var data = await collection.Find(filter).ToListAsync();
                foreach (var d in data)
                {
                    result.Add(
                        new PointerData(
                            d["_id"].ToString(),
                            d["Description"].ToString(),
                            d["North"].AsDouble,
                            d["East"].AsDouble,
                            d["Hight"].AsDouble,
                            d["Value"].AsDouble)
                        );
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<PointerData>();
            }
        }
    }
}
