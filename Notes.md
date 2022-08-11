
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

# TreeView, TreeViewItem

	TreeView : ItemsControl
		TreeViewItem[] Items

	TreeViewItem : HeaderedItemsControl
		TreeViewItem[] Items

# [mc:Ignorable](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/mc-ignorable-attribute?view=netframeworkdesktop-4.8)

Specifies which XML namespace prefixes encountered in a markup file may be ignored by a XAML processor.
The mc:Ignorable attribute supports markup compatibility both for custom namespace mapping 
and for XAML versioning.

# XmlDataProvider

Exemple:

    <StackPanel Background="Cornsilk">

        <StackPanel.DataContext>
            <XmlDataProvider XPath="Inventory/Books">
                <x:XData>
                    <Inventory xmlns="">
                        <Books>
                            <Book ISBN="0-7356-0562-9" Stock="in" Number="9">
                                <Title>XML in Action</Title>
                                <Summary>XML Web Technology</Summary>
                            </Book>
                            <Book ISBN="0-7356-1370-2" Stock="in" Number="8">
                                <Title>Programming Microsoft Windows With C#</Title>
                                <Summary>C# Programming using the .NET Framework</Summary>
                            </Book>
                            <Book ISBN="0-7356-1288-9" Stock="out" Number="7">
                                <Title>Inside C#</Title>
                                <Summary>C# Language Programming</Summary>
                            </Book>
                            <Book ISBN="0-7356-1377-X" Stock="in" Number="5">
                                <Title>Introducing Microsoft .NET</Title>
                                <Summary>Overview of .NET Technology</Summary>
                            </Book>
                            <Book ISBN="0-7356-1448-2" Stock="out" Number="4">
                                <Title>Microsoft C# Language Specifications</Title>
                                <Summary>The C# language definition</Summary>
                            </Book>
                        </Books>
                        <CDs>
                            <CD Stock="in" Number="3">
                                <Title>Classical Collection</Title>
                                <Summary>Classical Music</Summary>
                            </CD>
                            <CD Stock="out" Number="9">
                                <Title>Jazz Collection</Title>
                                <Summary>Jazz Music</Summary>
                            </CD>
                        </CDs>
                    </Inventory>
                </x:XData>
            </XmlDataProvider>
        </StackPanel.DataContext>

        <TextBlock FontSize="18" FontWeight="Bold" Margin="10" HorizontalAlignment="Center">
            XML Data Source Sample
        </TextBlock>
        
        <ListBox Width="400" Height="300" Background="Honeydew">
            
            <ListBox.ItemsSource>
                <Binding XPath="*[@Stock='out'] | *[@Number>=8 or @Number=3]"/>
            </ListBox.ItemsSource>

            <ListBox.ItemTemplate>
                <DataTemplate>
                    <TextBlock FontSize="12" Foreground="Red">
                        <TextBlock.Text>
                            <Binding XPath="Title"/>
                        </TextBlock.Text>
                    </TextBlock>
                </DataTemplate>
            </ListBox.ItemTemplate>
            
        </ListBox>
        
    </StackPanel>
