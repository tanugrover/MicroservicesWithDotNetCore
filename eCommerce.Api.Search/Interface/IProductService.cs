using eCommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Interface
{
    public interface IProductService
    {
        public Task<(bool Success, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync();
    }
}
