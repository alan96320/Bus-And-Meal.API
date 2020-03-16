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
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginResource loginResource)
    {
      var username = loginResource.Username;
      var password = loginResource.Password;

      var userLogin = await userRepository.Login(username, password);

      if (userLogin == null)
        return Unauthorized();

      var allUserModules = await userModuleRepository.GetAll();
      var userModules = allUserModules.Where(u => u.UserId == userLogin.Id).ToList();

      // Add user claim
      var claims = new List<Claim>();
      claims.Add(new Claim(ClaimTypes.Name, userLogin.Username));
      claims.Add(new Claim("Id", userLogin.Id.ToString()));

      if (userLogin.AdminStatus == true)
      {
        claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
      }

      foreach (UserModuleRight userModule in userModules)
      {
        var right = await moduleRightsRepository.GetOne(userModule.ModuleRightsId);
        var claim = right.Description.ToString();

        if (userModule.Read == true)
        {
          claims.Add(new Claim(ClaimTypes.Role, $"{claim}.R"));
        }

        if (userModule.Write == true)
        {
          claims.Add(new Claim(ClaimTypes.Role, $"{claim}.W"));
        }
      }

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





