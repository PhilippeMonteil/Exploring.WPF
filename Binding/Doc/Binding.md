# Binding

## 1) Documentation

### [Data binding overview (WPF .NET)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-6.0#basic-data-binding-concepts)

### [Binding declarations overview (WPF .NET)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/binding-declarations-overview?view=netdesktop-6.0)

### [Binding Markup Extension](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/binding-markup-extension?view=netframeworkdesktop-4.8)

### [PropertyPath XAML Syntax](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/propertypath-xaml-syntax?view=netframeworkdesktop-4.8)

## En r�sum�

- Source (Defaut : DataContext) / RelativeSource / ElementName (exclusifs)
- Path / XPath
- UpdateSourceTrigger : Defaut (cf metadata) / PropertyChanged / LostFocus / Explicit
- Mode : Default (cf metadata) / OneTime / OneWay / OneWayToSource / TwoWay
- Validation:
    - d�pend de 
        - Binding.ValidationRules
        - Binding.ValidatesOnDataErrors
        - Binding.ValidatesOnExceptions
        - Binding.ValidatesOnNotifyDataErrors
    - appelle Binding.UpdateSourceExceptionFilter en cas d'exception d�clench�e par la Source lors d'un update 
    - met � jour BoundElement.Validation.HasErrors, BoundElement.Validation.Errors
    - produit un UI d'affichage des erreurs de validation avec BoundElement.Validation.ErrorTemplate
- Converter, ConverterCulture , ConverterParameter

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

> By default, bindings inherit the data context specified by the DataContext property, if one has been set. 
However, the Source property is one of the ways you can explicitly set the source of a Binding and override the inherited data context.

> The Binding.ElementName and Binding.RelativeSource properties also enable you to set the source of the binding explicitly. 
However, only one of the three properties, ElementName, Source, and RelativeSource, should be set for each binding, or a conflict can occur. This property throws an exception if there is a binding source conflict.

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

## 3) [UpdateSourceTrigger](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.updatesourcetrigger?view=windowsdesktop-6.0) : Mise � jour du binding

 - UpdateSourceTrigger: 

	- Default / contr�le: ex: perte de focus
	- PropertyChanged
	- LostFocus
	- Explicit

 Exemple: 

	monTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource() .

- Modification de la valeur par d�faut de UpdateSourceTrigger de la DependencyProperty d'un Contr�le: 
 
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

### 6.1) [Validation.HasError, .Errors, .Error, .ErrorTemplate Attached Properties and Event](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.validation.errors?view=windowsdesktop-6.0)

When a value is being transferred from the target property to the source property:
- clear BoundElement[Validation.Errors]
- if Binding.ValidationRules != null
    - call the Validate method on each of the ValidationRules until one of them runs into an error or until all of them pass.
	- once there is a custom rule that does not pass, create a ValidationError object and add it to BoundElement[Validation.Errors]
    - if BoundElement[Validation.Errors] not empty then raise the BoundElement[Validation.Error] attached event
- if no error then call the Binding.Converter, if one exists.
- if the Binding.Converter passes, call the setter of the Source property.
- if the Binding.ValidationRules has an ExceptionValidationRule and an exception is thrown during the setter call: 
  - if there is a Binding.UpdateSourceExceptionFilter call it 
  - else create a ValidationError with the exception and add it to Target[Validation.Errors].

### 6.2) [ValidationRules](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.validationrules?view=windowsdesktop-6.0)

	public System.Collections.ObjectModel.Collection<System.Windows.Controls.ValidationRule> ValidationRules { get; }

> The WPF data binding model enables you to associate ValidationRules with your Binding or MultiBinding object. You can create custom rules by deriving from the ValidationRule class and implementing the Validate method, or you can use the built-in ExceptionValidationRule, which invalidates a value if there are exceptions during source updates.

- clause AND entre les Validation rules
- appel�es dans le sens Target -> Source, avant la conversion, ssi la validation r�ussit

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

### 6.3) [UpdateSourceExceptionFilter](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.updatesourceexceptionfilter?view=windowsdesktop-6.0) 

> Gets or sets a handler you can use to provide custom logic for handling exceptions that the binding engine encounters during the update of the binding source value. 
This is only applicable if you have associated an __ExceptionValidationRule__ with your binding in its __ValidationRules__ property.

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

### 6.4) [ValidatesOnDataErrors](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.validatesondataerrors?view=windowsdesktop-6.0)

	public bool ValidatesOnDataErrors { get; set; }

> Setting this property provides an alternative to using the DataErrorValidationRule element explicitly. The DataErrorValidationRule is a built-in validation rule that checks for errors that are raised by the IDataErrorInfo implementation of the source object. If an error is raised, the binding engine creates a ValidationError with the error and adds it to the Validation.Errors collection of the bound element. The lack of an error clears this validation feedback, unless another rule raises a validation issue.

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

[Validation.ErrorTemplate Attached Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.validation.errortemplate?view=windowsdesktop-6.0)

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

	public bool ValidatesOnExceptions { get; set; }

