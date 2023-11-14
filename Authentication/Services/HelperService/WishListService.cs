using Azure;

namespace Authentication.Services.HelperService
{
    public class WishListService
    {
        private readonly HttpClient _httpClient;

        public WishListService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCart(int userId)
        {
            var response = await _httpClient.GetAsync($"/api/products/{userId}");
            response.EnsureSuccessStatusCode();
        }
        public async Task CreateWishList(int userId)
        {
            var response = await _httpClient.GetAsync($"/api/products/{userId}");
            response.EnsureSuccessStatusCode();
        }
        //public async Task<Product> GetProductAsync(int id)
        //{
        //    var response = await _httpClient.GetAsync($"/api/products/{id}");

        //    response.EnsureSuccessStatusCode();

        //    var product = await response.Content.ReadFromJsonAsync<Product>();

        //    return product;
        //}
    }
}
