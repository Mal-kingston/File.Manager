using System.Windows.Input;

namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="DirectoryControl"/> of this application
    /// </summary>
    public class DirectoryControlViewModel : ViewModelBase
    {
        /// <summary>
        /// Default directory name
        /// </summary>
        private string _directoryName = "New Folder";

        /// <summary>
        /// The name of this directory
        /// </summary>
        public string DirectoryName 
        {
            // Get field value
            get => _directoryName;
            set
            {
                // if value doesn't match field...
                if (_directoryName != value)
                    // Set field to match current value
                    _directoryName = value;
                // Update property
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The Current icon type of this directory
        /// </summary>
        public IconType IconType { get; set; }

        /// <summary>
        /// Modifies this directory item properties
        /// [ Rename | Change directory icon | Pin or unpin from quick-access collection ]
        /// </summary>
        public ICommand EditCommand { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryControlViewModel()
        {

            // Set data to use
            DirectoryName = "Documents";
            IconType = IconType.Folder;
            // Create commands
            EditCommand = new RelayCommand(EditDirectory, canExecuteCommand => this != null); 
        }

        /// <summary>
        /// Modifies this directory item properties
        /// [ Rename | Change directory icon | Pin or unpin from quick-access collection ]
        /// </summary>
        private void EditDirectory()
        {
            throw new NotImplementedException();
        }
    }
}
