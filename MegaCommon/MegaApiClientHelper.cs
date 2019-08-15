using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCommon
{
    public static class MegaApiClientHelper
    {

        public static string[] SplitSubPaths(string fullPath)
        {
            var paths = (fullPath ?? string.Empty).Split(Path.DirectorySeparatorChar).Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
            return paths;
        }




    }
}
