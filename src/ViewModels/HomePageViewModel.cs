using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Manager
{
    /// <summary>
    /// View model for the <see cref="HomePage"/> of this application
    /// </summary>
    public class HomePageViewModel : ViewModelBase
    {
        /// <summary>
        /// Tabs
        /// <remark>Used to organize logical drives and their associated data to their very own tab</remark>
        /// </summary>
        public ObservableCollection<TabItemControlViewModel>  Tabs { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public HomePageViewModel()
        {
            // Set default properties
            Tabs = new ObservableCollection<TabItemControlViewModel> 
            {
                // Replace with real data
                new TabItemControlViewModel { Header = "Windows (C:)", Content= "Tab1" },
                new TabItemControlViewModel { Header = "something", Content= "Tab2" },
            };
        }
    }
}
