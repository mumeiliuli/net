﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;

namespace Email.ConsoleTest.castle
{
    public class ModuleTest
    {
    }
    public class WebAssemblyFinder 
    {
        /// <summary>
        /// This return all assemblies in bin folder of the web application.
        /// </summary>
        /// <returns>List of assemblies</returns>
        public List<Assembly> GetAllAssemblies()
        {
            var assembliesInBinFolder = new List<Assembly>();

            var allReferencedAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            var dllFiles = Directory.GetFiles(HttpRuntime.AppDomainAppPath + "bin\\", "*.dll", SearchOption.TopDirectoryOnly).ToList();

            foreach (string dllFile in dllFiles)
            {
                var locatedAssembly = allReferencedAssemblies.FirstOrDefault(asm => AssemblyName.ReferenceMatchesDefinition(asm.GetName(), AssemblyName.GetAssemblyName(dllFile)));
                if (locatedAssembly != null)
                {
                    assembliesInBinFolder.Add(locatedAssembly);
                }
            }

            return assembliesInBinFolder;
        }
    }
}
