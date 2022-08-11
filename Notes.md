
# design time data context

	d:DataContext="{d:DesignInstance IsDesignTimeCreatable=True, Type=local:TodoItemListTest}"

# DataBinding : get accessor

    public class TodoItem
    {
        // get requis pour le data binding
        public string Title { get; init; }
        public int Completion { get; init; }

# ColumnDefinition: percent, ...

    <ColumnDefinition Width="0.5*"/>

# ListBox : selected item aspect

    <ListBox.ItemContainerStyle>
        
        <Style TargetType="ListBoxItem">
            <Setter Property="Opacity" Value="0.5" />
            <Setter Property="MaxHeight" Value="75" />
            <Style.Triggers>
                <Trigger Property="IsSelected" Value="True">
                    <Trigger.Setters>
                        <Setter Property="Opacity" Value="1.0" />
                    </Trigger.Setters>
                </Trigger>
            </Style.Triggers>
        </Style>

    </ListBox.ItemContainerStyle>

# [Control Library](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/controls/control-library?view=netframeworkdesktop-4.8)

# [WPF Partial Trust Security](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/wpf-partial-trust-security?view=netframeworkdesktop-4.8)
