using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using Work.Core;
using Work.Infrastructure.Helpers;

namespace Work.Infrastructure.ServicesInterface
{
    public interface ITokenService
    {
        public string GenerateToken(User user, IOptions<AppSettings> appSettings);
    }
}
