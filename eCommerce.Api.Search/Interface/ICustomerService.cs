using eCommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Interface
{
    public interface ICustomerService
    {
        public Task<(bool Success, Customer Customer, string ErrorMessage)> GetCustomerAsync(int CustomerId);
    }
}
