﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Management;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using static File.Manager.DirectoryHelper;

namespace File.Manager
{
    /// <summary>
    /// View model for the <see cref="HomePage"/> of this application
    /// </summary>
    public class HomePageViewModel : ViewModelBase
    {
        #region Private Fields

        /// <summary>
        /// Tabs
        /// <remark>
        /// Used to organize logical drives and their associated data to their very own tab</remark>
        /// </summary>
        private ObservableCollection<TabItemModel> _tabs;

        /// <summary>
        /// The basic / common drive analysis
        /// </summary>
        private ObservableCollection<DriveStorageAnalysisViewModel> _driveFilesAnalysis;

        /// <summary>
        /// View model for <see cref="DriveBarControl"/>
        /// </summary>
        private DriveBarControlViewModel _driveBarControlVM;


        private ObservableCollection<DirectoryInfo> _recentDirectories;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Tabs    
        /// <remark>
        /// Used to organize logical drives and their associated data to their very own tab</remark>
        /// </summary>
        public ObservableCollection<TabItemModel> Tabs 
        {
            get => _tabs;
            set
            {
                // If tabs value are not the same...
                if (_tabs != value)
                    // Set tabs value 
                    _tabs = value;
                // Update properties
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The basic / common drive analysis
        /// </summary>
        public ObservableCollection<DriveStorageAnalysisViewModel> DriveFilesAnalysis
        {
            get => _driveFilesAnalysis;
            set
            {
                // If tabs value are not the same...
                if (_driveFilesAnalysis != value)
                    // Set tabs value 
                    _driveFilesAnalysis = value;
                // Update properties
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// View model for the drive bar
        /// </summary>
        public DriveBarControlViewModel DriveBarControlVM
        {
            get => _driveBarControlVM;
            set
            {
                // If tabs value are not the same...
                if (_driveBarControlVM != value)
                    // Set tabs value 
                    _driveBarControlVM = value;
                // Update properties
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DirectoryInfo> RecentDirectories
        {
            get => _recentDirectories;
            set
            {
                // If value are not the same...
                if (_recentDirectories != value)
                    // Set value 
                    _recentDirectories = value;
                // Update properties
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The information about a drive
        /// </summary>
        public Task DriveAnalysisTask { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HomePageViewModel()
        {
            // Set field defaults
            _driveBarControlVM = new DriveBarControlViewModel();
            _tabs = new ObservableCollection<TabItemModel>();
            _driveFilesAnalysis = new ObservableCollection<DriveStorageAnalysisViewModel>();
            _recentDirectories = new ObservableCollection<DirectoryInfo>();

            // Sets up logical drive
            SetupLogicalDriveUsedAndUnUsedSpaces();
            DriveAnalysisTask = LogicalDriveAnalysisAsync();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets up logical drive
        /// </summary>
        private void SetupLogicalDriveUsedAndUnUsedSpaces()
        {
            // Go through each drive on the machine
            Parallel.ForEach(DriveInfo.GetDrives(), drive =>
            {
                // Make sure drive is available
                if (drive.IsReady)
                {
                    // Convert drive values from byte to gigabyte
                    _driveBarControlVM.TotalDriveSize = ConvertByte(drive.TotalSize, 2);
                    _driveBarControlVM.MaxRange = ConvertByte(drive.TotalSize, 2, getJustTheValue: true);
                    _driveBarControlVM.CurrentMeterValue = ConvertByte(drive.TotalSize - drive.AvailableFreeSpace, 2, getJustTheValue: true);
                    _driveBarControlVM.UsedSpace = ConvertByte(drive.TotalSize - drive.AvailableFreeSpace, 2);
                    _driveBarControlVM.UnUsedSpace = ConvertByte(drive.AvailableFreeSpace, 2);

                    // Create label for each drive 
                    var label = GetLogicalDriveVolumeLabel(drive);
                    // Add each drive to tab item.
                    _tabs.Add(new TabItemModel { Header = label, Content = _driveBarControlVM });

                    // Reset our item view model
                    _driveBarControlVM = new DriveBarControlViewModel();
                }

            });
        }

        /// <summary>
        /// Analyze logical drive
        /// </summary>
        private async Task LogicalDriveAnalysisAsync()
        {
            // Create pictures category
            var pictures = new DriveStorageAnalysisViewModel()
            {
                IconType = IconType.Pictures,
                FileType = FileType.Pictures,
            };

            // Create videos category
            var videos = new DriveStorageAnalysisViewModel()
            {
                IconType = IconType.Videos,
                FileType = FileType.Videos,
            };

            // Create music category
            var music = new DriveStorageAnalysisViewModel()
            {
                IconType = IconType.Music,
                FileType = FileType.Music,
            };

            // Create documents category
            var documents = new DriveStorageAnalysisViewModel()
            {
                IconType = IconType.Documents,
                FileType = FileType.Documents,
            };

            // Create temporary category
            var temporaryFiles = new DriveStorageAnalysisViewModel()
            {
                IconType = IconType.Folder,
                FileType = FileType.TemporaryFiles,
            };

            // Create installed apps category
            var installedApps = new DriveStorageAnalysisViewModel()
            {
                IconType = IconType.Folder,
                FileType = FileType.InstalledApps,
            };

            // add categories to the list
            _driveFilesAnalysis.Add(music);
            _driveFilesAnalysis.Add(pictures);
            _driveFilesAnalysis.Add(videos);
            _driveFilesAnalysis.Add(documents);
            _driveFilesAnalysis.Add(temporaryFiles);
            _driveFilesAnalysis.Add(installedApps);

            // Get total sizes for each categories
            var musicFilesTask = GetDirectorySizeAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            var pictureFilesTask = GetDirectorySizeAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            var videoFilesTask = GetDirectorySizeAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            var documentFilesTask = GetDirectorySizeAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            var temporaryFilesTask = GetDirectorySizeAsync(Environment.GetEnvironmentVariable("TEMP") ?? string.Empty);
            var installedFilesTask = GetInstalledAppsTotalSizeAsync();

            // Wait for result to complete
            var result = await Task.WhenAny(pictureFilesTask, videoFilesTask, musicFilesTask, documentFilesTask, temporaryFilesTask, installedFilesTask);
            // Run on thread
            await Task.Run(() =>
            {
                // Set total sizes to their respective categories
                music.TotalSizeOnDrive = musicFilesTask.Result;
                pictures.TotalSizeOnDrive = pictureFilesTask.Result;
                videos.TotalSizeOnDrive = videoFilesTask.Result;
                documents.TotalSizeOnDrive = documentFilesTask.Result;
                temporaryFiles.TotalSizeOnDrive = temporaryFilesTask.Result;
                installedApps.TotalSizeOnDrive = installedFilesTask.Result;
            });

            // Refresh drive files analysis collection to update it with the latest data
            CollectionViewSource.GetDefaultView(DriveFilesAnalysis).Refresh();
        }

        #endregion
    }
}
