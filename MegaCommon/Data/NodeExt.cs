using CG.Web.MegaApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCommon.Data
{
    public class NodeExt
    {
        public readonly INode Node;

        public readonly List<INode> Children;



        public NodeExt(INode node)
        {
            Node = node;
        }
    }
}
