using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Work.Api.Models;
using Work.Api.Models.RequestModel;
using Work.Infrastructure.Helpers;
using Work.Infrastructure.RepositoryInterfaces;
using Work.Infrastructure.ServicesInterface;
using Work.Api.Helpers;
using Work.Core;
using Work.Infrastructure.BindingModel;
 
namespace Work.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;
        private readonly ITokenService _tokenService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEasyCachingProvider _easyCaching;
        private readonly IEasyCachingProviderFactory _cachingProviderFactory;
        private readonly ICachingService _cachingService;


        public AuthController(IUserRepository userRepository, ICryptoService cryptoService, ITokenService tokenService, IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork, IEasyCachingProviderFactory cachingProviderFactory, ICachingService cachingService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _tokenService = tokenService;
            _appSettings = appSettings;
            _unitOfWork = unitOfWork;
            _cachingProviderFactory = cachingProviderFactory;
            _cachingService = cachingService;
            _easyCaching = cachingProviderFactory.GetCachingProvider("redis_naim"); ;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequestModel model)
        {
            var result = new BaseResponseModel();
            if (!ModelState.IsValid)
            {
                result.IsError = true;
                result.ErrorDetail = ModelState.FillModelStateError();
                return Ok(result);
            }

            var user = _userRepository.LoginControl(model.Key, model.Password, _cryptoService);
            if (user == null)
            {
                result.IsError = true;
                result.ErrorDetail = "Login error (invalid Password,Mail or Username)";
                return Ok(result);
            }

            result.Data = new
            {
                Token = _tokenService.GenerateToken(user, _appSettings)
            };
            return Ok(result);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterRequestModel model)
        {
            var result = new BaseResponseModel();
            if (!ModelState.IsValid)
            {
                result.IsError = true;
                result.ErrorDetail = ModelState.FillModelStateError();
                return Ok(result);
            }

            var salt = new Random().Next(10000000, 99999999).ToString();
            var usr = new User
            {
                Email = model.Email,
                Name = model.Name,
                UserName = model.UserName,
                Phone = model.Phone,
                Website = model.Website,
                PasswordHash = _cryptoService.Encrypt(model.Password, salt),
                PasswordSalt = salt

            };
            _userRepository.Insert(usr);


            _unitOfWork.SaveChanges();
            _cachingService.SetUser(new UserCaching
            {
                Id = usr.Id,
                Email = usr.Email,
                Name = usr.Name,
                Phone = usr.Name,
                UserName = usr.UserName,
                Website = usr.Website
            },_easyCaching );
            _cachingService.GetUser(usr.Id, _easyCaching);
            return Ok(usr);
        }
    }


}
