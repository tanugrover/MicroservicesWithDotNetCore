using eCommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        public Task<(bool Success, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
        public Task<(bool Success, Product product, string ErrorMessage)> GetProductAsync(int id);
    }
}
