using Alpitour.Integration.MarketingCloud.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpitour.Integration.MarketingCloud.Service
{
    public interface IAuthService
    {
        Task<Uri> AuthToken(AuthRequest request);

    }
}
