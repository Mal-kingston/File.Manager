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
        public static DirectoryControlDesignModel Instance => new DirectoryControlDesignModel(new SelectionChangedEvent());

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryControlDesignModel(SelectionChangedEvent selectionEvent) : base(selectionEvent)
        {
            // Set data to use
            DirectoryName = "Documents";
            IconType = IconType.Folder;
        }
    }
}
