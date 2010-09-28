using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Trackyourtasks.Core.DAL.DataModel;

namespace Web.Infrastructure
{
    public class TrackyMapping 
    {
        public static void SetupMapping()
        {
            Mapper.CreateMap<Task, Web.API.v1.Model.TaskDto>();
        }
    }
}