using CG.Web.MegaApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCommon
{
    public class MegaApiClientEx : MegaApiClient
    {

        private INode FindFolder(IEnumerable<INode> nodes, string folderName)
        {
            var paths = MegaApiClientHelper.SplitSubPaths(folderName);
            var parentId = FindRoot(nodes)?.Id;
            if (!string.IsNullOrEmpty(parentId))
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    var folderNode = FindDirectory(nodes, paths[i], parentId);
                    if (folderNode == null)
                        return null;
                    if (i == (paths.Length - 1))
                        return folderNode;
                    parentId = folderNode.Id;
                }
            }
            return null;
        }

        //private INode FindDirectory(IEnumerable<INode> nodes, string folderName, string parentId)
        //{
        //    return nodes.FirstOrDefault(p => p.Type == NodeType.Directory
        //                                  && p.ParentId == parentId
        //                                  && p.Name == folderName);
        //}

        private INode FindNode(IEnumerable<INode> nodes, NodeType nodeType, string name, string parentId)
        {
            return nodes.FirstOrDefault(p => p.Type == nodeType
                                          && p.ParentId == parentId
                                          && p.Name == name);
        }

        private INode FindDirectory(IEnumerable<INode> nodes, string name, string parentId)
        {
            return FindNode(nodes, NodeType.Directory, name, parentId);
        }
        private INode FindDirectory(IEnumerable<INode> nodes, string name, INode parent)
        {
            return FindNode(nodes, NodeType.Directory, name, parent?.Id);
        }


        private INode FindRoot(IEnumerable<INode> nodes)
        {
            return FindNode(nodes, NodeType.Root, null, string.Empty);
        }



        public INode EnsureFolders(IEnumerable<INode> nodes, string folderName, INode rootNode = null)
        {
            var paths = MegaApiClientHelper.SplitSubPaths(folderName);
            var parent = rootNode ?? FindRoot(nodes);
            if (parent != null)
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    var folderNode = FindDirectory(nodes, paths[i], parent);
                    if (folderNode == null)
                        folderNode = CreateFolder(paths[i], parent);
                    if (folderNode == null)
                        break;
                    if (i == (paths.Length - 1))
                        return folderNode;
                    parent = folderNode;
                }
            }
            return null;
        }

     

        //private object CreateFolder(string parentId, string name)
        //{
        //   CreateFolder()
        //}
    }
}
