using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r=>r.Address, c=>c.MapFrom(dto=>new Address()
                {
                    City = dto.City, Street = dto.Street
                }));
        }
    }
}