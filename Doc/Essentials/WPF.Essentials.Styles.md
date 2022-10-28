
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

## Triggers

### Property Triggers

     <Style.Triggers>

       <Trigger Property="IsMouseOver" Value="True">
         <Setter Property="" Value="" />
       </Trigger>

       <Trigger Property="Validation.HasErrors" Value="True">
         <Setter Property="Background" Value="Red" />
         <Setter Property="Tooltip" 
            Value="{Binding 
                    RelativeSource={RelativeSource Self}},
                    Path={Vaidation.Errors[0].ErrorContent} " />
       </Trigger>

     </Style.Triggers>

### Data Triggers

     <Style.Triggers>

       <DataTrigger 
            Binding="{Binding RelativeSource={RelativeSource Self}, Path=Text}" 
            Value="True">
         <Setter Property="IsEnabled" Value="False" />
       </DataTrigger>

     </Style.Triggers>

### MultiTrigger

     <Style.Triggers>

       <MultiTrigger>

         <MultiTrigger.Conditions>
           <Condition Property="IsMouseOver" Value="True"/>
           <Condition Property="IsFocused" Value="True"/>
         </MultiTrigger.Conditions>

         <Setter Property="IsEnabled" Value="False" />

       </MultiTrigger>

     </Style.Triggers>

### Event Trigger

     <Button.Triggers>
       <EventTrigger RoutedEvent="Button.Click">
         <EventTrigger.Actions>
           <BeginStoryBoard>
             <StoryBoard>

             </StoryBoard>
           </BeginStoryBoard>
         </EventTrigger.Actions>
       </EventTrigger>
     </Button.Triggers>
