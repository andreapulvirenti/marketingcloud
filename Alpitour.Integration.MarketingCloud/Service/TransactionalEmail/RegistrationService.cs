using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Alpitour.Integration.MarketingCloud.Model.Registration;
using Newtonsoft.Json;
using RestSharp;

namespace Alpitour.Integration.MarketingCloud.Service.TransactionalEmail
{
    public class RegistrationService : BaseService
    {

        //https://mcqtvlg3hv4lyvz2nhqb0-r5jspq.rest.marketingcloudapis.com/messaging/v1/email/messages/{{$guid}}

        public async Task<RegistrationResponse> SendRegistrationEmail(RegistrationRequest req, string access_token)
        {
            Guid guid = Guid.NewGuid();
            //var ser = JsonConvert.SerializeObject(request);

            //client.DefaultRequestHeaders.Authorization
            //             = new AuthenticationHeaderValue("Bearer", access_token);

            //HttpResponseMessage response = await client.PostAsync($"messaging/v1/email/messages/{guid}",
            //    new StringContent(ser, Encoding.UTF8, "application/json"));

            //response.EnsureSuccessStatusCode();

            //string apiResponse = await response.Content.ReadAsStringAsync();
            //return JsonConvert.DeserializeObject<RegistrationResponse>(apiResponse);

            var client = new RestClient($"https://mcqtvlg3hv4lyvz2nhqb0-r5jspq.rest.marketingcloudapis.com/messaging/v1/email/messages/{guid}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {access_token}");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(req), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<RegistrationResponse>(response.Content);
        }

    }
}
