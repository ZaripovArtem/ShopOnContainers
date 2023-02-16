using Customer.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerContext db;

    public CustomerController(CustomerContext customerContext)
    {
        db = customerContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerData>>> GetCustomersAsync()
    {
        return await db.Customers.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerData>> GetCustomerByIdAsync(int id)
    {
        var customer = await db.Customers.FindAsync(id);
        return customer;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomerAsync([FromBody] CustomerData customer)
    {
        await db.Customers.AddAsync(customer);
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCustomerAsync([FromBody] CustomerData customer)
    {
        db.Customers.Update(customer);
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCustomerAsync(int id)
    {
        var customer = await db.Customers.FindAsync(id);

        if(customer == null)
        {
            return NotFound();
        }

        db.Customers.Remove(customer);
        await db.SaveChangesAsync();
        return Ok();
    }
}
