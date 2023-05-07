using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace D.YMX.Models
{
    public class Cache
    { 
        public static List<string> Keywords = new List<string>();
        public static Dictionary<string,Product> ProductList = new Dictionary<string, Product>();
    }
}
