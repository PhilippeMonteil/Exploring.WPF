
# [DotNet Fundamentals](https://docs.microsoft.com/en-us/dotnet/fundamentals/)

# design time data context

	d:DataContext="{d:DesignInstance IsDesignTimeCreatable=True, Type=local:TodoItemListTest}"

# DataBinding : get accessor

    public class TodoItem
    {
        // get requis pour le data binding
        public string Title { get; init; }
        public int Completion { get; init; }

# ColumnDefinition: percent, ...

    <ColumnDefinition Width="*"/> // ...

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

# [mc:Ignorable](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/mc-ignorable-attribute?view=netframeworkdesktop-4.8)

Specifies which XML namespace prefixes encountered in a markup file may be ignored by a XAML processor.
The mc:Ignorable attribute supports markup compatibility both for custom namespace mapping 
and for XAML versioning.

## Exemple:

    <Window x:Class="Test0.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">

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

# Triggers

    <Trigger SourceName="Bd" Property="IsMouseOver" Value="True">
        <Setter TargetName="Border" Property="Background" Value="{StaticResource Item.MouseOver.Background}" />
        <Setter TargetName="Border" Property="BorderBrush" Value="{StaticResource Item.MouseOver.Border}" />
    </Trigger>

    <MultiTrigger>
        <MultiTrigger.Conditions>
            <Condition Property="IsSelectionActive" Value="False" />
            <Condition Property="IsSelected" Value="True" />
        </MultiTrigger.Conditions>
        <Setter TargetName="Border" Property="Background" Value="{StaticResource Item.SelectedInactive.Background}" />
        <Setter TargetName="Border" Property="BorderBrush" Value="{StaticResource Item.SelectedInactive.Border}" />
    </MultiTrigger>

# String : ',alignement:format'

Console.WriteLine($"i='x{(int)0x12AB,-10:X8}'='{0x12AB,10}'");
Console.WriteLine($"i='{(double)123.0123456789,10:F2}'");
...

# TargetType : / Style, DataTemplate, ControlTemplate

# Commentaires 'intellisense'

Exemple

    /// <summary>
    /// Represents a provider of collection details.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    public interface IItemsProvider<T>
    {
        /// <summary>
        /// Fetches the total number of items available.
        /// </summary>
        /// <returns></returns>
        int FetchCount();

        /// <summary>
        /// Fetches a range of items.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The number of items to fetch.</param>
        /// <returns></returns>
        IList<T> FetchRange(int startIndex, int count);
    }
