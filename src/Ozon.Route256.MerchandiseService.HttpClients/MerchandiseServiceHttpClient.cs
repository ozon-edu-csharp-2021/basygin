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
    public class MerchandiseServiceHttpClient : IMerchandiseServiceHttpClient
    {
        private readonly HttpClient _httpClient;

        public MerchandiseServiceHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task CreateMerchRequestAsync(MerchRequestCreateModel merchRequestCreateModel, CancellationToken token)
        {
            var jsonContent = JsonContent.Create(merchRequestCreateModel);
            
            using var response = await _httpClient.PostAsync("v1/api/merch", jsonContent, token);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException("Error while creating merch request");
            }
        }

        public async Task<List<MerchRequestModel>> GetMerchRequestsByEmployeeIdAsync(long employeeId, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"v1/api/merch?employeeId={employeeId}", token);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException("Error while creating merch request");
            }
            
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<List<MerchRequestModel>>(body);
        }
    }
}