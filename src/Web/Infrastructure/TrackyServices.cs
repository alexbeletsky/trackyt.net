using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.DAL.Repositories.Impl;
using Web.Infrastructure.Security;
using AutoMapper;
using Web.Infrastructure.Helpers;
using Web.Services;

namespace Web.Infrastructure
{
    [CoverageExcludeAttribute]
    class TrackyServices : NinjectModule
    {
        public override void Load()
        {
            Bind<IUsersRepository>().To<UsersRepository>();
            Bind<ITasksRepository>().To<TasksRepository>();
            Bind<IFormsAuthentication>().To<TrackyFormsAuthentication>();
            Bind<IPathHelper>().To<PathHelper>();
            Bind<IMappingEngine>().ToConstant(Mapper.Engine);
            Bind<IBlogPostsRepository>().To<BlogPostsRepository>();
            Bind<IAuthenticationService>().To<AuthenticationService>();
        }
    }
}
