﻿using AutoMapper;
using Emaill.Model.Models;
using Emaill.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Util.File
{
    public class MapHelper<TSource, TDestination>
    {
         public  MapHelper()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TSource, TDestination>());
        }

        public  List<TDestination> AutoMapList(List<TSource> resources)
        {
            List<TDestination> list = new List<TDestination>();
            foreach(var r in resources)
            {
                list.Add(AutoMap(r));
            }
            return list;

        }


        public  TDestination AutoMap(object resource)
        {
            
            return Mapper.Map<TDestination>(resource);
        }
    }

    public class ScheduleMap
    {
        static ScheduleMap()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Schedule, ScheduleModel>()
            .ForMember(dest=>dest.start,opt=>opt.MapFrom(src=>src.StartTime.ToString("yyyy-MM-dd HH:mm")))
            .ForMember(dest => dest.end, opt => opt.MapFrom(src => src.EndTime.ToString("yyyy-MM-dd HH:mm")))
            );
        }
        public static ScheduleModel AutoMap(Schedule resource)
        {

            return Mapper.Map<ScheduleModel>(resource);
        }
    }
    public class UserMap
    {
        static UserMap()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<AccountUser, UserModel>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
            );
        }
        public static UserModel AutoMap(AccountUser resource)
        {

            return Mapper.Map<UserModel>(resource);
        }
    }
}
