using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.YMX.Utils
{
    public static class JsonConfigUtil
    {
        public static ProxyConfig ProxyUtil { get; set; } = null;
    }

    public class ProxyConfig
    {
        public string ProxyUrl { get; set; }
        public string Account { get; set; }
        public string Pwd { get; set; }
    }
}
