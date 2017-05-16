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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Email.ConsoleTest
{
    class Model
    {
        public  void Test1()
        {
            Console.WriteLine("Model Test1");
        }

        public virtual void Test2()
        {
            Console.WriteLine("Model Test2");
        }
    }
    class PlayerModelLocator : Model
    {
        static PlayerModelLocator mInstance = null;
        public static PlayerModelLocator Instance
        {
            get
            {
                if (mInstance == null)
                    mInstance = new PlayerModelLocator();
                return mInstance;
            }
        }
       
        //覆写  
        public new void Test1()
        {
            Console.WriteLine("PlayerModelLocator Test1");
        }
        //重新  
        public override void Test2()
        {
            Console.WriteLine("PlayerModelLocator Test2");
        }
    }
    class Program
    {
        static Stopwatch Watch = new Stopwatch();

        static void Main(string[] args)
        {
            Model playerModel = PlayerModelLocator.Instance as Model;
            playerModel.Test1();
            playerModel.Test2();
            Console.Read();
        }
    }
    // T is the type of data stored in a particular instance of GenericList.
    

}
