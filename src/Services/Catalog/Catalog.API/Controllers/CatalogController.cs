using Catalog.API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
	private readonly CatalogContext db;
	public CatalogController(CatalogContext catalogContext)
	{
		db = catalogContext;
	}

	// GET api/[controller]/items[?pageSize=&pageIndex=]
	[HttpGet]
	[Route("items")]
	public async Task<ActionResult<PaginatedItemsViewModel<CatalogItem>>> GetItemsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
	{
		var totalItems = await db.CatalogItems
			.CountAsync();

		var itemsOnCurrentPage = await db.CatalogItems
			.OrderBy(x => x.Id)
			.Skip(pageSize*pageIndex)
			.Take(pageSize)
			.ToListAsync();

        var model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnCurrentPage);
		return Ok(model);
	}

    // GET api/[controller]/items/id
    [HttpGet]
	[Route("items/{id:int}")]
	public async Task<ActionResult<CatalogItem>> GetItemByIdAsync(int id)
	{
		if (id < 0)
		{
			return BadRequest();
		}

		var item = await db.CatalogItems.SingleOrDefaultAsync(item => item.Id == id);

		if(item != null)
		{
			return item;
		}

		return NotFound();
	}

    // GET api/[controller]/items/withname/name[?pageSize=&pageIndex=]
    [HttpGet]
    [Route("items/withname/{name:minlength(1)}")]
	public async Task<ActionResult<PaginatedItemsViewModel<CatalogItem>>> ItemsWithNameAsync(string name, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
	{
        var totalItems = await db.CatalogItems
			.Where(item => item.Name.Contains(name))
            .CountAsync();

		var itemsOnCurrentPage = await db.CatalogItems
			.Where(item => item.Name.Contains(name))
			.Skip(pageSize*pageIndex)
			.Take(pageSize)
			.ToListAsync();

		return new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnCurrentPage);
	}

	// POST api/[controller]/items
	[HttpPost]
	[Route("items")]
	public async Task<ActionResult> CreateItemAsync([FromBody] CatalogItem catalogItem)
	{
		var item = new CatalogItem
		{
			Name = catalogItem.Name,
			Description = catalogItem.Description,
			Price = catalogItem.Price,
			CatalogBrandId = catalogItem.CatalogBrandId,
			CatalogTypeId = catalogItem.CatalogTypeId
		};

		db.CatalogItems.Add(item);
		await db.SaveChangesAsync();
		return Ok();
	}

    // PUT api/[controller]/items
    [HttpPut]
    [Route("items")]
	public async Task<ActionResult> UpdateItemAsync([FromBody] CatalogItem catalogItem)
	{
		db.CatalogItems.Update(catalogItem);
		await db.SaveChangesAsync();
		return Ok(); 
	}

    // DELETE api/[controller]/items
    [HttpDelete]
	[Route("items/{id:int}")]
	public async Task<ActionResult> DeleteItemAsync(int id)
	{
		var item = await db.CatalogItems.FindAsync(id);
		
		if(item == null)
		{
			return NotFound();
		}

		db.CatalogItems.Remove(item);
		await db.SaveChangesAsync();
		return Ok();
	}

    // GET api/[controller]/brands
    [HttpGet]
	[Route("brands")]
	public async Task<ActionResult<List<CatalogBrand>>> GetBrandsAsync()
	{
		return await db.CatalogBrands.ToListAsync();
	}

	// POST api/[controller]/brands
	[HttpPost]
    [Route("brands")]
	public async Task<ActionResult> CreateBrandAsync([FromBody] CatalogBrand catalogBrand)
	{
		var brand = new CatalogBrand
		{
			Brand = catalogBrand.Brand
		};

        db.CatalogBrands.Add(brand);
		await db.SaveChangesAsync();
		return Ok();
    }

	// PUT api/[controller]/brands
	[HttpPut]
    [Route("brands")]
	public async Task<ActionResult> UpdateBrandAsync([FromBody] CatalogBrand catalogBrand)
	{
		db.CatalogBrands.Update(catalogBrand);
		await db.SaveChangesAsync();
		return Ok();
	}

	// DELETE api/[controller]/brands
	[HttpDelete]
    [Route("brands/{id:int}")]
	public async Task<ActionResult> DeleteBrandAsync(int id)
	{
        var brand = await db.CatalogBrands.FindAsync(id);

		if(brand == null)
		{
			return NotFound();
		}

		db.CatalogBrands.Remove(brand);
		await db.SaveChangesAsync();
		return Ok();
    }

    // GET api/[controller]/types
    [HttpGet]
    [Route("types")]
    public async Task<ActionResult<List<CatalogType>>> GetTypesAsync()
    {
        return await db.CatalogTypes.ToListAsync();
    }

    // POST api/[controller]/types
    [HttpPost]
    [Route("types")]
    public async Task<ActionResult> CreateTypeAsync([FromBody] CatalogType catalogType)
    {
        var type = new CatalogType
        {
            Type = catalogType.Type
        };

        db.CatalogTypes.Add(type);
        await db.SaveChangesAsync();
        return Ok();
    }

    // PUT api/[controller]/types
    [HttpPut]
    [Route("types")]
    public async Task<ActionResult> UpdateTypeAsync([FromBody] CatalogType catalogType)
    {
        db.CatalogTypes.Update(catalogType);
        await db.SaveChangesAsync();
        return Ok();
    }

    // DELETE api/[controller]/types
    [HttpDelete]
    [Route("types/{id:int}")]
    public async Task<ActionResult> DeleteTypeAsync(int id)
    {
        var type = await db.CatalogTypes.FindAsync(id);

        if (type == null)
        {
            return NotFound();
        }

        db.CatalogTypes.Remove(type);
        await db.SaveChangesAsync();
        return Ok();
    }
}
