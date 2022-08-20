# Binding

## Documentation

### [Data binding overview (WPF .NET)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-6.0#basic-data-binding-concepts)

### [Binding declarations overview (WPF .NET)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/binding-declarations-overview?view=netdesktop-6.0)

### [Binding Markup Extension](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/binding-markup-extension?view=netframeworkdesktop-4.8)

### [PropertyPath XAML Syntax](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/propertypath-xaml-syntax?view=netframeworkdesktop-4.8)

## [Binding Class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding?view=windowsdesktop-6.0)

	public class Binding : System.Windows.Data.BindingBase

### Inheritance

- Object
- MarkupExtension
- BindingBase
- Binding

### Binding .Source, .RelativeSource, .ElementName

	public object Source { get; set; }

#### Default: DataContext

#### Source

	{Binding Source={StaticResource data} Path=Age}

#### RelativeSource : Self / TemplatedParent / FindAncestor / PreviousData

	{Binding RelativeSource={RelativeSource Self}}
	{Binding RelativeSource={RelativeSource TemplatedParent}}
	{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type StackPanel}}, Path=Orientation}
	{Binding RelativeSource={RelativeSource PreviousData}}

#### ElementName

	{Binding ElementName=ValueSlider Path=Value}

### .Path

#### Exemple

	<TextBox x:Name="myTextBox" Text="Hello world!" />
	<TextBox Text="{Binding ElementName=myTextBox, Path=Text.Length}" />
	<TextBox Text="{Binding ElementName=myTextBox, Path=Text[0]}" />

### .XPath

#### Exemple

	<Grid>

		<Grid.RowDefinitions>
			<RowDefinition Height="Auto" />
			<RowDefinition Height="Auto" />
		</Grid.RowDefinitions>

		<Grid.Resources>
			<XmlDataProvider x:Key="Movies">
				<x:XData>
					<Movies>
						<Movie title="Top Gun">
							<Actor>Tom Cruise</Actor>
						</Movie>
					</Movies>
				</x:XData>
			</XmlDataProvider>
		</Grid.Resources>

		<TextBox Grid.Row="0"
			 Text="{Binding Source={StaticResource Movies}, XPath=Movies/Movie[1]/@title}" />
		<TextBox Grid.Row="1"
			 Text="{Binding Source={StaticResource Movies}, XPath=Movies/Movie[1]/Actor[1]}" />

	</Grid>

- BindsDirectlyToSource: Path est interprété directement comme une propriété de l'objet source

#### Exemple

	<Grid>

		<Grid.RowDefinitions>
			<RowDefinition Height="Auto" />
			<RowDefinition Height="Auto" />
		</Grid.RowDefinitions>

		<Grid.Resources>
			<ObjectDataProvider x:Key="Movies">
				<ObjectDataProvider.ObjectInstance>
					<WpfApplication2:Movies>
						<WpfApplication2:Movie Title="Top Gun" />
						<WpfApplication2:Movie Title="Star Wars: A new hope" />
					</WpfApplication2:Movies>
				</ObjectDataProvider.ObjectInstance>
			</ObjectDataProvider>
		</Grid.Resources>

		<TextBox Grid.Row="0" Text="{Binding Source={StaticResource Movies}, Path=[0].Title}" />
		<TextBox Grid.Row="1" Text="{Binding Source={StaticResource Movies}, BindsDirectlyToSource=True, Path=ObjectType}" />

	</Grid>

### Conversions

  - using System.Windows.Data;
  - IValueConverter
  - [ValueConversion(typeof(string), typeof(string))]

#### Exemple

	<Grid x:Name="LayoutRoot">
		<Grid.Resources>
			<WpfApplication2:MyConverter x:Key="MyConverter" />
		</Grid.Resources>
		<TextBox Text="{Binding ElementName=LayoutRoot, Path=ActualWidth, 
						Converter={StaticResource MyConverter}, 
						ConverterParameter=2, 
						ConverterCulture=en-US, 
						Mode=OneWay}" />
	</Grid>

### Validation

#### ValidatesOnDataErrors: si l'objet source du binding expose IDataErrorInfo

	namespace System.ComponentModel
	{
		public interface IDataErrorInfo
		{
			string this[string columnName] { get; }
			string Error { get; }
		}
	}

##### Exemple

	Text="{Binding Path=FirstName, 
			Mode=TwoWay, 
			UpdateSourceTrigger=PropertyChanged,
			ValidatesOnDataErrors=True, 
			ValidatesOnExceptions=True, 
			NotifyOnValidationError=True}" 

#### Trigger / Validation.HasError == true

	<Style x:Key="textBoxInError" TargetType="TextBox">
		<Style.Triggers>
			<Trigger Property="Validation.HasError" Value="true">
				<Setter Property="ToolTip"
                    Value="{Binding RelativeSource={x:Static RelativeSource.Self},
					Path=(Validation.Errors)[0].ErrorContent}"/>
			</Trigger>
		</Style.Triggers>
	</Style>

