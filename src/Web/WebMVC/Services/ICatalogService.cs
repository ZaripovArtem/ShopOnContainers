using WebMVC.ViewModel;

namespace WebMVC.Services;

public interface ICatalogService
{
    Task<Catalog> GetCatalogItem(int page, int pageIndex, int? brand, int? type);
}
