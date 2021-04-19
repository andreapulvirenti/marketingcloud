using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpitour.Integration.MarketingCloud.Model.Registration
{
    public class Responses
    {
        public string messageKey { get; set; }
    }
    public class RegistrationResponse
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public string requestId { get; set; }
        public int errorcode { get; set; }
        public List<Responses> responses { get; set; }

    }
}
