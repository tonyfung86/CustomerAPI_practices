using CustomerAPI.Data;
using CustomerAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerController(DataContext context) {
            
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers() 
        {

            var customers = await _context.Customers.ToListAsync();

            /*var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Tony",
                    Email = "tony@gmail.com",
                    Phone = "12345678",
                    Address = "123 home Ave E",
                    City = "Torondo"
                }
            };*/
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customers = await _context.Customers.FindAsync(id);
            if(customers == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);

        }

        [HttpPut]
        public async Task<ActionResult<Customer>> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!CustomerExists(id)) { return NotFound(); }
                else { throw; }
            }

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(customer => customer.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            if(_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) 
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
