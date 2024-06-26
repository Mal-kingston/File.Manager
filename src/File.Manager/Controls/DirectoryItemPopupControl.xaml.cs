﻿using System;
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
    /// Interaction logic for DirectoryItemPopupControl.xaml
    /// </summary>
    public partial class DirectoryItemPopupControl : UserControl
    {
        public DirectoryItemPopupControl()
        {
            InitializeComponent();

            LostFocus += (s, e) =>
            {
                // Reset any existing popup or selected directory item
                ServiceLocator.AppViewModel.ResetDirectoryItemPopupAndSelection();
            };
        }

    }
}
