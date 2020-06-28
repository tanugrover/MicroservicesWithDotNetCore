using eCommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Interface
{
    public interface IOrderService
    {
        public  Task<(bool Success, IEnumerable<Order> Orders,string ErrorMessage)> GetOrdersAsync(int CustomerId);
    }
}
