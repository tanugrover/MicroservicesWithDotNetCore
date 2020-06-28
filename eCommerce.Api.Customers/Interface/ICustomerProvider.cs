using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Customers.Interface
{
    public interface ICustomerProvider
    {
        public Task<(bool Success, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomersAsync();
        public Task<(bool Success, Models.Customer customer, string ErrorMessage)> GetCustomerAsync(int Id);
      
    }
}
