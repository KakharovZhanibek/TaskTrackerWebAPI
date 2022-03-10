using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerWebAPI.DAL.Entities;
using TaskTrackerWebAPI.View_Models;

namespace TaskTrackerWebAPI
{
    public static class MappingConfigurator
    {
        public static Mapper ConfigureMapper()
        {
            var configurationExpression = new MapperConfigurationExpression();

            MapBothSide<ProjectEntity, ProjectVM>(configurationExpression);
            MapBothSide<TaskEntity, TaskVM>(configurationExpression);

            var config = new MapperConfiguration(configurationExpression);
            var mapper = new Mapper(config);

            return mapper;
        }
        public static void MapBothSide<Type1, Type2>(MapperConfigurationExpression configurationExpression)
        {
            configurationExpression.CreateMap<Type1, Type2>();
            configurationExpression.CreateMap<Type2, Type1>();
        }
    }
}
