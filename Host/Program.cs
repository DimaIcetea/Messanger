using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(Messanger.MsgService)))
            {
                host.Open();
                Console.WriteLine("Started correctly!");
                Console.ReadLine();
            }
        }
    }
}
