using AutoMapper;
using Trackyt.Core.DAL.DataModel;

namespace Web.Infrastructure
{
    [CoverageExcludeAttribute]
    public class TrackyMapping 
    {
        public static void SetupMapping()
        {
            Mapper.CreateMap<Task, Web.API.v1.Model.TaskDto>();
        }
    }
}