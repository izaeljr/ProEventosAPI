using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Application.Contracts;
using ProEventos.Application.DTOs;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
       public AccountController(IAccountService accountService, ITokenService tokenService)
       {
            _accountService = accountService;
            _tokenService = tokenService;
       }

       [HttpGet("GetUser/{username}")]
       [AllowAnonymous]
       public async Task<IActionResult> GetUser(string username)
       {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(username);
                return Ok(user);
                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error while retrieving user. Error: {ex.Message}");
            }
       }

       [HttpPost("Register")]
       [AllowAnonymous]
       public async Task<IActionResult> Register(UserDto userDto)
       {
            try
            {
                if (await _accountService.UserExist(userDto.Username))
                    return BadRequest("User already exists.");
                
                var user = await _accountService.CreateAccountAsync(userDto);

                if (user != null)
                    return Ok(user);
                
                return BadRequest("User not created, try again later.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error while retrieving user. Error: {ex.Message}");
            }
       }
    }
}