# Binding

## 1) Documentation

### [Data binding overview (WPF .NET)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-6.0#basic-data-binding-concepts)

### [Binding declarations overview (WPF .NET)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/binding-declarations-overview?view=netdesktop-6.0)

### [Binding Markup Extension](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/binding-markup-extension?view=netframeworkdesktop-4.8)

### [PropertyPath XAML Syntax](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/propertypath-xaml-syntax?view=netframeworkdesktop-4.8)

## En résumé

- Source / RelativeSource / ElementName
- Path / XPath
- UpdateSourceTrigger : Defaut (cd metadata) / PropertyChanged / LostFocus / Explicit
- Mode : Default (cf metadata) / OneTime / OneWay / OneWayToSource / TwoWay
- Converter, ConverterCulture , ConverterParameter
- Validation.Errors Attached Property
- ValidatesOnDataErrors (si la Source du Binding expose IDataErrorInfo)

## 2) [Binding Class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding?view=windowsdesktop-6.0)

	public class Binding : System.Windows.Data.BindingBase

### 2.1) Inheritance

- Object
- MarkupExtension
- BindingBase
- Binding

### 2.2) [Source](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.source?view=windowsdesktop-6.0), [RelativeSource](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.relativesource?view=windowsdesktop-6.0), [ElementName](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.elementname?view=windowsdesktop-6.0)

	public object Source { get; set; }
	public System.Windows.Data.RelativeSource RelativeSource { get; set; }
	public string ElementName { get; set; }

> By default, bindings inherit the data context specified by the DataContext property, if one has been set. However, the Source property is one of the ways you can explicitly set the source of a Binding and override the inherited data context.

> The Binding.ElementName and Binding.RelativeSource properties also enable you to set the source of the binding explicitly. However, only one of the three properties, ElementName, Source, and RelativeSource, should be set for each binding, or a conflict can occur. This property throws an exception if there is a binding source conflict.

#### Source

	{Binding Source={StaticResource data} Path=Age}

#### [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.datacontext?view=windowsdesktop-6.0) 

	[System.Windows.Localizability(System.Windows.LocalizationCategory.NeverLocalize)]
	public object DataContext { get; set; }

> Data context is a concept that allows elements to inherit information from their parent elements about the data source that is used for binding, as well as other characteristics of the binding, such as the path.

> Data context can be set directly to a .NET object, with the bindings evaluating to properties of that object. Alternatively, you can set the data context to a DataSourceProvider object.

> This dependency property inherits property values. If there are child elements without other values for DataContext established through local values or styles, then the property system will set the value to be the DataContext value of the nearest parent element with this value assigned.


#### RelativeSource

##### [RelativeSource Class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.relativesource?view=windowsdesktop-6.0)

	[System.Windows.Markup.MarkupExtensionReturnType(typeof(System.Windows.Data.RelativeSource))]
	public class RelativeSource : System.Windows.Markup.MarkupExtension, 
									System.ComponentModel.ISupportInitialize

##### Inheritance

- Object
- MarkupExtension
- RelativeSource

#### Properties

	public System.Windows.Data.RelativeSourceMode Mode { get; set; }
	public Type AncestorType { get; set; }
	...

#### [RelativeSourceMode](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.relativesourcemode?view=windowsdesktop-6.0)

Self / TemplatedParent / FindAncestor / PreviousData

##### Self / TemplatedParent / FindAncestor / PreviousData

	{Binding RelativeSource={RelativeSource Self}}
	{Binding RelativeSource={RelativeSource TemplatedParent}}
	{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type StackPanel}}, Path=Orientation}
	{Binding RelativeSource={RelativeSource PreviousData}}

#### ElementName

	{Binding ElementName=ValueSlider Path=Value}

### 2.3) [Path](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.path?view=windowsdesktop-6.0)

	public System.Windows.PropertyPath Path { get; set; }

#### [PropertyPath](https://docs.microsoft.com/en-us/dotnet/api/system.windows.propertypath?view=windowsdesktop-6.0)

