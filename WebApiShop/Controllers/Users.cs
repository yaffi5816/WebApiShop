using Entities;
using Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.Json;
//using WebApiShop.Properties;

namespace WebApiShop.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly UserService _userService;
        private readonly PasswordService _passwordService;

        public Users(UserService userService, PasswordService passwordService)
        {
            _userService = userService;
            _passwordService = passwordService;
        }


        // GET api/<Users>/5
        [HttpGet("{Id}")]
        public ActionResult<User> Get(int id)
        {
            User user = _userService.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        // POST api/<Users>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            Password passwordCheck = _passwordService.CheckPassword(user.Password);
            if (passwordCheck.Level < 2)
                return BadRequest($"Password too weak (score: {passwordCheck.Level}/4). Minimum required: 2");

            User user1 = _userService.AddUser(user);
            if(user1 != null) 
            {
                return CreatedAtAction(nameof(Get), new { Id = user1.Id }, user1);
            }
            return BadRequest("Failed to register user");
        }



        // POST api/<Users>
        [HttpPost("Login")]
        public ActionResult<User> Login([FromBody] User user)
        {
            User user1 = _userService.Login(user);
            if (user1 != null)
            {
                return Ok(user1);
            }
            return Unauthorized("Invalid username or password");

        }

        [HttpPut("{Id}")]
        public ActionResult Put(int id, [FromBody] User user)
        {
            Password passwordCheck = _passwordService.CheckPassword(user.Password);
            if (passwordCheck.Level < 2)
                return BadRequest($"Password too weak (score: {passwordCheck.Level}/4). Minimum required: 2");

            _userService.UpdateUser(id, user);
            return NoContent();
        }
    }
}
