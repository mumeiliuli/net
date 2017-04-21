using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Email.ConsoleTest.castle;
using Email.ConsoleTest.castle.assembly;
using Email.Util.amqp;
using Email.Util.json;
using Email.Util.other;
using Email.Util.reflex;
using Email.Util.solr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Email.ConsoleTest
{
    public class Person : IComparable
    {
        public int Age { get; set; }
        public int CompareTo(object obj)
        {
            var parent = Assembly.GetCallingAssembly();
            return 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Type basetype=typeof(IAnimal);
            if (basetype.IsAssignableFrom(typeof(IAnimal)))
            {
                Console.WriteLine("相同类型");
            }
            if (basetype.IsAssignableFrom(typeof(Dog)))
            {
                Console.WriteLine("派生类");
            }
            if (typeof(Nullable<Int32>).IsAssignableFrom(typeof(Int32)))
            {
                Console.WriteLine("值类型");
            }
            if (basetype.IsGenericTypeDefinition)
            {
                typeof(List<Dog>).GetInterface("IAnimal").GetGenericTypeDefinition();
            }
        }
    }

    // T is the type of data stored in a particular instance of GenericList.
    

}
