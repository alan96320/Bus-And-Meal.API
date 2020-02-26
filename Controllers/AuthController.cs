using System.Linq;
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
using BusMeal.API.Core.Models;

namespace BusMeal.API.Controllers
{

  [Route("api/[controller]")]

  public class AuthController : Controller
  {

    private readonly IConfiguration config;
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private IUserModuleRightsRepository userModuleRepository;
    private IModuleRightsRepository moduleRightsRepository;

    public AuthController(
      IConfiguration config,
      IUserRepository userRepository,
      IMapper mapper,
      IUserModuleRightsRepository userModuleRepository,
      IModuleRightsRepository moduleRightsRepository)
    {
      this.config = config;
      this.userRepository = userRepository;
      this.mapper = mapper;
      this.userModuleRepository = userModuleRepository;
      this.moduleRightsRepository = moduleRightsRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginResource loginResource)
    {
      var username = loginResource.Username;
      var password = loginResource.Password;

      var userLogin = await userRepository.Login(username, password);

      if (userLogin == null)
        return Unauthorized();

      var userModules = await userModuleRepository.GetAll();
      var userModuleRights = userModules.Where(u => u.Id == userLogin.Id);

      var claims = new[]
          {
            new Claim(ClaimTypes.Name, userLogin.Username)
          };


      // var roles = new[] 
      // {};

      //   foreach (UserModuleRights items in userModuleRights)
      //   {
      //     var right = await moduleRightsRepository.GetOne(items.Id);
      //     if (items.Read == true)
      //     {
      //       claims[] = {
      //         new Claim(ClaimTypes.Role, right.Code+ ".R");
      //     };
      //   }

      //   if (items.Write == true)
      //   {
      //     claims.Add({
      //       new Claim(ClaimTypes.Role, right.Code + ".W");
      //     });
      //   }
      // }


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





