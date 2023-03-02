namespace WebMVC.Services;

public static class API
{
    public static class Catalog
    {
        public static string GetAllCatalogItems(string baseUrl, int page, int pageIndex, int? brand, int? type)
        {
            var filter = "";

            if(type.HasValue)
            {
                var brandFilter = (brand.HasValue) ? brand.Value.ToString() : string.Empty;
                filter = $"/type/{type.Value}/brand/{brandFilter}";
            }
            else if (brand.HasValue)
            {
                var brandFilter = (brand.HasValue) ? brand.Value.ToString() : string.Empty;
                filter = $"/type/all/brand/{brandFilter}";
            }
            else
            {
                filter = string.Empty;
            }

            return $"{baseUrl}items{filter}?pageSize={page}&pageIndex={pageIndex}";
        }
    }
}
