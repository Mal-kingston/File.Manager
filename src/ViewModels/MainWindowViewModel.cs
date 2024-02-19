using System.Windows;
using System.Windows.Input;

namespace File.Manager
{
    /// <summary>
    /// View model for the main window of this application
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Members

        /// <summary>
        /// The size of padding around the window to show drops-shadow
        /// </summary>
        private double _outerPadding = 10;

        /// <summary>
        /// The size of padding of contents of the window
        /// </summary>
        private double _innerPadding = 4;

        /// <summary>
        /// size of caption height of this window
        /// </summary>
        private double _captionHeight = 40;

        /// <summary>
        /// The height of footer of this application 
        /// </summary>
        private double _footerHeight = 22;

        /// <summary>
        /// The size of borders around the window to allow user to click and drag to resize the window
        /// </summary>
        private double _resizeBorder = 6;

        #endregion

        #region Public Properties

        /// <summary>
        /// The size of padding around the window to show drops-shadow
        /// </summary>
        public double OuterPadding
        {
            get => _outerPadding;
            set
            {
                if(_outerPadding != value) 
                    _outerPadding = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The size of padding of contents of the window
        /// </summary>
        public double InnerPadding
        {
            get => _innerPadding;
            set
            {
                if (_innerPadding != value)
                    _innerPadding = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// size of caption height of this window
        /// </summary>
        public double CaptionHeight
        {
            get => _captionHeight;
            set
            {
                if (_captionHeight != value)
                    _captionHeight = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// size of footer height of this window
        /// </summary>
        public double FooterHeight
        {
            get => _footerHeight;
            set
            {
                if (_footerHeight != value)
                    _footerHeight = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The size of borders around the window to allow user to click and drag to resize the window
        /// </summary>
        public double ResizeBorder
        {
            get => _resizeBorder;
            set
            {
                if (_resizeBorder != value)
                    _resizeBorder = value;

                OnPropertyChanged();
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// Command to minimize the main window
        /// </summary>
        public ICommand WindowMinimizeCommand { get; set; }

        /// <summary>
        /// Command to maximize the main window
        /// </summary>
        public ICommand WindowMaximizeCommand { get; set; }

        /// <summary>
        /// Command to close the main window
        /// </summary>
        public ICommand WindowCloseCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindowViewModel()
        {
            // The main window
            var window = Application.Current.MainWindow;

            // Create commands
            WindowMinimizeCommand = new RelayCommand(() => window.WindowState = WindowState.Minimized, canExecuteCommand => window.WindowState != WindowState.Minimized);
            WindowMaximizeCommand = new RelayCommand(() => window.WindowState ^= WindowState.Maximized, canExecuteCommand => window.WindowState != WindowState.Minimized);
            WindowCloseCommand = new RelayCommand(window.Close, canExecuteCommand => true);
        }

        #endregion

    }
}
