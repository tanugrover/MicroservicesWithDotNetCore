using eCommerce.Api.Search.Interface;
using eCommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;
        private readonly ILogger<SearchService> logger;

        public SearchService(IOrderService orderService, IProductService productService,ICustomerService customerService, ILogger<SearchService> logger)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
            this.logger = logger;
        }
        public async Task<(bool Success, dynamic result)> GetSearchAsync(int Id)
        {
            
            var response = await orderService.GetOrdersAsync(Id);
            var products = await productService.GetProductsAsync();
            var CustomerResult = await customerService.GetCustomerAsync(Id);
            if(response.Success)
            {
                foreach(var order in response.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = products.products.FirstOrDefault(p => p.Id == item.ProductId).Name;
                    }
                }
                var result = new
                {
                    Customer =  CustomerResult.Customer ,
                    Orders = response.Orders 
                };
                return (response.Success, result);
            }
            return (false,null);
        }
    }
}
