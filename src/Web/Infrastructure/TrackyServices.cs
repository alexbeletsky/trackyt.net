using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using Trackyourtasks.Core.DAL.Repositories;
using Trackyourtasks.Core.DAL.Repositories.Impl;
using Web.Infrastructure.Security;
using AutoMapper;

namespace Web.Infrastructure
{
    class TrackyServices : NinjectModule
    {
        public override void Load()
        {
            Bind<IUsersRepository>().To<UsersRepository>();
            Bind<ITasksRepository>().To<TasksRepository>();
            Bind<IFormsAuthentication>().To<TrackyFormsAuthentication>();
            Bind<IMappingEngine>().ToConstant(Mapper.Engine);
        }
    }
}
