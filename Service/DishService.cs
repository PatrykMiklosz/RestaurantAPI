using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Service
{
    public interface IDishService
    {
        int Create(int id, CreateDishDto dto);
    }

    public class DishService : IDishService
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IMapper mapper;

        public DishService(RestaurantDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public int Create(int id, CreateDishDto dto)
        {
            var restaurant = dbContext.Restaurants.FirstOrDefault(r => r.Id == id);
            var dish = mapper.Map<Dish>(dto);
            
            dbContext.Dishes.Add(dish);
            dbContext.SaveChanges();
            return dish.Id;
        }
    }
}