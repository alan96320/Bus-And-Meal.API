using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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
    public AuthController(IConfiguration config)
    {
      this.config = config;
    }

    [HttpPost("login")]
    public ActionResult Post([FromBody]User user)
    {
      if (user.Name == "test" && user.Password == "123")
      {
        var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name)
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


        return Ok(new
        {
          token = tokenHandler.WriteToken(token),
          login = true,
          message = "Login success!"
        });
      }
      else
      {
        return Unauthorized(new
        {
          token = "No token available!",
          login = false,
          message = "Login failed!"
        });
      }
    }
  }
}





