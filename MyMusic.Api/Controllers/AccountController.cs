using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MyMusic.Api.Models.ViewModels.IdentityVM;
using MyMusic.Api.Settings;
using MyMusic.Core.AuthEntities;

namespace MyMusic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private JwtGenerator jwtGenerator;
        public AccountController(IMapper mapper, UserManager<User> userManager,
            RoleManager<Role> roleManager,IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            jwtGenerator = new JwtGenerator();
        }
        [HttpPost("SignUp")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([FromForm] SignUpViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                var user = _mapper.Map<SignUpViewModel, User>(model);
                var userRes = await _userManager.CreateAsync(user, model.Password);
                if (userRes.Succeeded)
                {
                    return Created(string.Empty, user);
                }
                return Problem(userRes.Errors.First().Description, null, 500);
            //}
        }
        [HttpPost("SignIn")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn([FromForm] SignInViewModel model)
        {
            //var user = _userManager.Users.FirstOrDefault(u => u.UserName == model.Email);
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user is null)
            {
                return NotFound($"User with email: '{model.Email}' not found");
            }

            var signinRes = await _userManager.CheckPasswordAsync(user, model.Password);
            if (signinRes)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(jwtGenerator.GenerateJwt(user, roles.ToList(), _jwtSettings));
            }
            return BadRequest("Email or password incorrect.");
        }

        [HttpPost("Roles")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name should be provided.");
            }

            var newRole = new Role
            {
                Name = roleName,
            };
            var result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
            {
                return Ok($"New role '{roleName}' is successfuly created!");
            }
            return Problem(result.Errors.First().Description,null,500);
        }
        [HttpPost("User/{userEmail}/Role")]
        public async Task<IActionResult> AddToRole(string userEmail,[FromBody] string roleName)
        {
            var user = await _userManager.FindByNameAsync(userEmail);
            if (user is null)
            {
                return NotFound("No user with same email!");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            return result.Succeeded ? Ok() : Problem(result.Errors.First().Description, null, 500);
        }
    }
}
