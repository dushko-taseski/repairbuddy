using App.BusinessLogicContracts;
using App.Entities;
using App.Web.Dtos.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("user/profile")]
        public IActionResult UserProfile()
        {
            var result = _userService.GetById(LoggedInUserId);
            if(!result.IsFaulted)
            {
                return Ok(result.Payload);
            }

            return BadRequest(result.ErrorMessage);
        }

        [AllowAnonymous]
        [Route("user/register")]
        [HttpPost]
        public IActionResult Registration([FromBody] RegisterUserDto registerUserDto)
        {
            var result = _userService.Register(registerUserDto.Email, registerUserDto.FullName, registerUserDto.Address, registerUserDto.Phone, registerUserDto.Password, registerUserDto.ConfirmPassword);
            if (!result.IsFaulted)
            {
                return Ok(result.Payload);
            }
           
            return BadRequest(result.ErrorMessage);
        }

        [Route("user/update")]
        [HttpPost]
        public IActionResult UpdateUser(UpdateUserDto updateUserDto)
        {
            var result = _userService.UpdateUser(updateUserDto.Id, updateUserDto.FullName, updateUserDto.Address, updateUserDto.Phone);
            if (!result.IsFaulted)
            {
                return Ok(result.Payload);
            }

            return NotFound(result.ErrorMessage);
        }

        [Route("user/delete")]
        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            var result = _userService.DeleteUser(userId);
            if(!result.IsFaulted)
            {
                return Ok();
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [AllowAnonymous]
        [Route("user/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var result = _userService.FindByEmailAndPassword(loginUserDto.Email, loginUserDto.Password);

            if (!result.IsFaulted)
            {
                var user = result.Payload;
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserId", user.Id.ToString()),
                 };

                var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                await HttpContext.SignInAsync(userPrincipal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddYears(1)
                });
                
                return StatusCode(200);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [Route("user/logout")]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return StatusCode(200);
        }
    }
}
