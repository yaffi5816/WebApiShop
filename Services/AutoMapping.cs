using AutoMapper;
using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDTO>();

            CreateMap<Product, ProductDTO>();

            CreateMap<Order, OrderDTO>();
        }
    }
}
