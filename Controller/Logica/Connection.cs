using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Logica
{
    public static class Connection
    {
        public static string IP { get; set; }
   
        public static string Port { get; set; } 

        public static string LINK = $"http://{IP}:{Port}";



    }
}
