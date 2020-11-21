using System;
using System.Collections.Generic;
using System.Text;
using EasyCaching.Core;
using Work.Infrastructure.BindingModel;

namespace Work.Infrastructure.ServicesInterface
{
    public interface ICachingService
    {
        void SetUser(UserCaching user, IEasyCachingProvider _easyCaching);
        UserCaching GetUser(Guid userId, IEasyCachingProvider _easyCaching);
        void DeleteUser(Guid userId, IEasyCachingProvider _easyCaching);
    }
}
