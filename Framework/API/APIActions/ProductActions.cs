using System.Threading.Tasks;
using qa_automation_exercise__mejiabritoabraham.Framework.API.Entities;
using qa_automation_exercise__mejiabritoabraham.Shared;
using qa_automation_exercise__mejiabritoabraham.Utils;
using RestSharp;

namespace qa_automation_exercise__mejiabritoabraham.Framework.API.APIActions
{

    public class ProductActions
    {
        private readonly RestClient _client = new(Constants.ApiServerUrl);

        public async Task<RestResponse> CreateProductAsync(string productId, string title, string description = null,
            string partnerId = null)
        {
            var request = new RestRequest($"/products/{productId}", Method.Put);

            if (!string.IsNullOrEmpty(partnerId))
            {
                request.AddHeader("X-Partner-ID", partnerId);
            }

            if (description != null)
            {
                request.AddJsonBody(new { title, description });
            }
            else
            {
                request.AddJsonBody(new { title });
            }

            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> CreateRandomProductAsync(string title, string description = null, string partnerId = null)
        {
            var randomId = Randomizer.GenerateRandomProductId();
            return await CreateProductAsync(randomId, title, description, partnerId);
        }

        public async Task<RestResponse<Product>> GetProductAsync(string productId, string partnerId)
        {
            var request = new RestRequest($"/products/{productId}", Method.Get);
            request.AddHeader("X-Partner-ID", partnerId);

            return await _client.ExecuteAsync<Product>(request);
        }

        public async Task<RestResponse> GetProductRawAsync(string productId, string partnerId)
        {
            var request = new RestRequest($"/products/{productId}", Method.Get);
            request.AddHeader("X-Partner-ID", partnerId);

            return await _client.ExecuteAsync(request);
        }
    }
}
