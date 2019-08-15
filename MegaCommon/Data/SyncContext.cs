using CG.Web.MegaApiClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCommon.Data
{
    // vstup: Dictionary<DirectoryInfo, List<FileInfo>>

    public class SyncContext
    {
        public List<SyncDirectory> SyncDirectories = new List<SyncDirectory>();

        public SyncContext(IEnumerable<INode> nodes, string megaRoot, string localRoot, List<FileInfo> localFiles )
        {

        }
    }

    public class SyncDirectory
    {
        public INode MegaNode;
        public DirectoryInfo Directory { get; set; }

        public List<SyncFile> files;
    }

    public class SyncFile
    {
        public INode MegaFile;
        public FileInfo File;
    }
}
