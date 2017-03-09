using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Util.json
{
    public class JsonTest
    {
        public void Fun()
        {
            MyJson json = new MyJson() { age=1,color=Color.red};
            var str=JsonConvert.SerializeObject(json);
            Console.WriteLine(str);

        }
    }
    public enum Color { red, yellow }
    public class MyJson
    {
        public int age { get; set; }
        [JsonConverter(typeof(StringEnumConverter))] //enum序列化为字符串
        public Color color { get; set; }
    }
}
