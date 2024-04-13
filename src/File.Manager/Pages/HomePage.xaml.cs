using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace File.Manager
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            // Set data context
            DataContext = ViewModelLocator.HomePageViewModel;

            // Listen out for when tab control is first loaded into view
            TabControl.Loaded += (sender, e) =>
            {
                // Make sure the first tab item is selected 
                if(TabControl.SelectedIndex.Equals(-1))
                    TabControl.SelectedIndex = 0;
            };
        }
    }
}
