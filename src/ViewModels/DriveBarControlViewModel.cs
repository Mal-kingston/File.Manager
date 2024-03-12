namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="DriveBarControl"/>
    /// </summary>
    public class DriveBarControlViewModel : ViewModelBase
    {
        #region Private Fields

        /// <summary>
        /// The size of used up space on the logical drive
        /// </summary>
        public string _usedSpace;

        /// <summary>
        /// The size of un-used up space on the logical drive
        /// </summary>
        public string _unUsedSpace;

        /// <summary>
        /// The current value of the logical drive meter in UI
        /// </summary>
        public string _currentMeterValue;

        #endregion

        #region Pubic Properties

        /// <summary>
        /// The total size of the logical drive
        /// </summary>
        public string TotalDriveSize { get; set; }

        /// <summary>
        /// The maximum range value of the drive meter gauge
        /// </summary>
        public string MaxRange { get; set; }

        /// <summary>
        /// True if percentage should be used to represent the used and un-used spaces on the logical drive
        /// Otherwise that actual size of the spaces will be used
        /// </summary>
        public bool UsePercentage { get; set; }

        /// <summary>
        /// The size of used up space on the logical drive
        /// </summary>
        public string UsedSpace 
        {
            get => _usedSpace;
            set
            {
                // If field isn't up to date
                if(_usedSpace != value)
                    // set to current value
                    _usedSpace = value;
                // Update property value
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The size of un-used up space on the logical drive
        /// </summary>
        public string UnUsedSpace 
        {
            get => _unUsedSpace;
            set
            {
                // If field isn't up to date
                if(_unUsedSpace != value)
                    // set to current value
                    _unUsedSpace = value;
                // Update property value
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The current value of the logical drive meter in UI
        /// </summary>
        public string CurrentMeterValue
        {
            get => _currentMeterValue;
            set
            {
                // If field isn't up to date
                if(_currentMeterValue != value)
                    // set to current value
                    _currentMeterValue = value;
                // Update property value
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DriveBarControlViewModel()
        {
            // Set default field values
            _usedSpace = string.Empty; 
            _unUsedSpace = string.Empty;
            _currentMeterValue = string.Empty;
            TotalDriveSize = string.Empty;
            MaxRange = string.Empty;
        }

        #endregion
    }
}
