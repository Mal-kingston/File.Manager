using System.Collections.ObjectModel;

namespace File.Manager
{
    /// <summary>
    /// Design time data of a <see cref="DirectoryCollectionControl"/>
    /// </summary>
    public class DirectoryCollectionControlDesignModel : DirectoryCollectionControlViewModel
    {
        /// <summary>
        /// Event that gets fired when this item gets selected in the view
        /// </summary>
        private readonly SelectionChangedEvent _selectionEvent = new SelectionChangedEvent();

        /// <summary>
        /// A singleton static instance of this class
        /// </summary>
        public static DirectoryCollectionControlDesignModel Instance => new DirectoryCollectionControlDesignModel();

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryCollectionControlDesignModel()  
        {
            // Actual design data
            DirectoryItems = new ObservableCollection<DirectoryControlViewModel>
            {
                new DirectoryControlViewModel(_selectionEvent)
                {
                    IconType = IconType.Folder,
                    DirectoryName = "Documents",
                },
                new DirectoryControlViewModel(_selectionEvent)
                {
                    IconType = IconType.Music,
                    DirectoryName = "Musics",
                },
                new DirectoryControlViewModel(_selectionEvent)
                {
                    IconType = IconType.Videos,
                    DirectoryName = "Videos",
                },
                new DirectoryControlViewModel(_selectionEvent)
                {
                    IconType = IconType.Downloads,
                    DirectoryName = "Downloads",
                },
                new DirectoryControlViewModel(_selectionEvent)
                {
                    IconType = IconType.Folder,
                    DirectoryName = "Github",
                },
            };
        }
    }
}
