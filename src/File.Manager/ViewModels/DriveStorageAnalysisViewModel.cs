using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Manager
{
    public class DriveStorageAnalysisViewModel : ViewModelBase
    {
        /// <summary> 
        /// The icon of a specific file type
        /// </summary>
        public IconType IconType { get; set; } 

        /// <summary>
        /// The type of a directory item
        /// </summary>
        public FileType FileType { get; set; }

        /// <summary>
        /// The total space a specific file type takes up on drive
        /// </summary>
        public string TotalSizeOnDrive { get; set; } = string.Empty;

        /// <summary>
        /// The raw size this category in byte
        /// <remark>
        /// Used in sorting categories in a descending order by how much space they occupy on drive
        /// </remark>
        /// </summary>
        public double RawTotalSizeOnDriveData { get => DirectoryHelper.ConvertValueToByte(TotalSizeOnDrive); private set { } }

    }
}
