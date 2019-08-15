using CG.Web.MegaApiClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vt.extensions;

namespace MegaCommon
{
    public class MegaClient
    {

        public void GetClient()
        {


            var megaRoot = @"TEST\SUB";



            var client = new MegaApiClientEx();
            client.Login("24kf@seznam.cz", "heslodne24");

            IEnumerable<INode> nodes = client.GetNodes();

            var rootNode = client.EnsureFolders(nodes, "2021"); // @"aaa\bbbbb\cccc\d\");
            if(rootNode != null)
            {
                nodes = client.GetNodes();

                var localRoot = @"D:\tmp\C1_END\vt-data\foto"; //  @"D:\tmp\mega-photos-test";

                var localFiles = GetLocalFiles(localRoot);

                foreach(var dir in localFiles)
                {
                    foreach(var file in dir.Value)
                    {
                        SYnc(client, dir.Key, file, localRoot, nodes, rootNode);

                        // TODO :-)
                        nodes = client.GetNodes();
                    }


                    
                }

                //var localRoot = @"d:\tmp\mega-photos-test\";

            }
            else
            {
                Console.WriteLine("root not found?");
            }


            // var iii = FindMegaFolder(nodes, @"photos-TEST\2019\2019-05-05-Akce");


            //     var root = FindCreateMegaRoot(nodes, @"mega\TEST\FLD1\FLD2\");

            //BuildNodeTree(nodes);

            //INode root = nodes.Single(x => x.Type == NodeType.Root);
            //INode myFolder = client.CreateFolder("Upload", root);



            //INode myFile = client.UploadFile(@"d:\tmp\competitivecyclist-fit-road-french.png", myFolder);
            //Uri downloadLink = client.GetDownloadLink(myFile);
            //Console.WriteLine(downloadLink);

            client.Logout();


          //  root.
        }

        private void SYnc(MegaApiClientEx client, DirectoryInfo key, FileInfo file, string root, IEnumerable<INode> nodes, INode rootNode)
        {
            Console.WriteLine($"****** {DateTime.Now}");
            Console.WriteLine($"file:{file.FullName}");

            var relative = GetRelative(root, file);
            var dirName = Path.GetDirectoryName(relative);
            Console.WriteLine($"relative:{relative}, dirName:{dirName}");

            var megaNode = client.EnsureFolders(nodes, dirName, rootNode);
            Console.WriteLine($"megaNode:{megaNode.Id} {megaNode.Name}");


            INode fileNode = client.UploadFile(file.FullName, megaNode);
            Console.WriteLine($"fileNode:{fileNode.Id} {fileNode.Name}");



        }

        private string GetRelative(string root, FileInfo file)
        {
            if (file.FullName.StartsWith(root))
            {
                return file.FullName.Substring(root.Length);
            }
            return null;
        }

        //private object FindCreateMegaRoot(IEnumerable<INode> nodes, string v)
        //{
        //    GetPathTokens();

        //    return null;
        //}

        //private static IEnumerable<string> SplitSubPaths(string fullPath)
        //{
        //    var paths = (fullPath ?? string.Empty).Split(Path.DirectorySeparatorChar).Where(p => !string.IsNullOrWhiteSpace(p));
        //    return paths;
        //}


        //private void BuildNodeTree(IEnumerable<INode> nodes)
        //{

        //    INode root = nodes.Single(x => x.Type == NodeType.Root);



        //}



        //public void Sync(IEnumerable<INode> nodes, string megaRoot, string localRoot)
        //{
        //    FindMegaFolder(nodes, megaRoot);

        //}

        //private INode FindMegaFolder(IEnumerable<INode> nodes, string folderName)
        //{
        //    string parentId =  nodes.FirstOrDefault(x => x.Type == NodeType.Root)?.Id ;
        //    var tokens = folderName.Split('\\');
        //    for (int i = 0; i < tokens.Length; i++)
        //    {
        //        var folderNode = FindMegaFolder(nodes, tokens[i], parentId);
        //        if (folderNode == null)
        //            return null;
        //        if (i == (tokens.Length - 1))
        //            return folderNode;

        //        parentId = folderNode.Id;

        //    }
        //    return null;
        //}

        //private INode FindMegaFolder(IEnumerable<INode> nodes, string folderName, string parentId)
        //{
        //    return nodes.FirstOrDefault(p => p.Type == NodeType.Directory && p.ParentId == parentId && p.Name == folderName);
        //}










        /// <summary>
        /// read a list of path + files in source ini path
        /// </summary>
        private Dictionary<DirectoryInfo, List<FileInfo>> GetLocalFiles(string root)
        {
            //   _logger.Write(string.Format("Scanning {0}...", _ini.SourcePath));

            //var srcPath = @"D:\tmp\mega-photos-test";

            var filesInPath = DirectoryExtension.GetFilesByExt(root, "jpg", ',');
            var sourceFiles = filesInPath.OrderBy(p => p)
                                         .GroupBy(p => Path.GetDirectoryName(p))
                                         .ToDictionary(p => new DirectoryInfo(p.Key), p => p.Select(f => new FileInfo(f)).ToList());

            var lengthSum = filesInPath.Sum(p => new FileInfo(p).Length);
           // _logger.Write(string.Format("{0} folders, {1} files, {2}", sourceFiles.Count, filesInPath.Count, lengthSum.ToFileSize()));

            return sourceFiles;
        }

    }
}
