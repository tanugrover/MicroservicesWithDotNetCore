using AutoMapper;
using eCommerce.Api.Products.Db;
using eCommerce.Api.Products.Profiles;
using eCommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using Xunit;

namespace Product.Tests
{
    public class ProductsTests
    {
        [Fact]
        public async void GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnAllProducts))
                .Options;
                    
            var productDbContext = new ProductsDbContext(options);
            var profile = new ProductProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var dbprovider = new ProductsProvider(productDbContext,null, mapper);

            var products = await  dbprovider.GetProductsAsync();
            Assert.True(products.Success);
            Assert.NotNull(products.Products);
          //  Assert.True(products.Products.Any());
            Assert.Contains("All", products.ErrorMessage);


        }

        [Fact]
        public async void GetProductValidProductId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductValidProductId))
                .Options;

            var productDbContext = new ProductsDbContext(options);
            var profile = new ProductProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var dbprovider = new ProductsProvider(productDbContext, null, mapper);

            var products = await dbprovider.GetProductAsync(1);
            Assert.True(products.Success);
            Assert.NotNull(products.product);
            //  Assert.True(products.Products.Any());
            Assert.Contains("All", products.ErrorMessage);


        }

        [Fact]
        public async void GetProductInValidProductId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductValidProductId))
                .Options;

            var productDbContext = new ProductsDbContext(options);
            var profile = new ProductProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(profile));
            var mapper = new Mapper(configuration);
            var dbprovider = new ProductsProvider(productDbContext, null, mapper);

            var products = await dbprovider.GetProductAsync(5);
            Assert.False(products.Success);
            Assert.Null(products.product);
            //  Assert.True(products.Products.Any());
            Assert.Contains("Not", products.ErrorMessage);


        }
    }
}

