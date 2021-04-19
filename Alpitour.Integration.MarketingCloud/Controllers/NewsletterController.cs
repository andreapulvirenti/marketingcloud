using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Alpitour.Integration.MarketingCloud.Model.NewsLetter;
using Microsoft.Extensions.Logging;

namespace Alpitour.Integration.MarketingCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterController : ControllerBase
    {
        private readonly ILogger<NewsletterController> _logger;

        public NewsletterController(ILogger<NewsletterController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public ActionResult<NewsLetterSubscribeResponse> Subscribe()
        {
            _logger.LogInformation("NewSletter Subscribe");
            return Ok(new OkObjectResult(new NewsLetterSubscribeResponse()));
        }


        [HttpGet]
        public ActionResult<NewsLetterUnsubscribeResponse> Unsubscribe()
        {
            _logger.LogInformation("NewSletter Unsubscribe");
            return Ok(new OkObjectResult(new NewsLetterUnsubscribeResponse()));
        }
    }
}