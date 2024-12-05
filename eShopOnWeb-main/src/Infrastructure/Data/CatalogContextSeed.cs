using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Infrastructure.Data;

public class CatalogContextSeed
{
    public static async Task SeedAsync(CatalogContext catalogContext,
        ILogger logger,
        int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            if (catalogContext.Database.IsSqlServer())
            {
                catalogContext.Database.Migrate();
            }

            if (!await catalogContext.CatalogBrands.AnyAsync())
            {
                await catalogContext.CatalogBrands.AddRangeAsync(
                    GetPreconfiguredCatalogBrands());

                await catalogContext.SaveChangesAsync();
            }

            if (!await catalogContext.CatalogTypes.AnyAsync())
            {
                await catalogContext.CatalogTypes.AddRangeAsync(
                    GetPreconfiguredCatalogTypes());

                await catalogContext.SaveChangesAsync();
            }

            if (!await catalogContext.CatalogItems.AnyAsync())
            {
                await catalogContext.CatalogItems.AddRangeAsync(
                    GetPreconfiguredItems());

                await catalogContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;
            
            logger.LogError(ex.Message);
            await SeedAsync(catalogContext, logger, retryForAvailability);
            throw;
        }
    }

    static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
    {
        return new List<CatalogBrand>
            {
                new("TRUEWERK"),
                new(".NET"),
                new("Visual Studio"),
                new("SQL Server"),
                new("Other")
            };
    }

    static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogType>
            {
                new("Pants"),
                new("T-Shirt"),
                new("Hat"),
                new("Socks")
            };
    }

    static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>
            {
                new(2,2, ".NET Bot Black Sweatshirt", ".NET Bot Black Sweatshirt", 19.5M,  "http://catalogbaseurltobereplaced/images/products/1.png"),
                new(1,1, "Lightweight Work Pants", "Lightweight Work Pants", 8.50M, "http://catalogbaseurltobereplaced/images/products/2.png"),
                new(2,5, "Prism White T-Shirt", "Prism White T-Shirt", 12,  "http://catalogbaseurltobereplaced/images/products/3.png"),
                new(2,2, "Audrey", "Audrey", 12, "http://catalogbaseurltobereplaced/images/products/4.png"),
                new(2,2, "Audrey", "Audrey", 12, "http://catalogbaseurltobereplaced/images/products/4.png"),
                new(3,5, "Audrey", "Audrey", 8.5M, "http://catalogbaseurltobereplaced/images/products/5.png"),
                new(2,2, ".NET Blue Sweatshirt", ".NET Blue Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/6.png"),
                new(2,5, "Roslyn Red T-Shirt", "Roslyn Red T-Shirt",  12, "http://catalogbaseurltobereplaced/images/products/7.png"),
                new(2,5, "Kudu Purple Sweatshirt", "Kudu Purple Sweatshirt", 8.5M, "http://catalogbaseurltobereplaced/images/products/8.png"),
                new(1,1, "Workwear Pants", "Workwear Pants", 12, "http://catalogbaseurltobereplaced/images/products/9.png"),
                new(3,5, "Leisure Hat", "Leisure Hat", 12, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,5, "Western Cap", "Western Cap", 8.5M, "http://catalogbaseurltobereplaced/images/products/11.png"),
                new(2,5, "Prism White TShirt", "Prism White TShirt", 12, "http://catalogbaseurltobereplaced/images/products/12.png"),
                new(3,5, "Leisure Hat", "Leisure Hat", 11, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,5, "Leisure Hat", "Leisure Hat", 11, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,5, "Leisure Hat", "Leisure Hat", 11, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,5, "Leisure Hat", "Leisure Hat", 11, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,5, "Leisure Hat", "Leisure Hat", 11, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,5, "Leisure Hat", "Leisure Hat", 11, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,5, "Leisure Hat", "Leisure Hat", 11, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,5, "Leisure Hat", "Leisure Hat", 11, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,5, "Leisure Hat", "Leisure Hat", 11, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,5, "Leisure Hat", "Leisure Hat", 11, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,5, "Leisure Hat", "Leisure Hat", 11, "http://catalogbaseurltobereplaced/images/products/10.png"),
            };
    }
}
