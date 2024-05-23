﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace File.Manager
{
    /// <summary>
    /// The view model for this application
    /// </summary>
    public class ApplicationViewModel : ViewModelBase
    {
        /// <summary>
        /// The navigation service
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// The number items loaded into view
        /// </summary>
        private string _numberOfItemsInView;

        /// <summary>
        /// The page currently in view
        /// </summary>
        public ApplicationPages CurrentPage 
        {
            get => _navigationService.CurrentPage;
            set
            {
                // If current page is not up to date...
                if (_navigationService.CurrentPage != value)
                    // Set current page
                    _navigationService.CurrentPage = value;
                // Update property
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The number items loaded into view
        /// </summary>
        public string NumberOfItemsInView 
        { 
            get => _numberOfItemsInView;
            set
            {
                // Set value
                _numberOfItemsInView = value;

                // Update property
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The layout options of directories
        /// 
        /// TODO: Each directory will remember what layout it used last
        /// </summary>
        public DirectoryItemsViewLayout DirectoryItemsViewLayout { get; set; }

        public bool IsViewLayoutSetToGrid { get; private set; }

        public bool IsViewLayoutSetToList { get; private set; }

        //public ICommand ListViewLayoutCommand { get; set; }
        //public ICommand GridViewLayoutCommand { get; set; }

        public ApplicationViewModel(INavigationService navigationService)
        {
            // Initialize
            IsViewLayoutSetToList = true;
            _navigationService = navigationService;
            //_navigationService.CurrentPage = ApplicationPages.Home;
            _numberOfItemsInView = "0 items(s)";

            // Event hookup
            _navigationService.NewPageRequested += (sender, e) => OnPropertyChanged(nameof(CurrentPage));

            //GridViewLayoutCommand = new RelayCommand(ViewLayout, canExecuteCommand:(o) => !this.Equals(null));
            //ListViewLayoutCommand = new RelayCommand(ViewLayout, canExecuteCommand:(o) => !this.Equals(null));
        }
    }
}
