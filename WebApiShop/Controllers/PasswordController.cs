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
        PasswordService password = new PasswordService();

        [HttpPost]
        public ActionResult<Password> Post([FromBody] Password passwordFromUser)
        {
            Password password1 = password.CheckPassword(passwordFromUser.ThePassword);
            return Ok(password1);

        }
    }
}
