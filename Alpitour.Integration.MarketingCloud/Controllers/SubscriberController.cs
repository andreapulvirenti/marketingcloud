using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpitour.Integration.MarketingCloud.Model.Settings;
using Alpitour.Integration.MarketingCloud.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alpitour.Integration.MarketingCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ILogger<SubscriberController> _logger;
        private readonly Settings _settings;

        public SubscriberController(IOptions<Settings> markCloudSettings)
        {
            _settings = markCloudSettings.Value;
        }

        [HttpGet]
        public async Task<string> Get(string email)
        {
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
                    Email = email,
                    Access_Token = access_tokenObj.access_token,
                    URI = _settings.SubscribeURI
                }
                );
            _logger.LogInformation($"Find Subscriber for email {email}, Sub Key: {ret}");

            return ret;

        }

    }
}