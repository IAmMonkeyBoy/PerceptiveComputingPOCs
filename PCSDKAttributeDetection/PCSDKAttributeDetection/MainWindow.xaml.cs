﻿using System.Windows;
using PCSDKAttributeDetection.Perceptual;
using PCSDKAttributeDetection.ViewModel;
using PCSDKAttributeDetection.Logging;

namespace PCSDKAttributeDetection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();

        }

        
    }
}