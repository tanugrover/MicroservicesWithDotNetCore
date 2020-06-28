using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Api.Orders.Db;
using eCommerce.Api.Orders.Interface;
using eCommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCommerce.Api.Orders.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrderProvider> logger;
        private readonly IMapper mapper;

        public OrderProvider(OrdersDbContext dbContext, ILogger<OrderProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedOrders();
        }

        private void SeedOrders()
        {
            if(!dbContext.Orders.Any<Db.Order>())
            {
                List<Db.OrderItem> item= new List<Db.OrderItem>();
                item.Add(new Db.OrderItem() { Id = 1, ProductId = 1, Quantity = 2, price = 50 });
                dbContext.Orders.Add(new Db.Order() { Id = 1, CustomerId = 1, Items = item, OrderDate = DateTime.Now, Total = 1 });
                dbContext.SaveChanges();
            }
        }
        public async Task<(bool Success, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetCustomerOrderAsync(int CustomerId)
        {
            try
            {
                var orders = await dbContext.Orders.
                                    Where(o => o.CustomerId == CustomerId)
                                    .Include(o => o.Items)
                                    .ToListAsync();
                                    
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, "All ok");
                }
                return (false, null, "Not Founc");
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
                return (false, null, "Error");
            }
        }

        public async Task<(bool Success, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync()
        {
            try
            {
                var orders = await dbContext.Orders.ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, "All ok");
                }
                return (false, null, "Not Founc");
            }catch(Exception e)
            {
                logger.LogError(e.ToString());
                return (false, null, "Error");
            }
        }
    }
}
