
# [ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.itemscontrol?view=windowsdesktop-6.0)


## Class
 
	public class __ItemsControl__ : System.Windows.Controls.Control, 
                                System.Windows.Controls.Primitives.IContainItemStorage, 
                                System.Windows.Markup.IAddChild
    {

## Default template : cf ControlTemplate

    <Style x:Key="{x:Type ItemsControl}"
           TargetType="{x:Type ItemsControl}">
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="{x:Type ItemsControl}">
                    <Border Background="{TemplateBinding Background}"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}"
                            Padding="{TemplateBinding Padding}"
                            SnapsToDevicePixels="true">
                        <ItemsPresenter SnapsToDevicePixels="{TemplateBinding SnapsToDevicePixels}"/>
                    </Border>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>

## Fields

    public static readonly System.Windows.DependencyProperty __ItemsSourceProperty__;
    ...

## properties:

    [System.ComponentModel.Bindable(true)]
    public System.Collections.IEnumerable __ItemsSource__ { get; set; }

    [System.ComponentModel.Bindable(true)]
    public System.Windows.DataTemplate __ItemTemplate__ { get; set; }

    [System.ComponentModel.Bindable(false)]
    public System.Windows.Controls.[ItemsPanelTemplate](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.itemspaneltemplate?view=windowsdesktop-6.0) __ItemsPanel__ { get; set; }

    // The Style that is applied to the container element generated for each item. 
    [System.ComponentModel.Bindable(true)]
    public System.Windows.Style ItemContainerStyle { get; set; }

## [ItemsPresenter](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.itemspresenter?view=windowsdesktop-6.0)

> Used __within the template of an item control__ to specify the place in the control's visual tree where 
the __ItemsPanel__ defined by the ItemsControl is to be added.

## Exemple: __ItemsPanel__

    <ItemsControl x:Name="ItemsControl1" 
              ItemsSource="{Binding}"
        Margin="4" Background="DarkBlue">
        
        <ItemsControl.ItemsPanel>
            <ItemsPanelTemplate>
                <UniformGrid Columns="2" />
            </ItemsPanelTemplate>
        </ItemsControl.ItemsPanel>

In this case, the ItemsPanelTemplate .ItemsPanel indicates to the ItemPresenter present in the
ItemsControl ControlTemplate (see Default Template above) that it should use a UniformGrid to contain
the UI Items produced from the .ItemsSource items using the .ItemTemplate DataTemplate.

## ItemContainerStyle

The .ItemContainerStyle Style is applied to each UI Item produced from the .ItemsSource content using the .ItemTemplate DataTemplate
so as to assign them Triggers but also to define their complete aspect using a ControlTemplate.



    
