
# CustomControl

- new UserControl
- MyUserControl

- XAML

        x:Name="Root"

        <TextBlock x:Name="theTextBox" Margin="0,0,2,0" HorizontalAlignment="Center" VerticalAlignment="Center"
                   Text="{Binding FileName, ElementName=Root}"/>

- Source

````
    [ContentProperty(nameof(FileName))]
    public partial class MyUserControl : UserControl
    {
        static DependencyProperty FileNameProperty = DependencyProperty.Register(nameof(FileName),
                                                        typeof(string),
                                                        typeof(MyUserControl),
                                                        new PropertyMetadata(null, fileName_ChangedCallback));

        static RoutedEvent FileNameChangedEvent = EventManager.RegisterRoutedEvent("FileNameChanged",
                                                        RoutingStrategy.Bubble,
                                                        typeof(RoutedEventHandler),
                                                        typeof(MyUserControl));
````

        static void fileName_ChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MyUserControl)?._fileName_ChangedCallback(e);
        }

        void _fileName_ChangedCallback(DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(FileNameChangedEvent);
            RaiseEvent(args);
        }

        public string FileName
        {
            get
            {
                return this.GetValue(FileNameProperty) as string;
            }
            set
            {
                this.SetValue(FileNameProperty, value);
            }
        }

        public event RoutedEventHandler FileNameChanged
        {
            add
            {
                AddHandler(FileNameChangedEvent, value);
            }
            remove
            {
                RemoveHandler(FileNameChangedEvent, value);
            }
        }
