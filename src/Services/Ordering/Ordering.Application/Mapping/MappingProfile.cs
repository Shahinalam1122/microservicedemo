﻿using AutoMapper;
using Ordering.Application.Features.Orders.Queries.GetOrdersByUserName;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Order, OrderVm>().ReverseMap();
        }
    }
}
