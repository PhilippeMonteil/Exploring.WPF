
# [ListBox](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.listbox?view=windowsdesktop-6.0)

	[System.Windows.Localizability(System.Windows.LocalizationCategory.ListBox)]
	[System.Windows.StyleTypedProperty(Property="ItemContainerStyle", StyleTargetType=typeof(System.Windows.Controls.ListBoxItem))]
	public class ListBox : System.Windows.Controls.Primitives.Selector

# [Selector](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.primitives.selector?view=windowsdesktop-6.0)

	Represents a control that allows a user to select items from among its child elements.


## .ItemContainerStyle

The __.ItemContainerStyle__ Style is applied to each UI Item produced from the .ItemsSource content using the .ItemTemplate DataTemplate
so as to assign them Triggers among others.

Le type des ItemContainers d'une ListBox est précisé par l'attribut System.Windows.StyleTypedProperty.

### Exemple : ListBox

    <ListBox.ItemContainerStyle>
        
        <Style TargetType="ListBoxItem">
            
            <Setter Property="Opacity" Value="0.5" />
            <Setter Property="MaxHeight" Value="75" />

            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="ListBoxItem">
                        <Border BorderThickness="4" BorderBrush="Yellow">
                            <ContentPresenter/>
                        </Border>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
            
            <Style.Triggers>
                <Trigger Property="IsSelected" Value="True">
                    <Trigger.Setters>
                        <Setter Property="Opacity" Value="1.0" />
                    </Trigger.Setters>
                </Trigger>
            </Style.Triggers>
            
        </Style>

    </ListBox.ItemContainerStyle>
