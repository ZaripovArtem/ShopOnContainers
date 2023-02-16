using Microsoft.EntityFrameworkCore;
using Customer.API.Infrastructure;
using Customer.API.Controllers;

namespace Customer.UnitTests
{
    public class CustomerControllerTest
    {
        private readonly DbContextOptions<CustomerContext> _dbConnect;
        public CustomerControllerTest()
        {
            _dbConnect = new DbContextOptionsBuilder<CustomerContext>()
            .UseInMemoryDatabase(databaseName: "in-memory")
            .Options;
        }

        [Fact]
        public async Task Create_Get_And_Update()
        {
            var user = new CustomerData()
            {
                Id = 1,
                Name = "User1",
                Email = "test@gmil.com",
                MobileNumber = "123456"
            };

            var connection = new CustomerContext(_dbConnect);
            var testcontroller = new CustomerController(connection);
            await testcontroller.CreateCustomerAsync(user);
            var get = await testcontroller.GetCustomerByIdAsync(1);

            Assert.Equal(user.Id, get.Value.Id);

            string oldName = get.Value.Name;
            user.Name = "User2";

            await testcontroller.UpdateCustomerAsync(user);
            var getUpdated = await testcontroller.GetCustomerByIdAsync(1);
            Assert.Equal(user.Name, "User2");
            Assert.NotEqual(oldName, getUpdated.Value.Name);
        }

        [Fact]
        public async Task Delete()
        {
            int id = 1;
            var connection = new CustomerContext(_dbConnect);
            var testcontroller = new CustomerController(connection);
            await testcontroller.DeleteCustomerAsync(id);
            var get = await testcontroller.GetCustomerByIdAsync(1);

            Assert.Null(get.Value);
        }
    }
}