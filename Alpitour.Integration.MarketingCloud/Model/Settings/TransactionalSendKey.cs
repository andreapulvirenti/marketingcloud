using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpitour.Integration.MarketingCloud.Model.Settings
{
    public class TransactionalSendKey
    {
        public string ConfirmRegistration { get; set; }
        public string PasswordRecovery { get; set; }
        public string NewsletterSubscription { get; set; }
        public string NewsLetterUnsubscription { get; set; }
        public string CompliantOpened { get; set; }

    }
}
