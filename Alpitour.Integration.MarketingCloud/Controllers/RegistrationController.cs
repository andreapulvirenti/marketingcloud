using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpitour.Integration.MarketingCloud.Model.Registration;
using Alpitour.Integration.MarketingCloud.Service.TransactionalEmail;
using Alpitour.Integration.MarketingCloud.Model.Settings;
using Microsoft.Extensions.Options;
using Alpitour.Integration.MarketingCloud.Service;

namespace Alpitour.Integration.MarketingCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly ILogger<RegistrationController> _logger;
        private readonly Settings _settings;


        public RegistrationController(ILogger<RegistrationController> logger, IOptions<Settings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
        }


        /// <summary>
        /// worst code written ever
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RegistrationResponse> Post(RegistrationDTO registration)
        {
            _logger.LogInformation("Registration ");

            var authService = new AuthService();
            var access_tokenObj = await authService.AuthToken(new Model.Authentication.AuthRequest()
            {
                grant_type = _settings.Grant_type,
                client_id = _settings.Client_id,
                client_secret = _settings.Client_secret
            });

            var subService = new SubscriberService();
            var ret = await subService.FindSubscriber(
                new Model.Subscriber.SubscriberRequest()
                {
                    Email = registration.username,
                    Access_Token = access_tokenObj.access_token,
                    URI = _settings.SubscribeURI
                }
                );

            RegistrationService service = new RegistrationService();
            var regResponse = await service.SendRegistrationEmail(new RegistrationRequest()
            {
                definitionKey = _settings.TransactionalSendKey.ConfirmRegistration,
                recipient = new Recipient()
                {
                    contactKey = !string.IsNullOrEmpty(ret) ? ret : $"TJ_{registration.username}",
                    to = registration.username,
                    attributes = new Attributes()
                    {
                        Username = registration.username,
                        Password = registration.password
                    }
                }
            }, access_tokenObj.access_token); 

            return regResponse;
        }
    }
}
