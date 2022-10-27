
# Styles

## Exemples

    <Style x:Key="MyStyle">
        <Setter Property="Control.FontSize" Value="32" />
    </Style>

    <Style x:Key="MyStyle" TargetType={x:Type Button} >
        <Setter Property="Control.FontSize" Value="32" />
    </Style>

    <Style x:Key="{x:Type ItemsControl}" TargetType="{x:Type ItemsControl}">
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

## implicit style

- Un TargetType mais pas de x:Key

      <Style TargetType={x:Type Button} >
        <Setter Property="Control.FontSize" Value="32" />
      </Style>

  Un tel Style peut être mentionné dans un ResourceDictionary, le TargetType servant implicitement de Key.

- Keyless Resource 

      <Button Style="{StaticResource {x:Type Button}" />

## FrameworkElement : Style, FocusVisualStyle

## propriétés de type ResourceKey

### Exemple : ToolBar.ButtonStyleKey Property

    public static System.Windows.ResourceKey ButtonStyleKey { get; }

    <Application>
      <Application.Resources>
        <Style x:Key="{x:Static ToolBar.ButtonStyleKey}" TargetType="{x:Type Button}"
      </Application.Resources>
    </Application>

