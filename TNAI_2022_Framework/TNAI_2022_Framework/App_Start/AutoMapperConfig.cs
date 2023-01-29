using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TNAI.Model.Entities;

using TNAI_2022_Framework.Models.OutputModels;

namespace TNAI_2022_Framework.App_Start
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<Category, CategoryOutputModel>()
                .ForMember(x => x.Name, d => d.MapFrom(s => $"{s.Id} - {s.Name}"));
            CreateMap<Product, ProductOutputModel>()
                .ForMember(x => x.Name, d => d.MapFrom(s => $"{s.Id} - {s.Name}"));
            CreateMap<Order, OrderOutputModel>()
                .ForMember(x => x.Name, d => d.MapFrom(s => $"{s.Id} - {s.Name}"));
        }
    }
}