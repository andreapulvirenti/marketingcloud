using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alpitour.Integration.MarketingCloud.Model.Authentication;
using Newtonsoft.Json;

namespace Alpitour.Integration.MarketingCloud.Service
{
    public class AuthService : BaseService
    {
        public async Task<AuthResponse> AuthToken(AuthRequest request)
        {
            var ser = JsonConvert.SerializeObject(request);
            HttpResponseMessage response = await client.PostAsync("v2/token",
                new StringContent(ser, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthResponse>(apiResponse);
        }
    }
}
