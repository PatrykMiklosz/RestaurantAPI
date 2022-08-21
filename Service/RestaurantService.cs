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
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        List<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        bool Update(int id, CreateRestaurantDto dto);
        bool Delete(int id);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IMapper mapper;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = mapper.Map<Restaurant>(dto);
            dbContext.Add(restaurant);
            dbContext.SaveChanges();
            return restaurant.Id;
        }

        public bool Delete(int id)
        {
            var restaurant = dbContext.Restaurants.Include(r => r.Address).FirstOrDefault(r => r.Id == id);
            if (restaurant is null)
                return false;
            dbContext.Remove(restaurant);
            dbContext.SaveChanges();
            return true;
        }

        public List<RestaurantDto> GetAll()
        {
            var restaurants = dbContext.Restaurants.Include(r => r.Address).ToList();
            var restaurantDtos = mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantDtos;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = dbContext.Restaurants.Include(r => r.Address).FirstOrDefault(r => r.Id == id);
            if (restaurant is null)
                return null;
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }

        public bool Update(int id, CreateRestaurantDto dto)
        {
            var restaurant = dbContext.Restaurants.Include(r => r.Address).FirstOrDefault(r => r.Id == id);
            if (restaurant is null)
                return false;
            restaurant.Address.City = dto.City;
            restaurant.Address.Street = dto.Street;
            restaurant.ContactNumber = dto.ContactNumber;
            restaurant.Category = dto.Category;
            restaurant.HasDelivery = dto.HasDelivery;
            restaurant.Name = dto.Name;
            dbContext.SaveChanges();
            return true;
        }
    }
}