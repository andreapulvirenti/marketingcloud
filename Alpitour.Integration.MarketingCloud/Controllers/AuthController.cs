using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Alpitour.Integration.MarketingCloud.Model.Authentication;
using Alpitour.Integration.MarketingCloud.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Alpitour.Integration.MarketingCloud.Model.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Alpitour.Integration.MarketingCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<NewsletterController> _logger;
        private readonly Settings _settings;

        public AuthController(IOptions<Settings> markCloudSettings, ILogger<AuthController> logger)
        {
            _settings = markCloudSettings.Value;
        }

        [HttpGet]
        public async Task<AuthResponse> Get()
        {
            var authServ = new AuthService();
            return await authServ.AuthToken(new Model.Authentication.AuthRequest()
            {
                grant_type = _settings.Grant_type,
                client_id = _settings.Client_id,
                client_secret = _settings.Client_secret
            });
        }

    }
}