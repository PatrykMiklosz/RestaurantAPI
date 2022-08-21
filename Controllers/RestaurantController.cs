using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Service;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService service;

        public RestaurantController(IRestaurantService service)
        {
            this.service = service;
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = service.Create(dto);
            return Created($"api/restaurant/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var dtos = service.GetAll();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetById([FromRoute] int id)
        {
            var dto = service.GetById(id);
            if (dto is null)
                return NotFound();
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = service.Delete(id);
            if (isDeleted)
                return NoContent();
            else
                return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] CreateRestaurantDto dto)
        {
            var isUpdated = service.Update(id, dto);
            if (isUpdated)
                return Ok($"api/restaurant/{id}");
            else
            return NotFound();
        }
    }
}