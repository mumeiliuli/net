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

namespace Email.Console
{
    class Program
    {

        static void Main(string[] args)
        {
            string str = "abcdefgh";
            var bytes=Encoding.Default.GetBytes(str);
            string hex = BitConverter.ToString(bytes);
            System.Console.Write(hex);

            System.Console.ReadLine();
           
        }
    }
    
}