cf [PropertyPath XAML Syntax](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/propertypath-xaml-syntax?view=netframeworkdesktop-4.8)
>PropertyPath supports two modes of behavior:

>__Source mode__ describes a path to a property that is used as a source for some other operation. This mode is used by the Binding class to support data binding.

>__Target mode__ describes a path to a property that will be set as a target property. This mode is used by animation in support of storyboard and timeline setters.

	[System.ComponentModel.TypeConverter(typeof(System.Windows.PropertyPathConverter))]
	public sealed class PropertyPath

	public PropertyPath (object parameter);
	public PropertyPath (string path, params object[] pathParameters);

	public string Path { get; set; }
	public System.Collections.ObjectModel.Collection<object> PathParameters { get; }

#### [PropertyPath for Objects in Data Binding](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/propertypath-xaml-syntax?view=netframeworkdesktop-4.8#propertypath-for-objects-in-data-binding)

- Single Property on the Immediate Object as Data Context

	<Binding Path="propertyName" ... />

- Single Indexer on the Immediate Object as Data Context

    <Binding Path="[key]" ... />

- Multiple Property (Indirect Property Targeting)

    <Binding Path="propertyName.propertyName2" ... />

- Single Property, Attached or Otherwise Type-Qualified

    <object property="(ownerType.propertyName)" ... />

- Source Traversal (Binding to Hierarchies of Collections)

	<object Path="propertyName/propertyNameX" ... />

- Multiple Indexers

    <object Path="[index1,index2...]" ... />
	<object Path="propertyName[index,index2...]" ... />

- Mixing Syntaxes

	<Rectangle Fill="{Binding ColorGrid[20,30].SolidColorBrushResult}" ... />

### 2.4) [BindsDirectlyToSource](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.bindsdirectlytosource?view=windowsdesktop-6.0)

	// Gets or sets a value that indicates whether to evaluate the Path relative to 
	// the data item or the DataSourceProvider object.
	public bool BindsDirectlyToSource { get; set; }

#### [DataSourceProvider](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.datasourceprovider?view=windowsdesktop-6.0)

	public abstract class DataSourceProvider : System.ComponentModel.INotifyPropertyChanged, 
											System.ComponentModel.ISupportInitialize

##### Inheritance

- Object
- DataSourceProvider

##### Derived

- System.Windows.Data.ObjectDataProvider
- System.Windows.Data.XmlDataProvider

##### Properties

	// When the DataSourceProvider is used as the source of a binding, 
	// this is the resulting binding source object.
	public object Data { get; }

##### Exemple

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

### 2.6) [XPath](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.xpath?view=windowsdesktop-6.0)

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

## 3) [UpdateSourceTrigger](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.updatesourcetrigger?view=windowsdesktop-6.0) : Mise à jour du binding

 - UpdateSourceTrigger: 

	- Default / contrôle: ex: perte de focus
	- PropertyChanged
	- LostFocus
	- Explicit

 Exemple: 

	monTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource() .

- Modification de la valeur par défaut de UpdateSourceTrigger de la DependencyProperty d'un Contrôle: 
 
 Cf FrameworkPropertyMetadata.DefaultUpdateSourceTrigger 

 Exemple :

	static MainWindow() 
	{
        FrameworkPropertyMetadata fpm = new FrameworkPropertyMetadata(string.Empty, 
    		FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
    		null, 
    		null, 
    		true, 
    		UpdateSourceTrigger.PropertyChanged);
        TextEdit.EditValueProperty.OverrideMetadata(typeof(TextEdit), fpm);
    }

## 4) [Mode](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.mode?view=windowsdesktop-6.0)

	public System.Windows.Data.BindingMode Mode { get; set; }

### [BindingMode](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.mode?view=windowsdesktop-6.0)

- Default
- OneTime
- OneWay
- OneWayToSource
- TwoWay

