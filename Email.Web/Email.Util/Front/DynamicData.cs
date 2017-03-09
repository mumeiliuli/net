using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Util.Front
{
    public partial class DynamicData : DynamicObject
    {
        private IDictionary<string, object> _data = new Dictionary<string, object>();

        /// <summary>
        /// 字典数据
        /// </summary>
        public IDictionary<string, object> Obj
        {
            get { return _data; }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (_data.ContainsKey(binder.Name))
                _data[binder.Name] = value;
            else
                _data.Add(binder.Name, value);

            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name;
            return _data.TryGetValue(name, out result);
        }
    }
}
