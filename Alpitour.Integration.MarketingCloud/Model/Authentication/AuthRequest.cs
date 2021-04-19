using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpitour.Integration.MarketingCloud.Model.Authentication
{
    public class AuthRequest
    {

        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }

    }
}
