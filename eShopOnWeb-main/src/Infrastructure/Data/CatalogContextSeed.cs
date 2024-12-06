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
        new("Patagonia"),
        new("Carhartt"),
        new("Columbia"),
        new("Under Armour")
    };
    }

    static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogType>
            {
                new("Pants"),
                new("Shirts"),
                new("Hats"),
                new("Socks")
            };
    }

    static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>
    {
        new(1, 1, "Patagonia Comfortable slim-fit pants", "Patagonia Slim Pants", 49.99m, "https://www.patagonia.ca/dw/image/v2/BDJB_PRD/on/demandware.static/-/Sites-patagonia-master/default/dwf6f754e0/images/hi-res/82960_TRBN.jpg?sw=768&sh=768&sfrm=png&q=95&bgcolor=f5f5f5"),
        new(1, 2, "Carhartt Relaxed-fit casual pants", "Carhartt Casual Pants", 54.99m, "https://m.media-amazon.com/images/I/51Xobp1R+GL._AC_UY1000_.jpg"),
        new(1, 3, "Columbia Water-resistant hiking pants", "Columbia Hiking Pants", 69.99m, "https://columbia.scene7.com/is/image/ColumbiaSportswear2/1531481_464_f?wid=768&hei=806&v=1732784020"),
        new(2, 4, "Under Armour Soft cotton T-shirt", "Under Armour Basic T-Shirt", 19.99m, "https://underarmour.scene7.com/is/image/Underarmour/V5-1388408-410_FC?rp=standard-0pad%7CpdpMainDesktop&scl=1&fmt=jpg&qlt=85&resMode=sharp2&cache=on%2Con&bgc=F0F0F0&wid=566&hei=708&size=566%2C708"),
        new(2, 1, "Patagonia Graphic printed T-shirt", "Patagonia Graphic T-Shirt", 24.99m, "https://www.patagonia.ca/dw/image/v2/BDJB_PRD/on/demandware.static/-/Sites-patagonia-master/default/dw28c44952/images/hi-res/45235_UFSX.jpg?sw=768&sh=768&sfrm=png&q=95&bgcolor=f5f5f5"),
        new(2, 2, "Carhartt V-neck T-shirt for casual wear", "Carhartt V-Neck T-Shirt", 22.99m, "https://m.media-amazon.com/images/I/71BSKrvoDfL._AC_UY1000_.jpg"),
        new(2, 3, "Columbia Long sleeve T-shirt for cool weather", "Columbia Long Sleeve T-Shirt", 29.99m, "https://columbia.scene7.com/is/image/ColumbiaSportswear2/2013431_414_f_om?wid=768&hei=806&v=1732784020"),
        new(3, 4, "Under Armour Classic baseball cap", "Under Armour Baseball Hat", 15.99m, "https://i8.amplience.net/t/jpl/jd_product_list?plu=jd_527168_al&qlt=85&qlt=92&w=320&h=320&v=1&fmt=auto"),
        new(3, 1, "Patagonia Knitted winter beanie", "Patagonia Winter Beanie", 18.99m, "https://www.patagonia.ca/dw/image/v2/BDJB_PRD/on/demandware.static/-/Sites-patagonia-master/default/dw7e69b343/images/hi-res/29187_SKBR.jpg?sw=768&sh=768&sfrm=png&q=95&bgcolor=f5f5f5"),
        new(3, 2, "Carhartt Wide-brim sun hat", "Carhartt Sun Hat", 25.99m, "https://m.media-amazon.com/images/I/51T6akK+IhL._AC_UY1000_.jpg"),
        new(3, 3, "Columbia Bucket hat for casual style", "Columbia Bucket Hat", 21.99m, "https://columbia.scene7.com/is/image/ColumbiaSportswear2/2032081_608_f_tt?wid=768&hei=806&v=1732784020"),
        new(4, 4, "Under Armour Cotton ankle socks (3-pack)", "Under Armour Ankle Socks", 9.99m, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT1-nJU4BH8WxSaOTjaCScP8Dw13WrjdxPmsQ&s"),
        new(4, 1, "Patagonia Wool thermal socks", "Patagonia Thermal Socks", 14.99m, "https://www.patagonia.ca/dw/image/v2/BDJB_PRD/on/demandware.static/-/Sites-patagonia-master/default/dwd85a43f5/images/hi-res/50151_BCW.jpg?sw=768&sh=768&sfrm=png&q=95&bgcolor=f5f5f5"),
        new(4, 2, "Carhartt Sports crew socks", "Carhartt Crew Socks", 12.99m, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQQHvu2kVcOhRoqZDXaqHT0GODt1dGfefLq7Q&s"),
        new(4, 3, "Columbia No-show socks for casual wear", "Columbia No-Show Socks", 10.99m, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTXh7qTGV7hcRt9mdbjvjx1Ch_u9LRiGGMuPA&s"),
        new(1, 1, "Patagonia Chino pants with a tapered fit", "Patagonia Chino Pants", 59.99m, "https://www.patagonia.ca/dw/image/v2/BDJB_PRD/on/demandware.static/-/Sites-patagonia-master/default/dw1ee3df89/images/hi-res/22120_COI.jpg?sw=768&sh=768&sfrm=png&q=95&bgcolor=f5f5f5"),
        new(1, 2, "Carhartt Joggers with elastic cuffs", "Carhartt Joggers", 44.99m, "https://apim.marks.com/v1/product/api/v1/product/image/79121009f?baseStoreId=MKS&lang=en_CA&subscription-key=c01ef3612328420c9f5cd9277e815a0e&imwidth=640&impolicy=mZoom"),
        new(2, 4, "Under Armour Crewneck T-shirt with a soft feel", "Under Armour Crewneck T-Shirt", 21.99m, "https://underarmour.scene7.com/is/image/Underarmour/V5-1381688-110_FC?rp=standard-0pad%7CpdpMainDesktop&scl=1&fmt=jpg&qlt=85&resMode=sharp2&cache=on%2Con&bgc=F0F0F0&wid=566&hei=708&size=566%2C708"),
        new(3, 1, "Patagonia Stylish fedora hat", "Patagonia Fedora Hat", 34.99m, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_yvqMKK12mgf-6xPOqXxsPe_E4n0pqB5P1g&s"),
        new(4, 2, "Carhartt Compression socks for support", "Carhartt Compression Socks", 19.99m, "https://s0.metrouniforms.com/images/P/A677Navy.jpg"),
        new(3, 3, "Columbia Fleece-lined trapper hat", "Columbia Trapper Hat", 29.99m, "https://columbia.scene7.com/is/image/ColumbiaSportswear2/2093451_010_f_pu?wid=768&hei=806&v=1732784020"),
        new(1, 4, "Under Armour Cargo pants with multiple pockets", "Under Armour Cargo Pants", 64.99m, "https://underarmour.scene7.com/is/image/Underarmour/V5-1379199-001_FC?rp=standard-0pad%7CpdpMainDesktop&scl=1&fmt=jpg&qlt=85&resMode=sharp2&cache=on%2Con&bgc=F0F0F0&wid=566&hei=708&size=566%2C708"),
        new(2, 1, "Patagonia Henley T-shirt with buttons", "Patagonia Henley T-Shirt", 26.99m, "https://www.patagonia.ca/dw/image/v2/BDJB_PRD/on/demandware.static/-/Sites-patagonia-master/default/dwa286f831/images/hi-res/53115_BWX.jpg?sw=768&sh=768&sfrm=png&q=95&bgcolor=f5f5f5"),
        new(1, 2, "Carhartt Formal dress pants", "Carhartt Dress Pants", 74.99m, "https://catalog-resize-images.thedoublef.com/cabd41fc6ac03d50e482fb4ea284f57f/900/900/I031497CO_P_CARH-8902.a.jpg"),
        new(2, 3, "Columbia Muscle fit T-shirt", "Columbia Muscle Fit T-Shirt", 20.99m, "https://columbia.scene7.com/is/image/ColumbiaSportswear2/1990751_010_f_om?wid=768&hei=806&v=1732784020"),
        new(3, 4, "Under Armour Classic top hat for formal occasions", "Under Armour Top Hat", 49.99m, "https://underarmour.scene7.com/is/image/Underarmour/1361544-005_SLF_SL?rp=standard-0pad%7CpdpMainDesktop&scl=1&fmt=jpg&qlt=85&resMode=sharp2&cache=on%2Con&bgc=F0F0F0&wid=566&hei=708&size=566%2C708"),
        new(4, 1, "Patagonia Knee-high socks for boots", "Patagonia Knee-High Socks", 13.99m, "https://www.patagonia.ca/dw/image/v2/BDJB_PRD/on/demandware.static/-/Sites-patagonia-master/default/dwc8c8d240/images/hi-res/50116_PSBU.jpg?sw=768&sh=768&sfrm=png&q=95&bgcolor=f5f5f5"),
        new(4, 2, "Carhartt Anti-blister running socks", "Carhartt Running Socks", 11.99m, "https://images-na.ssl-images-amazon.com/images/I/81UHLmq7-SL._AC_SR462,693_.jpg"),
    };
    }
}
