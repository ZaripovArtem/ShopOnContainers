using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;
using WebMVC.ViewModel;
using WebMVC.ViewModel.CatalogViewModel;

namespace WebMVC.Controllers;

public class CatalogController : Controller
{
    private ICatalogService _catalogService;
    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public async Task<ActionResult> Index(int? brandFilter, int? typeFilter, int? page)
    {
        var itemsOnPage = 10;
        var catalog = await _catalogService.GetCatalogItem(page ?? 0, itemsOnPage, brandFilter, typeFilter);

        var vm = new IndexViewModel()
        {
            CatalogItems = catalog.Data, // добавить про бренды и типы
            BrandFilter = brandFilter ?? 0,
            TypeFilter = typeFilter ?? 0,
            PaginationInfo = new ViewModel.Pagination.PaginationInfo()
            {
                ActualPage = page ?? 0,
                ItemsOnPage = catalog.Data.Count,
                TotalItems = catalog.Count,
                TotalPage = (int)Math.Ceiling(((decimal)catalog.Count / itemsOnPage))
            }
        };

        vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPage - 1) ? "is-disabled" : "";
        vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

        return View(vm);
    }
}
