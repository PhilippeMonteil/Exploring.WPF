﻿using ItemsControlLib;
using System.Windows;

namespace Test0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ItemsControl1.ItemsSource = new TodoItemListTest();
        }

    }
}