#### Validation.ErrorTemplate

    <ControlTemplate x:Key="validationTemplate">
        <DockPanel>
        <TextBlock Foreground="Red" FontSize="20">!</TextBlock>
        <AdornedElementPlaceholder/>
        </DockPanel>
    </ControlTemplate>

    <TextBox Name="textBox1" Width="50" FontSize="10" Foreground="Red" Margin="8" HorizontalAlignment="Left" VerticalAlignment="Top"
         Validation.ErrorTemplate="{StaticResource validationTemplate}"
         Style="{StaticResource textBoxInError}">
        <TextBox.Text>
        <Binding Source="{StaticResource Data0}" Path="Age" UpdateSourceTrigger="PropertyChanged" >
            <Binding.ValidationRules>
            <localvr:ValidationRule0 Min="7" Max="77"/>
            </Binding.ValidationRules>
        </Binding>
        </TextBox.Text>
    </TextBox>

#### ValidatesOnExceptions

    <!-- pas de 'boîte rouge' -->
    <TextBox Text="{Binding Name, ValidatesOnExceptions=False,Mode=TwoWay,UpdateSourceTrigger=PropertyChanged}" Margin="8"/>
    <!-- pas de 'boîte rouge' -->
    <TextBox Text="{Binding Name, ValidatesOnExceptions=True,Mode=TwoWay,UpdateSourceTrigger=PropertyChanged}" Margin="8"/>

    public string Name
    {
        get
        {
			Debug.WriteLine($"get {nameof(Name)} -> m_Name={m_Name}");
			return m_Name;
        }
        set
        {
			Debug.WriteLine($"set {nameof(Name)} value={value}");
			throw new Exception($"set {nameof(ViewModel)}.{nameof(Name)}");
			m_Name = value;
        }
    }

#### ValidatesOnNotifyDataErrors 

Mise en oeuvre de INotifyDataErrorInfo

	namespace System.ComponentModel
	{
		public interface INotifyDataErrorInfo
		{
			bool HasErrors { get; }
			event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
			IEnumerable GetErrors(string propertyName);
		}
	}

#### ValidationRules 

- clause AND entre les Validation rules
- appelées dans le sens Target -> Source, avant la conversion, ssi la validation réussit

	<TextBox Grid.Row="0" Margin="2">
    	<TextBox.Text>
    		<Binding Path="Value"
				 UpdateSourceTrigger="PropertyChanged">
    			<Binding.ValidationRules>
    				<WpfApplication2:RangeRule Min="0" Max="100" />
    				<WpfApplication2:EvenRule />
    			</Binding.ValidationRules>
    		</Binding>
    	</TextBox.Text>
	</TextBox>

	using System.Windows.Controls;

	public class RangeRule : ValidationRule
    {
    	public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    	{
    		int valueToCheck;

    		if (!(value is String) || !int.TryParse(value as String, out valueToCheck))
    			return new ValidationResult(false, "Value is not in a valid integer");

    		if (valueToCheck < Min || valueToCheck > Max)
    			return new ValidationResult(false, String.Format("Value must be between {0} and {1}", Min, Max));

    		return new ValidationResult(true, null);
    	}
	}

#### BindingGroupName 

 	<Grid.BindingGroup>
		<BindingGroup Name="DateGroup" NotifyOnValidationError="True">
			<BindingGroup.ValidationRules>
				<WpfApplication2:DateRule ValidatesOnTargetUpdated="True" />
			</BindingGroup.ValidationRules>
		</BindingGroup>
	</Grid.BindingGroup>
	<TextBox x:Name="day"
			 Grid.Row="0"
			 Margin="2"
			 Text="{Binding Path=Day, BindingGroupName=DateGroup, UpdateSourceTrigger=Explicit}" />
	<TextBox x:Name="month"
			 Grid.Row="1"
			 Margin="2"
			 Text="{Binding Path=Month, BindingGroupName=DateGroup, UpdateSourceTrigger=Explicit}" />

- Dans le code:

	LayoutRoot.BindingGroup.BeginEdit();

	private void SubmitClick(object sender, RoutedEventArgs e)
	{
		if (LayoutRoot.BindingGroup.CommitEdit())
		{
			LayoutRoot.BindingGroup.BeginEdit();
		}
	}

	private void CancelClick(object sender, RoutedEventArgs e)
	{
		LayoutRoot.BindingGroup.CancelEdit();
		LayoutRoot.BindingGroup.BeginEdit();
	}

