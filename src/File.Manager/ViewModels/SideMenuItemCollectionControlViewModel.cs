using System.Collections.ObjectModel;

namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="SideMenuItemCollectionControl"/> of this application
    /// </summary>
    public class SideMenuItemCollectionControlViewModel : ViewModelBase
    {
        /// <summary>
        /// List of directories <see cref="DirectoryControl"/>
        /// </summary>
        private ObservableCollection<SideMenuItemControlViewModel> _items;

        /// <summary>
        /// List of directories <see cref="DirectoryControl"/>
        /// </summary>
        public ObservableCollection<SideMenuItemControlViewModel> Items
        {
            // Get view model
            get => _items;
            set
            {
                // If value aren't the same...
                if(_items != value)
                    // Make value same
                    _items = value;
                // Update this property
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuItemCollectionControlViewModel()
        {
            // Set default properties
            _items = new ObservableCollection<SideMenuItemControlViewModel>();
        }
    }
}
