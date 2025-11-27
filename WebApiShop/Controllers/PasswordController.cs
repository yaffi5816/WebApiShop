using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly PasswordService _passwordService;

        public PasswordController(PasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpPost]
        public ActionResult<Password> Post([FromBody] Password passwordFromUser)
        {
            Password password1 = _passwordService.CheckPassword(passwordFromUser.ThePassword);
            return Ok(password1);

        }
    }
}