- ValidationRule:

	public class DateRule : ValidationRule
    {
    	public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    	{
    		if (!(value is BindingGroup))
    			return new ValidationResult(false, "Invalid group");

    		var group = value as BindingGroup;
		
    		var day = Convert.ToInt32(group.GetValue(group.Items[0], "Day"));
    		var month = Convert.ToInt32(group.GetValue(group.Items[0], "Month"));
    		var year = Convert.ToInt32(group.GetValue(group.Items[0], "Year"));

    		try
    		{
    			new DateTime(year, month, day);
    		}
    		catch (Exception)
    		{
    			return new ValidationResult(false, "This is not a valid date");
    		}

    		return new ValidationResult(true, null);
    	}
	}

## Notifications

- NotifyOnSourceUpdated  / NotifyOnTargetUpdated 

  Si NotifyOnSourceUpdated / NotifyOnTargetUpdated == true: un évènement SourceUpdated / TargetUpdated est généré

	<TextBox x:Name="day"
			 Grid.Row="0"
			 Margin="2"
			 Text="{Binding Path=Value, NotifyOnSourceUpdated=True, NotifyOnTargetUpdated=True, UpdateSourceTrigger=PropertyChanged}"
			 SourceUpdated="OnSourceUpdated"
			 TargetUpdated="OnTargetUpdated" />

- NotifyOnValidationError 

   détermine si l'event Validation.Error doit être déclenché en cas d'ereur de validation

	<TextBox Grid.Row="0"
				Margin="2"
				Text="{Binding Path=Value, NotifyOnValidationError=True, UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True}"
				Validation.Error="OnError" />

## Mise à jour du binding

 - UpdateSourceTrigger: 

	- Default / contrôle: ex: perte de focus
	- PropertyChanged
	- LostFocus
	- Explicit
	  ex: monTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource() .

  - UpdateSourceExceptionFilter 

    définition d'un callback qui sera appelé en cas d'erreur de validation. 

    ex:

  <TextBox Grid.Row="0"
		 Margin="2">
	<TextBox.Text>
		<Binding Path="Value"
				 UpdateSourceTrigger="PropertyChanged"
				 UpdateSourceExceptionFilter="OnUpdateSourceExceptionFilter">
			<Binding.ValidationRules>
				<ExceptionValidationRule />
			</Binding.ValidationRules>
		</Binding>
	</TextBox.Text>
</TextBox>

	public partial class MainWindow

		object OnUpdateSourceExceptionFilter(object bindingExpression, Exception exception)
		{
			Debug.WriteLine(exception);
			return exception;
		}

## Options

- FallbackValue 

- IsAsync

  ex:

  <TextBox x:Name="textbox" Text="{Binding Path=Value, IsAsync=True, FallbackValue=Loading...}" />

- Mode

  - TwoWay
  - OneWay (source -> cible)
  - OneTime
  - OneWayToSource
  - Default	: une des valeurs précédentes, précisée par le contrôle

- StringFormat 

<TextBox x:Name="textbox"
		Grid.Row="0"
		Margin="2">
	<TextBox.Text>
		<Binding Path="Now" StringFormat="HH:mm:ss.ff">
			<Binding.Source>
				<System:DateTime />
			</Binding.Source>
		</Binding>
	</TextBox.Text>
</TextBox>

- TargetNullValue 

## Les Converters

  - IValueConverter

namespace System.Windows.Data
{
    public interface IValueConverter
    {
        object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}

  - Converter et Markup extension
    - using System.Windows.Markup;
    - MarkupExtension
    - [MarkupExtensionReturnType(typeof(IValueConverter))]

## Multibinding et multivalue converters

<Window xmlns:local="clr-namespace:BlogIMultiValueConverter">
    <Window.Resources>
        <local:NameMultiValueConverter x:Key="NameMultiValueConverter" />
    </Window.Resources>
    <Grid>
        <TextBox Text="{Binding Path=FirstName, UpdateSourceTrigger=PropertyChanged}" />
        <TextBox Text="{Binding Path=Surname, UpdateSourceTrigger=PropertyChanged}" />
        <TextBlock>
            <TextBlock.Text>
                <MultiBinding Converter="{StaticResource MultiValueConverter}">
                    <Binding Path="FirstName" />
                    <Binding Path="Surname" />
                </MultiBinding>
            </TextBlock.Text>
        </TextBlock>
    </Grid>
</Window>

public class NameMultiValueConverter : IMultiValueConverter
{
   public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
       return String.Format("{0} {1}", values[0], values[1]);
   }
}

## Exemple

	<Style x:Key="textBoxInError" TargetType="{x:Type TextBox}">
		<Style.Triggers>
			<Trigger Property="Validation.HasError" Value="true">
				<Setter Property="ToolTip"
					Value="{Binding RelativeSource={x:Static RelativeSource.Self},
                        Path=(Validation.Errors)/ErrorContent}"/>
			</Trigger>
		</Style.Triggers>
	</Style>

## URLs

- [Comprendre le Binding](https://nathanaelmarchand.developpez.com/tutoriels/dotnet/comprendre-binding-wpf-et-silverlight/)


