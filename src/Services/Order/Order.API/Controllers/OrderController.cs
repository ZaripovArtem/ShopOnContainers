using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Order.API.Models;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Orders> db;
        public OrderController()
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";

            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            db = database.GetCollection<Orders>("order");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrdersAsync()
        {
            return await db.Find(Builders<Orders>.Filter.Empty).ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Orders>> GetOrderByIdAsync(string Id)
        {
            var findOrder = Builders<Orders>.Filter.Eq(order => order.OrderId, Id);
            return await db.Find(findOrder).SingleOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrderAsync([FromBody] Orders order)
        {
            await db.InsertOneAsync(order);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrderAsync([FromBody] Orders order)
        {
            var findOrder = Builders<Orders>.Filter.Eq(o => o.OrderId, order.OrderId);
            await db.ReplaceOneAsync(findOrder, order);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteOrderAsync(string Id)
        {
            var findOrder = Builders<Orders>.Filter.Eq(order => order.OrderId, Id);
            await db.DeleteOneAsync(findOrder);
            return Ok();
        }
    }  
}
