using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="DirectoryItemControl"/>
    /// </summary>
    public class DirectoryItemControlViewModel : ViewModelBase
    {
        /// <summary>
        /// The name of directory
        /// </summary>
        public string DirectoryName { get; set; } = string.Empty;

        /// <summary>
        /// Date this directory was last accessed
        /// </summary>
        public DateTime LastDateAccessed { get; set; }

        /// <summary>
        /// Date this directory was last written to or made changes to
        /// </summary>
        public string LastDateModified { get; set; } = string.Empty;

        /// <summary>
        /// The type of this directory
        /// </summary>
        public string DirectoryItemType { get; set; } = string.Empty;

        /// <summary>
        /// The size of this directory
        /// </summary>
        public string SizeOfDirectoryItem { get; set; } = string.Empty;
        
        /// <summary>
        /// The full path of this directory
        /// </summary>
        public string FullPath { get; set; } = string.Empty;
    }
}
