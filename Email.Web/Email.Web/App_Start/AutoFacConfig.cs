using Autofac;
using Autofac.Integration.Mvc;
using Email.Service;
using Emaill.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Email.Web
{
    public class AutoFacConfig
    {
        public static void RegisterServce()
        {
            var builder = new ContainerBuilder();
            var baseType = typeof(IDependency);

            builder.RegisterType<EmailDbContext>();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t=>baseType.IsAssignableFrom(t)).AsImplementedInterfaces().InstancePerLifetimeScope();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}