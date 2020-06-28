using AutoMapper;
using eCommerce.Api.Customers.Db;
using eCommerce.Api.Customers.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext dbContext;
        private readonly ILogger<CustomerProvider> logger;
        private readonly IMapper mapper;

        public CustomerProvider(CustomerDbContext dbContext,ILogger<CustomerProvider> logger,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedCustomers();
        }

        private void SeedCustomers()
        {
            if (!dbContext.Customers.Any<Db.Customer>())
            {
                dbContext.Customers.Add(new Customer() { Id = 1, Name = "Tanu Dhawan", Address = "Robina" });
                dbContext.SaveChanges();
            }
        }
        public async Task<(bool Success, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if(customers!=null)
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, "All ok");
                }
                return (false, null, "Not Found");
            }catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
        public async Task<(bool Success, Models.Customer customer, string ErrorMessage)> GetCustomerAsync(int Id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(cust => cust.Id == Id);
                if (customer != null)
                {
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, "All ok");
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }
    }
}
