using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
            var user=await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return UnauthorizedResponse();
            var res=await _signInManager.CheckPasswordSignInAsync(user, model.Password,false);
            if(!res.Succeeded) return UnauthorizedResponse();
            var userDto = new UserDto()
            {
                Email = user.Email,
                FullName = user.FullName,
                Token =await _tokenService.CreateToken(user, _userManager)
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
            var res=await _userManager.CreateAsync(user,model.Password);
            if(!res.Succeeded)
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


    }
}
