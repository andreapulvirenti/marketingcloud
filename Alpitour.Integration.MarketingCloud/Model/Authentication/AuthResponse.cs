using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpitour.Integration.MarketingCloud.Model.Authentication
{
    public class AuthResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
        public string scope_instance_url { get; set; }
        public string rest_instance_url { get; set; }


    }
}
