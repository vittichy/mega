using MegaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MegaClient();
            client.GetClient();
        }
    }
}
