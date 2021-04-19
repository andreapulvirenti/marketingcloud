using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Alpitour.Integration.MarketingCloud.Service
{
    public class BaseService
    {
        protected static HttpClient client;

        public BaseService()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("https://mcqtvlg3hv4lyvz2nhqb0-r5jspq.auth.marketingcloudapis.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

    }
}
