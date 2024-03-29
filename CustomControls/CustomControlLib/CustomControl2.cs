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

namespace CustomControlLib
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomControlLib"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomControlLib;assembly=CustomControlLib"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl2/>
    ///
    /// </summary>
    public class CustomControl2 : Control
    {
        static DependencyProperty FileNameProperty = DependencyProperty.Register(nameof(FileName),
                                                            typeof(string),
                                                            typeof(CustomControl2),
                                                            new PropertyMetadata(null, onFileNameChanged));

        static RoutedEvent FileNameChangedEvent = EventManager.RegisterRoutedEvent(nameof(FileNameChanged),
                                                            RoutingStrategy.Bubble,
                                                            typeof(RoutedEventHandler),
                                                            typeof(CustomControl2));

        static RoutedUICommand browseCommand;

        public static RoutedUICommand BrowseCommand => browseCommand;

        static CustomControl2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl2), 
                                                    new FrameworkPropertyMetadata(typeof(CustomControl2)));

            browseCommand = new RoutedUICommand("Browse ...", "BrowseCommand", 
                                                typeof(CustomControl2));

            CommandManager.RegisterClassCommandBinding(typeof(CustomControl2),
                                                new CommandBinding(browseCommand, browseCommandHandler));

            //CommandManager.RegisterClassInputBinding(typeof(CustomControl2), 
            //                                        new InputBinding(browseCommand, 
            //                                                            new MouseGesture(MouseAction.LeftClick)));
        }

        static void browseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            (sender as CustomControl2)?.browseCommandHandler(e);
        }

        void browseCommandHandler(ExecutedRoutedEventArgs e)
        {
            this.FileName += "+";
        }

        static void onFileNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CustomControl2)?.onFileNameChanged(e);
        }

        void onFileNameChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        public string FileName
        {
            get
            {
                return GetValue(FileNameProperty) as string;
            }
            set
            {
                SetValue(FileNameProperty, value);
            }
        }

        public event RoutedEventHandler FileNameChanged
        {
            add
            {
                this.AddHandler(FileNameChangedEvent, value);
            }
            remove
            {
                this.RemoveHandler(FileNameChangedEvent, value);
            }
        }

    }

}
