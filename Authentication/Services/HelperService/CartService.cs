namespace Authentication.Services.HelperService
{
    public class CartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
