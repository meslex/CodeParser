using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParse
{
    class DataEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public DataEntity(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
