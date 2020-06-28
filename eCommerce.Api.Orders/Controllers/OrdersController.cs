using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Api.Orders.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProvider orderProvider;

        public OrdersController(IOrderProvider orderProvider)
        {
            this.orderProvider = orderProvider;
        }
        [HttpGet]
        public  async Task<IActionResult> GetOrdersAsync()
        {

            var result = await orderProvider.GetOrdersAsync();
            if(result.Success)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }

        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetCustomerOrderAsync(int CustomerId)
        {

            var result = await orderProvider.GetCustomerOrderAsync(CustomerId);
            if (result.Success)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }

    }
}
