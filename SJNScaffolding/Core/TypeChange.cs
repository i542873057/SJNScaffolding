using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJNScaffolding.Core
{
    public static class TypeChange
    {
        //前导是数据库中字段，后导是对应程序中的数据类型
        public static Dictionary<string, string> typeChangeDictionary = new Dictionary<string, string>
        {
            {"int","int"},
            {"int?","int?"},
            {"nvarchar","string"},
            {"varchar","string"},
            {"decimal","decimal"},
            {"decimal?","decimal?"},
            {"datetime","DateTime?"},
            {"date","DateTime?"},
            {"bigint","long?"},
            {"bit","bool"},
            {"bit?","bool?"},
            {"bool","bool"},
            {"bool?","bool?"},
            {"long","long" },
            {"long?","long?" },
            {"other","" }
        };
    }
}
