using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Orders.Interface
{
    public interface IOrderProvider
    {
        public Task<(bool Success, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync();
        public Task<(bool Success, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetCustomerOrderAsync(int CustomerId);

    }
}
