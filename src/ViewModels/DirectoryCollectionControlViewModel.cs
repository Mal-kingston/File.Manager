using System.Collections.ObjectModel;

namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="DirectoryCollectionControl"/> of this application
    /// </summary>
    public class DirectoryCollectionControlViewModel : ViewModelBase
    {
        /// <summary>
        /// List of directories <see cref="DirectoryControl"/>
        /// </summary>
        private ObservableCollection<DirectoryControlViewModel> _directoryItems;

        /// <summary>
        /// List of directories <see cref="DirectoryControl"/>
        /// </summary>
        public ObservableCollection<DirectoryControlViewModel> DirectoryItems
        {
            // Get view model
            get => _directoryItems;
            set
            {
                // If value aren't the same...
                if(_directoryItems != value)
                    // Make value same
                    _directoryItems = value;
                // Update this property
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryCollectionControlViewModel()
        {
            // Set default properties
            _directoryItems = new ObservableCollection<DirectoryControlViewModel>();
        }
    }
}
