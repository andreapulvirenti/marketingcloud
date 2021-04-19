using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpitour.Integration.MarketingCloud.Model.Settings;


namespace Alpitour.Integration.MarketingCloud.Model.Settings
{
    public class Settings
    {
        public TransactionalSendKey TransactionalSendKey { get; set; }
        public string BaseUrl { get; set; }
        public string Grant_type { get; set; }
        public string Client_id { get; set; }
        public string Client_secret { get; set; }

        public string SubscribeURI { get; set; }
    }
}
