using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Email.Util.reflex
{
    public class MethodTest
    {
        public static void Fun()
        {
            MyMethod obj = new MyMethod();
            MethodInfo method = typeof(MyMethod).GetMethod("StaticMethod");
            MethodInfo generic = method.MakeGenericMethod(typeof(string));
            generic.Invoke(null, new object[]{ "1",4});
        }
    }

    class MyMethod
    {
        public void GenericMethod<T>(T value,int num)
        {
            Console.WriteLine("value:"+value+","+num);
        }

        public  static void StaticMethod<T>(T value, int num)
        {
            Console.WriteLine("value:" + value + "," + num);
        }
    }
}
