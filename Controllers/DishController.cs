using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Service;

namespace RestaurantAPI.Controllers
{
    [Route("api/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService service;

        public DishController(IDishService service)
        {
            this.service = service;
        }

        [HttpPost]
        public ActionResult Create([FromRoute] int restaurantId, [FromBody] CreateDishDto dto)
        {
            var dishId = service.Create(restaurantId, dto);
            return Created($"api/{restaurantId}/dish/{dishId}", null);
        }

        [HttpGet]
        public ActionResult Get([FromRoute] int restaurantId)
        {
            var dishes = service.Get(restaurantId);
            return Ok(dishes);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int restaurantId, [FromRoute] int id)
        {
            var isDeleted = service.Delete(restaurantId, id);
            if(isDeleted)
            return NoContent();
            else
            return NotFound();
        }
    }
}