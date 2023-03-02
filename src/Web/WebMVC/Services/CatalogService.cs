using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using WebMVC.ViewModel;

namespace WebMVC.Services;

public class CatalogService : ICatalogService
{
    string urlBase = "http://host.docker.internal:8002/api/catalog/";
    private HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<Catalog> GetCatalogItem(int page, int pageIndex, int? brand, int? type)
    {
        HttpClientHandler handler = new();
        handler.ServerCertificateCustomValidationCallback = 
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        _httpClient = new HttpClient(handler); // Обход SSL сертификата

        var url = API.Catalog.GetAllCatalogItems(urlBase, pageIndex, page, brand, type);


        var jsonString = await _httpClient.GetStringAsync(url);

        var catalog = JsonSerializer.Deserialize<Catalog>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return catalog;
    }

}
