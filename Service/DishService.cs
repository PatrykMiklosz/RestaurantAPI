using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Service
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);
        List<DishDto> Get(int restaurantId);
        bool Delete(int restaurantId, int dishId);
    }

    public class DishService : IDishService
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<DishService> logger;

        public DishService(RestaurantDbContext dbContext, IMapper mapper, ILogger<DishService> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }
        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = dbContext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            var dish = mapper.Map<Dish>(dto);

            dbContext.Dishes.Add(dish);
            dbContext.SaveChanges();
            return dish.Id;
        }

        public bool Delete(int restaurantId, int dishId)
        {
            logger.LogWarning($"Dish with ID : {dishId}  DELETED ACTION INVOKED");
            var restaurant = dbContext.Restaurants.Include(r => r.Dishes).FirstOrDefault(r => r.Id == restaurantId);
            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish is null)
                return false;
            dbContext.Remove(dish);
            dbContext.SaveChanges();
            return true;
        }

        public List<DishDto> Get(int restaurantId)
        {
            var restaurant = dbContext.Restaurants.Include(r => r.Dishes).FirstOrDefault(r => r.Id == restaurantId);
            var dishes = restaurant.Dishes.ToList();
            var dishesDto = mapper.Map<List<DishDto>>(dishes);
            return dishesDto;
        }
    }
}