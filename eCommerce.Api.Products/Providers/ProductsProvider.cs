using AutoMapper;
using eCommerce.Api.Products.Db;
using eCommerce.Api.Products.Interfaces;
using eCommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbcontext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbcontext, ILogger<ProductsProvider> logger,IMapper mapper)
        {            
            this.dbcontext = dbcontext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if( ! dbcontext.Products.Any<Db.Product>())
            {
                dbcontext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Description = "Wireless", Inventory = 1, Price = 50});
                dbcontext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Description = "Wireless", Inventory = 1, Price = 20 });
                dbcontext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Description = "Wireless", Inventory = 1, Price = 300 });
                dbcontext.Products.Add(new Db.Product() { Id = 4, Name = "Desk", Description = "Wooden", Inventory = 1, Price = 20 });
                dbcontext.SaveChanges();
            }
        }
        public IMapper Mapper { get; }

        public async Task<(bool Success, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {

            try
            {
                var products = await dbcontext.Products.ToListAsync();
                if(products!=null && products.Any())
                {
                   var result= mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, "All good");
                }
                return (false, null, "No Product found");
                
            }catch(Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

       
        public async Task<(bool Success, Models.Product product, string ErrorMessage)> GetProductAsync(int id)
        {

            try {

                var product = await dbcontext.Products.FirstOrDefaultAsync(p => p.Id == id);

                if(product!=null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
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