> Setting this property provides an alternative to using the ExceptionValidationRule element explicitly. The ExceptionValidationRule is a built-in validation rule that checks for exceptions that are thrown during the update of the source property. If an exception is thrown, the binding engine creates a ValidationError with the exception and adds it to the Validation.Errors collection of the bound element. The lack of an error clears this validation feedback, unless another rule raises a validation issue.

#### Exemple

    <!-- pas de 'bo�te rouge' -->
    <TextBox Text="{Binding Name, ValidatesOnExceptions=False,Mode=TwoWay,UpdateSourceTrigger=PropertyChanged}" Margin="8"/>
    <!-- pas de 'bo�te rouge' -->
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

	public bool ValidatesOnNotifyDataErrors { get; set; }

> When ValidatesOnNotifyDataErrors is true, the binding checks for and reports errors that are raised by a data source that implements INotifyDataErrorInfo.

	namespace System.ComponentModel
	{
		public interface INotifyDataErrorInfo
		{
			bool HasErrors { get; }
			event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
			IEnumerable GetErrors(string propertyName);
		}
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

## 7) [FrameworkElement.BindingGroup](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.bindinggroup?view=windowsdesktop-6.0)

	[System.Windows.Localizability(System.Windows.LocalizationCategory.NeverLocalize)]
	public System.Windows.Data.BindingGroup BindingGroup { get; set; }

### [BindingGroup](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.bindinggroup?view=windowsdesktop-6.0)

> Contains a collection of bindings and ValidationRule objects that are used to validate an object.

Un ensemble de Bindings peuvent faire r�f�rence � un __BindingGroup__ par son nom en assignant leur 
propri�t� __BindingGroupName__.

Ce BindingGroup doit �tre explicitement initi� (__BeginEdit__) puis commit� (__CommitEdit__) ou abandonn� (__CancelEdit__)
pour que les Bindings qui s'y sont rattach�s soient ex�cut�s. 

#### Exemple

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

Code:

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

ValidationRule:

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

## 8) Notifications

### Binding.NotifyOnSourceUpdated  / NotifyOnTargetUpdated 

  Si __NotifyOnSourceUpdated__ / __NotifyOnTargetUpdated__ == true un �v�nement __SourceUpdated__ / __TargetUpdated__ est g�n�r�

	<TextBox x:Name="day"
			 Grid.Row="0"
			 Margin="2"
			 Text="{Binding Path=Value, 
					NotifyOnSourceUpdated=True, 
					NotifyOnTargetUpdated=True, 
					UpdateSourceTrigger=PropertyChanged}"
			 SourceUpdated="OnSourceUpdated"
			 TargetUpdated="OnTargetUpdated" />

### Binding.NotifyOnValidationError 

   d�termine si l'Attached Event Validation.Error doit �tre d�clench� en cas d'erreur de validation

	<TextBox Grid.Row="0"
				Margin="2"
				Text="{Binding Path=Value, 
						NotifyOnValidationError=True, 
						UpdateSourceTrigger=PropertyChanged, 
						ValidatesOnExceptions=True}"
				Validation.Error="OnError" />

## 10) [BindingBase.StringFormat](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.bindingbase.stringformat?view=windowsdesktop-6.0)

	public string StringFormat { get; set; }

> Gets or sets a string that specifies how to format the binding if it displays the bound value as a string.

### Exemple 

	<TextBox x:Name="textbox" Grid.Row="0" Margin="2">
		<TextBox.Text>
			<Binding Path="Now" StringFormat="HH:mm:ss.ff">
				<Binding.Source>
					<System:DateTime />
				</Binding.Source>
			</Binding>
		</TextBox.Text>
	</TextBox>

## 9) [BindingBase.FallbackValue](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.bindingbase.fallbackvalue?view=windowsdesktop-6.0)

	public object FallbackValue { get; set; }

> Gets or sets the value to use when the binding is unable to return a value.

## 11) [Binding.IsAsync](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.isasync?view=windowsdesktop-6.0)

	public bool IsAsync { get; set; }

> Gets or sets a value that indicates whether the Binding should get and set values asynchronously.

cf [Binding.AsyncState Property](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.asyncstate?view=windowsdesktop-6.0)

### Exemple

	<TextBox x:Name="textbox" Text="{Binding Path=Value, IsAsync=True, FallbackValue=Loading...}" />

## 12) [TargetNullValue](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.bindingbase.targetnullvalue?view=windowsdesktop-6.0)

	public object TargetNullValue { get; set; }

> Gets or sets the value that is used in the target when the value of the source is null.

## 13) Multibinding et multivalue converters

### Exemple

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

		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return String.Format("{0} {1}", values[0], values[1]);
		}

	}

## URLs

- [Comprendre le Binding](https://nathanaelmarchand.developpez.com/tutoriels/dotnet/comprendre-binding-wpf-et-silverlight/)

## Data binding how-to topics

- https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/data-binding-how-to-topics?view=netframeworkdesktop-4.8

