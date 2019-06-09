using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace DatingApp.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
      _authRepository = authRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
    {
      userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
      if (await _authRepository.UserExists(userForRegisterDto.Username))
        return BadRequest("Username Already Exists");

      var userToCreate = new User
      {
        UserName = userForRegisterDto.Username
      };

      var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);

      return StatusCode(201);
    }
  }
}
