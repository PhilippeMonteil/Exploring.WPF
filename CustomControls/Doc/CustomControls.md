
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

# CustomControl1

- en résumé :

    - Name="PART_Browse"
    - [TemplatePart(Name = "PART_Browse", Type = typeof(Button))]
    - OnApplyTemplate / GetTemplateChild

- Generic.xaml :

````
    <Button x:Name="PART_Browse" Content="Browse ..." 
                DockPanel.Dock="Right" />
    <TextBlock x:Name="theTextBox" Margin="0,0,2,0" HorizontalAlignment="Center" VerticalAlignment="Center"
                Text="{Binding FileName, RelativeSource={RelativeSource AncestorType=local:CustomControl1}}"/>
````

- Source code :
 
````
    [TemplatePart(Name = "PART_Browse", Type = typeof(Button))]
    public class CustomControl1 : Control
    {
        static DependencyProperty FileNameProperty = DependencyProperty.Register(nameof(FileName), 
                                                                                    typeof(string), 
                                                                                    typeof(CustomControl1),
                                                                                    new PropertyMetadata(null, propertyChangedCallback));
        static RoutedEvent FileNameChangedEvent = EventManager.RegisterRoutedEvent(nameof(FileNameChanged),
                                                                RoutingStrategy.Bubble,
                                                                typeof(RoutedEventHandler), 
                                                                typeof(CustomControl1));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Button _button = base.GetTemplateChild("PART_Browse") as Button;
            if (_button != null)
            {
                _button.Click += _button_Click;
            }
        }

````

# CustomControl2

- en résumé

- generic.xaml :

````
     <Button x:Name="PART_Browse" Content="Browse ..." 
                 DockPanel.Dock="Right" 
             Command="{x:Static local:CustomControl2.BrowseCommand}"/>
````

- Source code :

     static RoutedUICommand browseCommand;

     browseCommand = new RoutedUICommand("Browse ...", "BrowseCommand", 
                              typeof(CustomControl2));

     CommandManager.RegisterClassCommandBinding(typeof(CustomControl2),
                              new CommandBinding(browseCommand, browseCommandHandler));

     static void browseCommandHandler(object sender, ExecutedRoutedEventArgs e)
     {
         (sender as CustomControl2)?.browseCommandHandler(e);
     }

     void browseCommandHandler(ExecutedRoutedEventArgs e)
     {
         this.FileName += "+";
     }

