using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Api.Search.Interface;
using eCommerce.Api.Search.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService )
        {
            this.searchService = searchService;
        }
        [HttpPost]
        public async Task<IActionResult> GetSearch(SearchTerm term)
        {
            var result = await searchService.GetSearchAsync(term.CustomerId);
            return Ok(result.result);
        }
    }
}