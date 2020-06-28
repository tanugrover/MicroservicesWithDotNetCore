using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Api.Customers.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.Api.Customers.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerProvider customerProvider;

        public CustomerController(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await customerProvider.GetCustomersAsync();
            if(result.Success)
            {
                return Ok(result.customers);
            }
            return NotFound();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var result = await customerProvider.GetCustomerAsync(id);
            if (result.Success)
            {
                return Ok(result.customer);
            }
            return NotFound();
        }

    }
}
