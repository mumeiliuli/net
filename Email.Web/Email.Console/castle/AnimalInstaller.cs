using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Reflection;

namespace Email.ConsoleTest.castle
{
    public class AnimalInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            
            container.Register(Classes.FromThisAssembly().Where(t=>t.Name.EndsWith("Aniaml"))
                            .WithService.DefaultInterfaces());
        }
    }
}
