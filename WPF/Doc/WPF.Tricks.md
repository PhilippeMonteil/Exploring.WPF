
# WPF Tricks

## Windows : CommandBindings, InputBindings:

    <Window.DataContext>
        <local:ViewModel />
    </Window.DataContext>

    <Window.CommandBindings>
        <CommandBinding Command="New" CanExecute="NewCommand_CanExecute" Executed="NewCommand_Executed" />
    </Window.CommandBindings>
    
    <Window.InputBindings>
        <KeyBinding Key="O" Modifiers="Control" Command="{Binding Command}" CommandParameter="O"/>
        <KeyBinding Key="S" Modifiers="Control" Command="{Binding Command}" CommandParameter="S"/>
    </Window.InputBindings>

## Menus

- mettre en oeuvre le Nuget CommunityToolkit.MVVM : RelayCommand, ...
- Brancher tous les MenuItems d'un Menu sur une commande, en lui passant this comme paramètre
- Brancher un MenuItem sur une commande standard ("Copy", "Cut") envoyée à un contrôle particulier
- MenuItem.InputGestureText

    <Menu>

        <Menu.Resources>
            <Style TargetType="{x:Type MenuItem}">
                <Setter Property="Command" Value="{Binding Command}" />
                <Setter Property="CommandParameter" Value="{Binding RelativeSource={RelativeSource Self}}" />
            </Style>
        </Menu.Resources>

        <MenuItem Header="_File" >
            <MenuItem Header="_Open" InputGestureText="Ctrl+O"/>
            <MenuItem Header="_Save">
                <MenuItem Header="_Save0"/>
                <MenuItem Header="_Save1" />
            </MenuItem>
            <Separator/>
            <MenuItem Header="Exi_t"/>
        </MenuItem>

        <MenuItem Header="_File2">
            <MenuItem Command="New" CommandTarget="{Binding ElementName=bn0}"/>
            <MenuItem Command="Copy" CommandTarget="{Binding ElementName=tb0}"/>
            <MenuItem Command="Cut" CommandTarget="{Binding ElementName=tb0}"/>
            <MenuItem Command="Paste" CommandTarget="{Binding ElementName=tb0}"/>
        </MenuItem>

    </Menu>

## [Explicit, Implicit and Default styles in WPF](https://michaelscodingspot.com/explicit-implicit-and-default-styles-in-wpf/)

### Explicit (Style x:Key= ...)

    <Application.Resources>
        <Style x:Key=“BaseButtonStyle” TargetType=“Button”>
            <Setter Property=“Background” Value=“Black”/>
            <Setter Property=“Foreground” Value=“White”/>
        </Style>
    </Application.Resources>

    <Button Style=”{StaticResource BaseButtonStyle}“>Hello world</Button>

### Implicit

    <Application.Resources>
        <Style TargetType=“Button”>
            <Setter Property=“Background” Value=“Black”/>
            <Setter Property=“Foreground” Value=“White”/>
        </Style>
    </Application.Resources>

    <Button>Hello world</Button>

    // Implicit, particularisé localement
    <Button>
        <Button.Style>
            <Style TargetType=“Button” BasedOn=”{StaticResource {x:Type Button}}“>
                <Setter Property=“Padding” Value=“20”/>
            </Style>
        </Button.Style>
        Hello world
    </Button>

### Default / Custom Control

## [WPF Merged Dictionary problems and solutions](https://michaelscodingspot.com/wpf-merged-dictionary-problemsandsolutions/)

## [ThemeInfoAttribute ](https://docs.microsoft.com/en-us/dotnet/api/system.windows.themeinfoattribute?view=windowsdesktop-6.0)

    public ThemeInfoAttribute (System.Windows.ResourceDictionaryLocation themeDictionaryLocation, 
                            System.Windows.ResourceDictionaryLocation genericDictionaryLocation);

public enum ResourceDictionaryLocation

ExternalAssembly 	2 	

Theme dictionaries exist in assemblies external to the one defining the types being themed. They are named based on the original assembly with the theme name appended to it; for example, PresentationFramework.Luna.dll. These dictionaries share the same version and key as the original assembly.
None 	0 	

No theme dictionaries exist.
SourceAssembly 	1 	

Theme dictionaries exist in the assembly that defines the types being themed.
