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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }
        

        // GET api/<Users>/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            Order orders =await _service.GetOrderByIdAsync(id);
            if (orders != null)
            {
                return Ok(orders);
            }
            return NoContent();
        }

        // POST api/<Users>
        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] Order order)
        {
            Order order1 = await _service.AddOrderAsync(order);
            if(order1 != null) 
            {
                return CreatedAtAction(nameof(Get), new { Id = order1.OrderId }, order1);
            }
            return BadRequest();
        }
    }
}
