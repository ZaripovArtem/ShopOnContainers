using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.ViewModel.Pagination;

namespace WebMVC.ViewModel.CatalogViewModel;

public class IndexViewModel
{
    public IEnumerable<CatalogItem> CatalogItems { get; set; }
    //public IEnumerable<SelectListItem> Brands { get; set; }
    //public IEnumerable<SelectListItem> Types { get; set; }
    public int? BrandFilter { get; set; }
    public int? TypeFilter { get; set; }
    public PaginationInfo PaginationInfo { get; set; }
}
