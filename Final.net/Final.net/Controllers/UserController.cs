
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using New.Models;
using New.RequestModel;

namespace New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DriveContext _driveContext;
        public UsersController(DriveContext User)
        {
            _driveContext = User;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult Get()
        {
            var getUser = _driveContext.Users.ToList();
            return Ok(getUser);
        }

       
        [HttpPost]
        public void Post([FromBody] UsersRequest value)
        {
            Users obj = new Users();
            obj.Username = value.Username;
            obj.UserPassword = value.UserPassword;
            obj.CreatedAt = value.CreatedAt;
            _driveContext.Users.Add(obj);
            _driveContext.SaveChanges();

        }

    }
}
