using Entities;
using Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.Json;
using DTO;
//using WebApiShop.Properties;

namespace WebApiShop.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            IEnumerable<UserDTO> usersDTO = await _service.GetAsync();
            if (usersDTO != null)
            {
                return Ok(usersDTO);
            }
            return NoContent();

        }


        // GET api/<Users>/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user =await _service.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NoContent();
        }

        // POST api/<Users>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            User user1 = await _service.AddUser(user);
            if(user1 != null) 
            {
                return CreatedAtAction(nameof(Get), new { Id = user1.UserId }, user1);
            }
            return BadRequest();
        }



        // POST api/<Users>
        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            User user1 = await _service.Login(user);
            if (user1 != null)
            {
                return Ok(user1);
            }
            return Unauthorized();
        }

        [HttpPut("{Id}")]
        public void Put(int id, [FromBody] User user)
        {
            _service.UpdateUser(id, user);
        }
    }
}
