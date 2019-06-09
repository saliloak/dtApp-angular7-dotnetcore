using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthRepository authRepository, IConfiguration configuration)
    {
      _authRepository = authRepository;
      _configuration = configuration;
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

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {
      var userFromRepo = await _authRepository.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

      if (userFromRepo == null)
        return Unauthorized();

      //Generate Claims
      var claims = new[]
      {
        new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
        new Claim(ClaimTypes.Name, userFromRepo.UserName),
      };

      //Generate Token Key
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

      //Generate Signing Credentials
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      //Create token
      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = creds
      };

      //Using token handler we can create a token and pass it on
      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return Ok(new {
        token = tokenHandler.WriteToken(token)
      });
    }
  }
}
