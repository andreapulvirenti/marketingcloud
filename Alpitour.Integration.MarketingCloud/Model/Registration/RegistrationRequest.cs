using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpitour.Integration.MarketingCloud.Model.Registration
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Attributes
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Recipient
    {
        public string contactKey { get; set; }
        public string to { get; set; }
        public Attributes attributes { get; set; }
    }

    public class RegistrationRequest
    {
        public string definitionKey { get; set; }
        public Recipient recipient { get; set; }
    }



}
