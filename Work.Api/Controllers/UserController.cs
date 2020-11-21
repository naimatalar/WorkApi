using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work.Api.Helpers;
using Work.Api.Models;
using Work.Api.Models.RequestModel;
using Work.Api.Services;
using Work.Core;
using Work.Infrastructure.BindingModel;
using Work.Infrastructure.RepositoryInterfaces;
using Work.Infrastructure.ServicesInterface;

namespace Work.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEasyCachingProvider _easyCaching;
        private readonly IEasyCachingProviderFactory _cachingProviderFactory;
        private readonly ICachingService _cachingService;

        public UserController(IUserRepository userRepository, IUnitOfWork unitOfWork, IEasyCachingProviderFactory cachingProviderFactory, ICachingService cachingService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _cachingProviderFactory = cachingProviderFactory;
            _cachingService = cachingService;
            _easyCaching = cachingProviderFactory.GetCachingProvider("redis_naim"); ;
        }

        [HttpPost]
        [Route("userUpdate")]
        public IActionResult UserUpdate([FromBody] UserUpdateRequestModel model)
        {
            var data= User.Identity.UserMailAdress();
            var result = new BaseResponseModel();
            if (!ModelState.IsValid)
            {
                result.IsError = true;
                result.ErrorDetail = ModelState.FillModelStateError();
                return Ok(result);
            }

            var user = _userRepository.GetById(model.Id);
            if (user==null)
            {
                result.IsError = true;
                result.ErrorDetail = "User not found";
                return Ok(result);
            }
            user.UserName = model.UserName;
            user.Name = model.Name;
            user.Phone = model.Phone;
            user.Website = model.Website;
            user.Email = user.Email;

            _userRepository.Update(user);
            _cachingService.SetUser(new UserCaching
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Phone = user.Name,
                UserName = user.UserName,
                Website = user.Website
            }, _easyCaching);

            _cachingService.GetUser(user.Id, _easyCaching);
            _unitOfWork.SaveChanges();
            return Ok(user);
        }

        [HttpPost]
        [Route("userDelete")]
        public IActionResult UserDelete([FromBody] IdRequestModel model)
        {
            var result = new BaseResponseModel();
            if (!ModelState.IsValid)
            {
                result.IsError = true;
                result.ErrorDetail = ModelState.FillModelStateError();
                return Ok(result);
            }
            var user = _userRepository.GetById(model.Id);
            if (user == null)
            {
                result.IsError = true;
                result.ErrorDetail = "User not found";
                return Ok(result);
            }
            _userRepository.Delete(user);
            _unitOfWork.SaveChanges();
            _cachingService.DeleteUser(user.Id,_easyCaching);
            return Ok(result);
        }


        [HttpPost]
        [Route("getUserById")]
        public IActionResult GetUserById([FromBody] IdRequestModel model)
        {
            var result = new BaseResponseModel();
            if (!ModelState.IsValid)
            {
                result.IsError = true;
                result.ErrorDetail = ModelState.FillModelStateError();
                return Ok(result);
            }
            var user = _userRepository.GetById(model.Id);
            if (user == null)
            {
                result.IsError = true;
                result.ErrorDetail = "User not found";
                return Ok(result);
            }
         
            return Ok(user);
        }

        [HttpGet]
        [Route("getAllUser")]
        public IActionResult GetAllUser()
        {
            var result = new BaseResponseModel();
            if (!ModelState.IsValid)
            {
                result.IsError = true;
                result.ErrorDetail = ModelState.FillModelStateError();
                return Ok(result);
            }

            var user = _userRepository.GetAll().ToString();
            if (user == null)
            {
                result.IsError = true;
                result.ErrorDetail = "User not found";
                return Ok(result);
            }

            return Ok(user);
        }

    }
}