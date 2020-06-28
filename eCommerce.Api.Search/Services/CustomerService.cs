using eCommerce.Api.Search.Interface;
using eCommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory httpclientfactory;
        private readonly ILogger<CustomerService> logger;

        public CustomerService(IHttpClientFactory httpclientfactory, ILogger<CustomerService> logger)
        {
            this.httpclientfactory = httpclientfactory;
            this.logger = logger;
        }
        public async Task<(bool Success,  Customer Customer, string ErrorMessage)> GetCustomerAsync(int CustomerId)
        {
            try
            {
                var client = httpclientfactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customers/{CustomerId}");
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Customer>(content, options);
                    return (true, result, "All ok");
                }

                return (false, null, "Not Found");
            }catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

    }
}
