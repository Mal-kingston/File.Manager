using System.Collections.ObjectModel;

namespace File.Manager
{
    /// <summary>
    /// Design time data of a <see cref="DirectoryCollectionControl"/>
    /// </summary>
    public class DirectoryCollectionControlDesignModel : DirectoryCollectionControlViewModel
    {
        /// <summary>
        /// A singleton static instance of this class
        /// </summary>
        public static DirectoryCollectionControlDesignModel Instance => new DirectoryCollectionControlDesignModel();

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryCollectionControlDesignModel() 
        {
            // Actual data
            DirectoryItems = new ObservableCollection<DirectoryControlViewModel>
            {
                new DirectoryControlViewModel
                {
                    IconType = IconType.Folder,
                    DirectoryName = "Documents",
                },
                new DirectoryControlViewModel
                {
                    IconType = IconType.Music,
                    DirectoryName = "Musics",
                },
                new DirectoryControlViewModel
                {
                    IconType = IconType.Videos,
                    DirectoryName = "Videos",
                },
                new DirectoryControlViewModel
                {
                    IconType = IconType.Downloads,
                    DirectoryName = "Downloads",
                },
                new DirectoryControlViewModel
                {
                    IconType = IconType.Folder,
                    DirectoryName = "Github",
                },
            };
        }
    }
}
