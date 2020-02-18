using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Controllers.Resources;
using System.Threading.Tasks;
using AutoMapper;

namespace BusMeal.API.Controllers
{
  public class User
  {
    public string Name { get; set; }
    public string Password { get; set; }

  }

  [Route("api/[controller]")]

  public class AuthController : Controller
  {

    private readonly IConfiguration config;
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public AuthController(IConfiguration config, IUserRepository userRepository, IMapper mapper)
    {
      this.config = config;
      this.userRepository = userRepository;
      this.mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginResource loginResource)
    {
      var userLogin = await userRepository.Login(loginResource.Username, loginResource.Password);

      if (userLogin == null)
        return Unauthorized();

      var claims = new[]
          {
                new Claim(ClaimTypes.Name, userLogin.Username)
            };

      var key = new SymmetricSecurityKey(Encoding.UTF8
          .GetBytes(this.config.GetSection("AppSettings:Token").Value));

      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = creds
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateToken(tokenDescriptor);

      var user = mapper.Map<ViewUserResource>(userLogin);

      return Ok(new
      {
        token = tokenHandler.WriteToken(token),
        user
      });
    }
  }
}





