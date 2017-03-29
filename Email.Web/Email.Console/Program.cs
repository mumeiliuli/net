using Email.ConsoleTest.castle;
using Email.Util.amqp;
using Email.Util.json;
using Email.Util.other;
using Email.Util.reflex;
using Email.Util.solr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.ConsoleTest
{

    class Program
    {
        static void Main(string[] args)
        {
            InterceptorFun.Fun();
            Console.Read();
        }
    }

    // T is the type of data stored in a particular instance of GenericList.
    

}
