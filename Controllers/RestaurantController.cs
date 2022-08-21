using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IMapper mapper;

        public RestaurantController(RestaurantDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurant = mapper.Map<Restaurant>(dto);
            dbContext.Add(restaurant);
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = dbContext.Restaurants.Include(r => r.Address).ToList();
            var restauratnsDto = mapper.Map<List<RestaurantDto>>(restaurants);
            return Ok(restauratnsDto);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetById([FromRoute] int id)
        {
            var restaurant = dbContext.Restaurants.Include(r => r.Address).FirstOrDefault(r => r.Id == id);
            if (restaurant is null)
                return NotFound();
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
            return Ok(restaurantDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById([FromRoute] int id)
        {
            var restaurant = dbContext.Restaurants.Include(r => r.Address).FirstOrDefault(r => r.Id == id);
            if (restaurant is null)
                return NotFound();
            dbContext.Remove(restaurant);
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateById([FromRoute] int id, [FromBody] CreateRestaurantDto dto)
        {
            var restaurant = dbContext.Restaurants.Include(r => r.Address).FirstOrDefault(r => r.Id == id);
            if (restaurant is null)
                return NotFound();
            restaurant.Address.City = dto.City;
            restaurant.Address.Street = dto.Street;
            restaurant.ContactNumber = dto.ContactNumber;
            restaurant.Category = dto.Category;
            restaurant.HasDelivery = dto.HasDelivery;
            restaurant.Name = dto.Name;
            dbContext.SaveChanges();
            return Ok(); 
        }
    }
}