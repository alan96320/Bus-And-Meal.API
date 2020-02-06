using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

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
    [HttpPost("login")]
    public ActionResult Post([FromBody]User user)
    {
      if (user.Name == "test" && user.Password == "123")
      {
        var result = new
        {
          login = true,
          msg = "Login success"
        };
        return Ok(result);
      }
      else
      {
        var result = new
        {
          login = false,
          msg = "Login Failed!"
        };
        return Unauthorized(result);
      }
    }
  }
}