> Default : a programmatic way to determine whether a dependency property binds one-way or two-way by default is to get the property metadata of the property using GetMetadata(Type) and then check the Boolean value of the BindsTwoWayByDefault property.

## 5) Conversion : [Converter](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.converter?view=windowsdesktop-6.0), [ConverterCulture](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.converterculture?view=windowsdesktop-6.0), [ConverterParameter](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.converterparameter?view=windowsdesktop-6.0)

> A binding implicitly uses a default converter that tries to do a type conversion between the source value and the target value. If a conversion cannot be made, the default converter returns null.

- [IValueConverter](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.ivalueconverter?view=windowsdesktop-6.0)
- [ValueConversionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.valueconversionattribute?view=windowsdesktop-6.0)

### Exemple

	[ValueConversion( typeof( string ), typeof( ProcessingState ) )]
	public class IntegerStringToProcessingStateConverter : IValueConverter
	{
		...
	}

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

## 6) Validation

### 6.1) [ValidationRules](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.validationrules?view=windowsdesktop-6.0)

	public System.Collections.ObjectModel.Collection<System.Windows.Controls.ValidationRule> ValidationRules { get; }

> The WPF data binding model enables you to associate ValidationRules with your Binding or MultiBinding object. You can create custom rules by deriving from the ValidationRule class and implementing the Validate method, or you can use the built-in ExceptionValidationRule, which invalidates a value if there are exceptions during source updates.

### 6.2) [UpdateSourceExceptionFilter](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.updatesourceexceptionfilter?view=windowsdesktop-6.0) 

> Gets or sets a handler you can use to provide custom logic for handling exceptions that the binding engine encounters during the update of the binding source value. This is only applicable if you have associated an __ExceptionValidationRule__ with your binding in its __ValidationRules__ property.

#### Exemple

	<TextBox Grid.Row="0" Margin="2">
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

### 6.3) [Validation.HasError, .Errors, .Error, .ErrorTemplate Attached Properties and Event](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.validation.errors?view=windowsdesktop-6.0)

> The WPF data binding model enables you to associate ValidationRules with your Binding object. Validation occurs during binding target-to-binding source value transfer before the converter is called.

### 6.4) [ValidatesOnDataErrors](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.validatesondataerrors?view=windowsdesktop-6.0)

	public bool ValidatesOnDataErrors { get; set; }

Si l'objet source du binding expose [IDataErrorInfo](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.idataerrorinfo?view=net-6.0)

	namespace System.ComponentModel
	{
		public interface IDataErrorInfo
		{
			string this[string columnName] { get; }
			string Error { get; }
		}
	}

#### Exemple

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

### 6.5) [ValidatesOnExceptions](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.validatesonexceptions?view=windowsdesktop-6.0)

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

### 6.6) [ValidatesOnNotifyDataErrors](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.validatesonnotifydataerrors?view=windowsdesktop-6.0)

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

## 5) Notifications

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

## 7) Options

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

	<TextBox x:Name="textbox" Grid.Row="0" Margin="2">
	<TextBox.Text>
		<Binding Path="Now" StringFormat="HH:mm:ss.ff">
			<Binding.Source>
				<System:DateTime />
			</Binding.Source>
		</Binding>
	</TextBox.Text>
</TextBox>

- TargetNullValue 

## 8) Les Converters

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

## 9) Multibinding et multivalue converters

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

### Exemple

	<Style x:Key="textBoxInError" TargetType="{x:Type TextBox}">
		<Style.Triggers>
			<Trigger Property="Validation.HasError" Value="true">
				<Setter Property="ToolTip"
					Value="{Binding RelativeSource={x:Static RelativeSource.Self},
                        Path=(Validation.Errors)/ErrorContent}"/>
			</Trigger>
		</Style.Triggers>
	</Style>

## 10) URLs

- [Comprendre le Binding](https://nathanaelmarchand.developpez.com/tutoriels/dotnet/comprendre-binding-wpf-et-silverlight/)


