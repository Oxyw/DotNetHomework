using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderApi.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderContext _db;

        public OrderController(ILogger<OrderController> logger, OrderContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return (from o in _db.Orders.Include(o => o.Items)
                    orderby o.TotalPrice
                    select o).ToList();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            try
            {
                await _db.Orders.AddAsync(order);
                await _db.OrderDetails.AddRangeAsync(order.Items);
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }

            return order;
        }

        [HttpDelete("delete")]
        public ActionResult DeleteOrder(Order order)
        {
            if (order != null && !_db.Orders.Where(o => o.OrderId == order.OrderId).Any())
            {
                _db.OrderDetails.RemoveRange(order.Items);
                _db.Orders.Remove(order);
                _db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
