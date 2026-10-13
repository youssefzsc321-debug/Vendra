using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendra.Core.Dtos.Identity;
using Vendra.Core.Enities.Identity;

namespace Vendra.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/users")]
    [ApiController]
    public class UsersAdminController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UsersAdminController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminRegisterDto model)
        {
            var existUser = await _userManager.FindByEmailAsync(model.Email);
            if (existUser != null)
                return BadRequestResponse("This email is already in use.");

            var user = _mapper.Map<AppUser>(model);
            user.UserName = model.Email.Split('@')[0];

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequestResponse("Admin creation failed", errors);
            }

            await _userManager.AddToRoleAsync(user, "Admin");

            var userDto = _mapper.Map<UserDto>(user);
            return Created(nameof(CreateAdmin), new { email = user.Email }, userDto, "Admin created successfully!");
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return Success(usersDto, "Success!");
        }
    }
}
