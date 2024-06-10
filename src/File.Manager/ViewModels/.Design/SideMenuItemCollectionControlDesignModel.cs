using System.Collections.ObjectModel;

namespace File.Manager
{
    /// <summary>
    /// Design time data of a <see cref="SideMenuItemCollectionControl"/>
    /// </summary>
    public class SideMenuItemCollectionControlDesignModel : SideMenuItemCollectionControlViewModel
    {
        /// <summary>
        /// Event that gets fired when this item gets selected in the view
        /// </summary>
        private readonly SelectionChangedEvent _selectionEvent = new SelectionChangedEvent();

        /// <summary>
        /// A singleton static instance of this class
        /// </summary>
        public static SideMenuItemCollectionControlDesignModel Instance => new SideMenuItemCollectionControlDesignModel();

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuItemCollectionControlDesignModel()  
        {
            // Design data
            Items = new ObservableCollection<SideMenuItemControlViewModel>
            {
                new SideMenuItemControlViewModel(_selectionEvent)
                {
                    IconType = IconType.Folder,
                    DirectoryName = "Documents",
                },
                new SideMenuItemControlViewModel(_selectionEvent)
                {
                    IconType = IconType.Music,
                    DirectoryName = "Music",
                },
                new SideMenuItemControlViewModel(_selectionEvent)
                {
                    IconType = IconType.Videos,
                    DirectoryName = "Videos",
                },
                new SideMenuItemControlViewModel(_selectionEvent)
                {
                    IconType = IconType.Downloads,
                    DirectoryName = "Downloads",
                },
                new SideMenuItemControlViewModel(_selectionEvent)
                {
                    IconType = IconType.Folder,
                    DirectoryName = "Github",
                },
            };
        }
    }
}
