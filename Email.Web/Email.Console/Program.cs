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
        static Stopwatch Watch = new Stopwatch();

        static void Main(string[] args)
        {

            //启动计时器
            Watch.Start();

            const string url1 = "http://www.cnblogs.com/";
            const string url2 = "http://www.cnblogs.com/liqingwen/";

            //两次调用 CountCharacters 方法（下载某网站内容，并统计字符的个数）
             var t1= CountCharactersAsync(1);
             var t2= CountCharactersAsync(2);

            //三次调用 ExtraOperation 方法（主要是通过拼接字符串达到耗时操作）
            for (var i = 0; i <3; i++)
            {
                ExtraOperation(i + 1);
            }

            //控制台输出
            Console.WriteLine($"{url1} 的字符个数：{t1}");
            Console.WriteLine($"{url2} 的字符个数：{t2}");

            Console.Read();
        }
        /// <summary>
        /// 统计字符个数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        private static long  CountCharactersAsync(int max)
        {
            Console.WriteLine($"开始调用 max = {max}：{Watch.ElapsedMilliseconds} ms");

            var sum=  AddAsync(max);
            Console.WriteLine($"调用完成 max = {max}：{Watch.ElapsedMilliseconds} ms");
            return sum.Result;

        }

        private static async Task<long> AddAsync(long max)
        {
            long sum = 0;
            sum=await Task.Run(() =>{
                for (long i = 0; i < max * 100000000; i++)
                {
                    if (i % 4 == 0)
                    {
                        sum += i;
                    }
                    else if (i % 3 == 0)
                    {
                        sum -= i;
                    }
                    else
                    {
                        sum += 1;
                    }
                }
                return sum;

            });
            return sum;
           
        }

        /// <summary>
        /// 额外操作
        /// </summary>
        /// <param name="id"></param>
        private static void ExtraOperation(int id)
        {
            //这里是通过拼接字符串进行一些相对耗时的操作
            var s = "";

            for (var i = 0; i < 6000; i++)
            {
                s += i;
            }

            Console.WriteLine($"id = {id} 的 ExtraOperation 方法完成：{Watch.ElapsedMilliseconds} ms");
        }
    }

    // T is the type of data stored in a particular instance of GenericList.
    

}
