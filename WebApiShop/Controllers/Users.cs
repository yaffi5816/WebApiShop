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
        UserService service=new UserService();


        // GET api/<Users>/5
        [HttpGet("{Id}")]
        public ActionResult<User> Get(int id)
        {
            User user =service.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NoContent();
        }

        // POST api/<Users>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            User user1 = service.AddUser(user);
            if(user1 != null) 
            {
                return CreatedAtAction(nameof(Get), new { Id = user1.Id }, user1);
            }
            return BadRequest();
        }



        // POST api/<Users>
        [HttpPost("Login")]
        public ActionResult<User> Login([FromBody] User user)
        {
            User user1 = service.Login(user);
            if (user1 != null)
            {
                return CreatedAtAction(nameof(Get), new { Id = user1.Id }, user1);
            }
            return BadRequest();

        }

        [HttpPut("{Id}")]
        public void Put(int id, [FromBody] User user)
        {
            service.UpdateUser(id, user);
        }
    }
}
