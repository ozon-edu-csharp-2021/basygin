using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Ozon.Route256.MerchandiseService.HttpModels;

namespace Ozon.Route256.MerchandiseService.HttpClients
{
    public class MerchandiseServiceHttpClient
    {
        private readonly HttpClient _httpClient;

        public MerchandiseServiceHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task CreateMerchRequestAsync(RequestMerchRequestModel merchRequest, CancellationToken token)
        {
            var jsonContent = JsonContent.Create(merchRequest);
            
            using var response = await _httpClient.PostAsync("v1/api/merch", jsonContent, token);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException("Error while creating merch request");
            }
        }

        public async Task<RequestMerchModel> GetRequestMerchByIdAsync(Guid id, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"v1/api/merch/{id}", token);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException("Error while creating merch request");
            }
            
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<RequestMerchModel>(body);
        }
    }
}