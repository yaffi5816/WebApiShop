using Microsoft.AspNetCore.Mvc;
using Services;
using Entities;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("application/json")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;
        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpPost("PasswordScore")]
        public ActionResult<int> PasswordScore([FromBody] string password)
        {
            int strength = _passwordService.GetPasswordScore(password);
            return Ok(strength);
        }
    }
}
