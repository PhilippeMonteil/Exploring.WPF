﻿using ResourceAssembly;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace TestResources0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Class1.Test0();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imTest.Source = new BitmapImage(new Uri("pack://application:,,,/ResourceAssembly;Component/Folder0/Image0.jpg"));


            {
                Debug.WriteLine($"Properties.Resources.String1={Properties.Resources.String1}");
            }

            System.Threading.Thread.CurrentThread.CurrentUICulture =
                        new System.Globalization.CultureInfo("es");

            Properties.Resources.Culture = new System.Globalization.CultureInfo("es");

            {
                Debug.WriteLine($"Properties.Resources.String1={Properties.Resources.String1}");
            }

        }
    }
}
