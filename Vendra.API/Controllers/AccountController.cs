using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Vendra.API.Extentions;
using Vendra.Core.Dtos.Identity;
using Vendra.Core.Enities.Identity;
using Vendra.Infrastructure.IdentityServices.Interfaces;

namespace Vendra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return UnauthorizedResponse();
            var res = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!res.Succeeded) return UnauthorizedResponse();
            var userDto = new UserDto()
            {
                Email = user.Email,
                FullName = user.FullName,
                Token = await _tokenService.CreateToken(user, _userManager)
            };
            return Success(userDto);

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var existUser = await _userManager.FindByEmailAsync(model.Email);
            if (existUser != null) return BadRequestResponse("This email is already in use");
            var user = _mapper.Map<AppUser>(model);
            user.UserName = model.Email.Split('@')[0];
            var res = await _userManager.CreateAsync(user, model.Password);
            if (!res.Succeeded)
            {
                var errors = res.Errors.Select(e => e.Description).ToList();
                return BadRequestResponse("User creation Failed", errors);
            }
            var userDto = new UserDto()
            {
                Email = user.Email,
                FullName = user.FullName,
                Token = await _tokenService.CreateToken(user, _userManager)
            };

            return Success(userDto, "User registered successfully");
        }

        [HttpGet("GetCurrnetUser")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(email);
            var userDto = new UserDto()
            {
                Email = user.Email,
                FullName = user.FullName,
                Token = await _tokenService.CreateToken(user, _userManager)
            };

            return Success(userDto);
        }

        [HttpGet("Get-Address")]
        [Authorize]
        public async Task<IActionResult> GetAddress()
        {
            var user = await _userManager.FindUserWithAddressByEmail(User);
            var addressDto = _mapper.Map<AddressDto>(user.Address);
            return Success(addressDto);
        }

        [HttpPut("Update-Address")]
        [Authorize]
        public async Task<IActionResult> UpdateAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindUserWithAddressByEmail(User);
            if (user.Address is null)
                user.Address = _mapper.Map<Address>(addressDto);
            else
                user.Address = _mapper.Map(addressDto, user.Address);
            var res = await _userManager.UpdateAsync(user);

            if (!res.Succeeded)
            {
                var errors = res.Errors.Select(e => e.Description).ToList();
                return BadRequestResponse("Address modification Failed", errors);
            }
            var AddressDto = _mapper.Map<AddressDto>(user.Address);
            return Success(AddressDto);
        }

        [HttpGet("EmailExist")]
        public async Task<IActionResult> EmailExist(string email)
        {
            var exist = await _userManager.FindByEmailAsync(email)!=null;
            return Success(exist);
        }


    }
}
