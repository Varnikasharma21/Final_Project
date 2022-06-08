
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//using New.Models;
using New.RequestModel;

namespace New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public static int userid;
        public static string name;
        private IConfiguration _drive;
        private readonly DriveContext _drivecontext;
        public LoginController(IConfiguration drive, DriveContext driveContext)
        {
            _drive = drive;
            _drivecontext = driveContext;
        }


        // POST: api/Login
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginRequest login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString ,userid,name});
            }

            return response;
        }
        private string BuildToken(UsersRequest user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_drive["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_drive["Jwt:Issuer"],
              _drive["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UsersRequest Authenticate(LoginRequest login)
        {
            UsersRequest user = null;

            var result = _drivecontext.Users.FirstOrDefault(obj => obj.Username == login.Username);

            try
            {
                if (result.Username != null && result.UserPassword == login.UserPassword)
                {
                    user = new UsersRequest { Username = result.Username, UserPassword = result.UserPassword };
                    userid = result.UserId;
                    name = result.Username;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return user;
        }
    }
}
