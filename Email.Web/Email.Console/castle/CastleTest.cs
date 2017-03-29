using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Email.ConsoleTest.castle
{
    public interface ITest
    {
        string message { get; set; }
        void GetMessage();
    }
    public class CastleTest : ITest
    {
        private string _msg;
        public string message
        {
            get
            {
                return _msg;
            }

            set
            {
                _msg=value;
            }
        }
        public CastleTest()
        {
            message = "castle test";
        }

        public void GetMessage()
        {
            System.Console.WriteLine(message);
        }
    }
}
