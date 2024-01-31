using CustomerAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCustomers() 
        {
            var customers = new List<Customer>
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
            };
            return Ok(customers);
        }
    }
}
