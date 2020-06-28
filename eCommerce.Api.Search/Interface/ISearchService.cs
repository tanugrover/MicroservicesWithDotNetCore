using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Interface
{
    public interface ISearchService
    {
        public Task<(bool Success, dynamic result)> GetSearchAsync(int Id);
    }
}
