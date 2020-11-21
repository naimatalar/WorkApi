using System;
using System.Collections.Generic;
using System.Text;
using EasyCaching.Core;
using Newtonsoft.Json;
using Work.Infrastructure.BindingModel;
using Work.Infrastructure.ServicesInterface;

namespace Work.Infrastructure.Services
{
    public class CachingService : ICachingService
    {


        public void SetUser(UserCaching user, IEasyCachingProvider _easyCaching)
        {
            try
            {
                var isExist = _easyCaching.Get<string>(user.Id.ToString()).HasValue;
                if (!isExist)
                {
                    var userJson = JsonConvert.SerializeObject(user);
                    _easyCaching.Set(user.Id.ToString(), userJson, TimeSpan.FromDays(100));
                }
            }
            catch
            { }



        }

        public UserCaching GetUser(Guid userId, IEasyCachingProvider _easyCaching)
        {
            try
            {
                var userJson = _easyCaching.Get<string>(userId.ToString()).Value;
                var userDeserialize = JsonConvert.DeserializeObject<UserCaching>(userJson);
                return JsonConvert.DeserializeObject<UserCaching>(userJson);
            }
            catch (Exception e)
            {

            }
            return new UserCaching();

        }

        public void DeleteUser(Guid userId, IEasyCachingProvider _easyCaching)
        {
            try
            {
                _easyCaching.Remove(userId.ToString());
            }
            catch
            { }

        }
    }
}
