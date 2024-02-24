namespace File.Manager
{
    /// <summary>
    /// Design time data of a <see cref="DirectoryControl"/>
    /// </summary>
    public class DirectoryControlDesignModel : DirectoryControlViewModel
    {
        /// <summary>
        /// A singleton static instance of this class
        /// </summary>
        public static DirectoryControlViewModel Instance => new DirectoryControlViewModel();

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryControlDesignModel()
        {
            // Set data to use
            DirectoryName = "Documents";
            IconType = IconType.Folder;
        }
    }
}
