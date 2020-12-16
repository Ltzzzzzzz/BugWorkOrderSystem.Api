using System;
using System.Reflection;
using Autofac;
using BugWorkOrderSystem.IRepository.Base;
using BugWorkOrderSystem.IServices.Base;
using BugWorkOrderSystem.Repository.Base;
using BugWorkOrderSystem.Services.Base;

namespace BugWorkOrderSystem.Extensions.Extensions
{
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 注册泛型仓储
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();

            // 注册泛型服务
            builder.RegisterGeneric(typeof(BaseServices<>)).As(typeof(IBaseServices<>)).InstancePerDependency();

            //  注册Repository
            var assemblysRepository = Assembly.Load("BugWorkOrderSystem.Repository");
            builder.RegisterAssemblyTypes(assemblysRepository)
                .InstancePerDependency() // 每次调用，都会重新实例化对象；每次请求都创建一个新的对象； = AddScope
                .AsImplementedInterfaces();

            //  注册Services
            var assemblysServices = Assembly.Load("BugWorkOrderSystem.Services");
            builder.RegisterAssemblyTypes(assemblysServices)
                .InstancePerDependency()
                .AsImplementedInterfaces();
        }
    }
}
