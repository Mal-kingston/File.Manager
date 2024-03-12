namespace File.Manager
{
    /// <summary>
    /// Design time data of a <see cref="SideMenuItemControl"/>
    /// </summary>
    public class SideMenuItemControlDesignModel : SideMenuItemControlViewModel
    {
        /// <summary>
        /// A singleton static instance of this class
        /// </summary>
        public static SideMenuItemControlDesignModel Instance => new SideMenuItemControlDesignModel(new SelectionChangedEvent());

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuItemControlDesignModel(SelectionChangedEvent selectionEvent) : base(selectionEvent)
        {
            // Set data to use
            DirectoryName = "Documents";
            IconType = IconType.Folder;
        }
    }
}
