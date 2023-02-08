using Catalog.API.Controllers;
using Catalog.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.UnitTests;

public class CatalogControllerTest
{
    private readonly DbContextOptions<CatalogContext> _dbConnect;
    public CatalogControllerTest()
    {
        _dbConnect = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "in-memory")
            .Options;
    }

    [Fact]
    public async Task Get_Items_By_Id()
    {
        var connection = new CatalogContext(_dbConnect);
        connection.AddRange(TestCatalog());
        await connection.SaveChangesAsync();
        var testController = new CatalogController(connection);
        var result = await testController.GetItemByIdAsync(1);

        Assert.Equal(result.Value.Id, 1);
        Assert.Equal(result.Value.Name, "TestName1");
    }

    [Fact]
    public async Task Get_All_Items()
    {
        var connection = new CatalogContext(_dbConnect);
        var testController = new CatalogController(connection);
        int pagesize = 3;
        int pageindex = 2;
        var result = await testController.GetItemsAsync(pagesize, pageindex);
        Assert.IsType<ActionResult<PaginatedItemsViewModel<CatalogItem>>>(result);
    }

    private List<CatalogItem> TestCatalog()
    {
        return new List<CatalogItem>()
        {
            new()
            {
                Id = 1,
                Name = "TestName1",
                Description = "TestDescription1",
                Price = 1000,
                CatalogBrandId = 1,
                CatalogTypeId = 1
            },
            new()
            {
                Id = 2,
                Name = "TestName2",
                Description = "TestDescription2",
                Price = 1000,
                CatalogBrandId = 1,
                CatalogTypeId = 1
            },
            new()
            {
                Id = 3,
                Name = "TestName3",
                Description = "TestDescription3",
                Price = 1000,
                CatalogBrandId = 1,
                CatalogTypeId = 1
            },
            new()
            {
                Id = 4,
                Name = "TestName4",
                Description = "TestDescription4",
                Price = 1000,
                CatalogBrandId = 1,
                CatalogTypeId = 1
            },
            new()
            {
                Id = 5,
                Name = "TestName5",
                Description = "TestDescription5",
                Price = 1000,
                CatalogBrandId = 1,
                CatalogTypeId = 1
            },
            new()
            {
                Id = 6,
                Name = "TestName6",
                Description = "TestDescription6",
                Price = 1000,
                CatalogBrandId = 1,
                CatalogTypeId = 1
            }
        };
    }
}