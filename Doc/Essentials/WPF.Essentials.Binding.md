
# [Binding](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-7.0&viewFallbackFrom=netdesktop-5.0)

## En résumé

- classe Data.BindingBase

    - public abstract class BindingBase : Markup.MarkupExtension
 
    - Inheritance : Object, MarkupExtension, BindingBase
 
    - Derived : Binding / MultiBinding / PriorityBinding

    - properties
        - public string BindingGroupName { get; set; }
        - public int Delay { get; set; }
        - public object FallbackValue { get; set; }
        - public string StringFormat { get; set; }
        - public object TargetNullValue { get; set; }

- classe Data.Binding

    - Inheritance : Object - MarkupExtension - BindingBase - Binding

    - properties

        - public object Source { get; set; } // Default : DataContext du DependencyObject Target du Binding
        - public string ElementName { get; set; }
        - public RelativeSource RelativeSource { get; set; } // Self / TemplatedParent / FindAncestor

        - public PropertyPath Path { get; set; }

        - public BindingMode Mode { get; set; }
        - public UpdateSourceTrigger UpdateSourceTrigger { get; set; } // si Mode == TwoWay ou OneWayToSource, defaut : MetaData Target DependencyProperty

        - public IValueConverter Converter { get; set; }
        - public System.Globalization.CultureInfo ConverterCulture { get; set; }
        - public object ConverterParameter { get; set; }

        // déclenchement d'events
        - public bool NotifyOnSourceUpdated { get; set; }
        - public bool NotifyOnTargetUpdated { get; set; }
        - public bool NotifyOnValidationError { get; set; }

        - public bool IsAsync { get; set; }
        - public object AsyncState { get; set; }

        - public bool BindsDirectlyToSource { get; set; }

        - public string XPath { get; set; } 

        // validation
        - public Collection<ValidationRule> ValidationRules { get; }
        - public bool ValidatesOnDataErrors { get; set; }
        - public bool ValidatesOnExceptions { get; set; }
        - public bool ValidatesOnNotifyDataErrors { get; set; }
        - public UpdateSourceExceptionFilterCallback UpdateSourceExceptionFilter { get; set; }

- classe BindingOperations

    public static BindingExpressionBase SetBinding (DependencyObject target, DependencyProperty dp, BindingBase binding);
    public static Binding GetBinding (DependencyObject target, DependencyProperty dp);

    public static BindingExpression GetBindingExpression (DependencyObject target, DependencyProperty dp);
    public static bool IsDataBound (System.Windows.DependencyObject target, System.Windows.DependencyProperty dp);

    public static void ClearBinding (System.Windows.DependencyObject target, System.Windows.DependencyProperty dp);
    public static void ClearAllBindings (System.Windows.DependencyObject target);

- classe BindingExpression

    public sealed class BindingExpression : BindingExpressionBase, 
                                                System.Windows.IWeakEventListener

- properties
    - public object DataItem { get; } // Gets the binding source object that this BindingExpression uses.
    - public Binding ParentBinding { get; }
    - public object ResolvedSource { get; }
    - public string ResolvedSourcePropertyName { get; }

- methods
    - public override void UpdateSource ();
    - public override void UpdateTarget ();

- FrameworkElement
 
    - public BindingExpression GetBindingExpression (DependencyProperty dp); // id. BindingOperations.GetBindingExpression

- IValueConverter : Convert, ConvertBack

- Validation : Binding.ValidationRules, Validation attached properties & events, IDataErrorInfo / Source
    - dépend de 
        - Binding.ValidationRules
        - règles standard introduites par : 
            - Binding.ValidatesOnDataErrors : DataErrorValidationRule (ValidationStep = UpdatedValue)
            - Binding.ValidatesOnExceptions : ExceptionValidationRule
            - Binding.ValidatesOnNotifyDataErrors : NotifyDataErrorValidationRule
    - appelle Binding.UpdateSourceExceptionFilter en cas d'exception déclenchée par la Source lors d'un update 
    - met à jour BoundElement.Validation.HasErrors, BoundElement.Validation.Errors
    - ExceptionValidationRule : rule that checks for exceptions that are thrown during the update of the binding source property.
    - DataErrorValidationRule : rule that checks for errors that are raised by the IDataErrorInfo implementation of the source object.
    - NotifyDataErrorValidationRule : rule that checks for errors that are raised by the INotifyDataErrorInfo implementation of the source object.

    - Displaying Validation Errors
        - Validation.ErrorTemplate : ControlTemplate used to generate validation error feedback on the adorner layer.
        - ContentPresenter : ex: <ContentPresenter Content="{Binding ElementName=yInput, Path=(Validation.Errors).CurrentItem}" ...
        - Tooltip : ex:
            <Style.Triggers>
                <Trigger Property="Validation.HasError" Value="true">
                    <Setter Property="ToolTip" Value="{Binding RelativeSource={x:Static RelativeSource.Self}, Path=(Validation.Errors)[0].ErrorContent}"/>
                    <Setter Property="Tag" Value="{Binding RelativeSource={x:Static RelativeSource.Self}, Path=(Validation.Errors)[0].ErrorContent}"/>
                </Trigger>
            </Style.Triggers>

- Update, Error Events

    - Binding class attached events :

        public static readonly RoutedEvent SourceUpdatedEvent;
        public static readonly RoutedEvent TargetUpdatedEvent;

    - Validation class 

        public static readonly System.Windows.RoutedEvent ErrorEvent;

    - FrameworkElement class :

        public event EventHandler<System.Windows.Data.DataTransferEventArgs> SourceUpdated;
        public event EventHandler<System.Windows.Data.DataTransferEventArgs> TargetUpdated;

        public bool NotifyOnSourceUpdated { get; set; }
        public bool NotifyOnTargetUpdated { get; set; }
        public bool NotifyOnValidationError { get; set; }

- Multibinding et multivalue converters
- Priority Binding
- Binding Groups

## [BindingBase](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.bindingbase?view=windowsdesktop-7.0)

## [Binding](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.binding?view=windowsdesktop-7.0)

## [BindingOperations](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.bindingoperations?view=windowsdesktop-7.0)

## [BindingExpressionBase](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.bindingexpressionbase?view=netframework-4.8)

## [BindingExpression](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.bindingexpression?view=windowsdesktop-7.0)

    public sealed class BindingExpression : BindingExpressionBase, 
                                                System.Windows.IWeakEventListener

- properties
    - public object DataItem { get; } // Gets the binding source object that this BindingExpression uses.
    - public Binding ParentBinding { get; }
    - public object ResolvedSource { get; }
    - public string ResolvedSourcePropertyName { get; }

- methods
    - public override void UpdateSource ();
    - public override void UpdateTarget ();

- A related class, BindingExpression, is the underlying object that maintains the connection between 
  the source and the target. 
- A binding contains all the information that can be shared across several binding expressions. 
- A BindingExpression is an instance expression that cannot be shared and contains all the instance information 
  of the Binding.

## FrameworkElement and Binding : FrameworkElement.GetBindingExpression, BindingOperations.GetBindingExpression 

    public BindingExpression GetBindingExpression (DependencyProperty dp);
    public static BindingExpression GetBindingExpression (DependencyObject target, DependencyProperty dp);

## Source / RelativeSource / ElementName

	{Binding Source={StaticResource data} Path=Age}

	{Binding RelativeSource={RelativeSource Self}}
	{Binding RelativeSource={RelativeSource TemplatedParent}}
	{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type StackPanel}}, Path=Orientation}
	{Binding RelativeSource={RelativeSource PreviousData}}

	{Binding ElementName=ValueSlider Path=Value}

## Path

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

## [UpdateSourceTrigger](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.updatesourcetrigger?view=windowsdesktop-7.0)

- Default : valeur définie par le FrameworkPropertyMetadata associé à la DependencyProperty target
- PropertyChanged
- LostFocus
- Explicit

- code:
 
	monTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource()

- FrameworkPropertyMetadata.DefaultUpdateSourceTrigger 

### [FrameworkPropertyMetadata](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkpropertymetadata?view=windowsdesktop-7.0)

## Converter, ConverterCulture , ConverterParameter

### [IValueConverter](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.ivalueconverter?view=windowsdesktop-7.0)

    // Source -> Target
    - public object Convert (object value, Type targetType, object parameter, 
                                System.Globalization.CultureInfo culture);

        - value : The value produced by the binding source.

    // Target -> Source
    - public object ConvertBack (object value, Type targetType, object parameter, 
                                System.Globalization.CultureInfo culture);

         - value : The value that is produced by the binding target.

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

## [Data Validation](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-7.0#data-validation)

- The WPF data binding model enables you to associate ValidationRules with your Binding object. 
- Validation occurs during binding target-to-binding source value transfer before the converter is called ...

### [Validation Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.validation?view=windowsdesktop-7.0)

- public static class Validation

### [Validation.HasError, .Errors, .Error, .ErrorTemplate Attached Properties and Event](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.validation.errors?view=windowsdesktop-6.0)

BoundElement : DependencyObject owning the DependencyProperty target of the Binding ...?

When a value is being transferred from the target property to the source property:

- clear BoundElement[Validation.Errors]

- if Binding.ValidationRules != null
    - call the Validate method on each of the ValidationRules until one of them runs into an error 
      or until all of them pass.
	- once there is a custom rule that does not pass, create a ValidationError object 
      and add it to BoundElement[Validation.Errors]
    - if BoundElement[Validation.Errors] not empty then raise the 
      BoundElement[Validation.Error] attached event
    - l'appel des ValidationRules se fait en plusieurs fois, chaque ValidationRules précisant un .ValidationStep
    
- if no error then call the Binding.Converter, if one exists.

- if the Binding.Converter passes, call the setter of the Source property.

- if the Binding.ValidationRules has an ExceptionValidationRule and an exception is thrown during the setter call: 
    - if there is a Binding.UpdateSourceExceptionFilter call it 
    - else create a ValidationError with the exception and add it to BoundElement[Validation.Errors].

- les propriétés Binding.ValidatesOnDataErrors , .ValidatesOnExceptions , .ValidatesOnNotifyDataErrors déclenchent 
  l'inclusion ou non d'instances dans Binding.ValidationRules :

    - public bool ValidatesOnDataErrors { get; set; } // Gets or sets a value that indicates whether to include the DataErrorValidationRule.
    - public bool ValidatesOnExceptions { get; set; } // Gets or sets a value that indicates whether to include the ExceptionValidationRule.
    - public bool ValidatesOnNotifyDataErrors { get; set; } // Gets or sets a value that indicates whether to include the NotifyDataErrorValidationRule.
    
### [ValidationRules](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.validationrules?view=windowsdesktop-6.0)

	public System.Collections.ObjectModel.Collection<System.Windows.Controls.ValidationRule> ValidationRules { get; }

> The WPF data binding model enables you to associate ValidationRules with your Binding or MultiBinding object. You can create custom rules by deriving from the ValidationRule class and implementing the Validate method, or you can use the built-in ExceptionValidationRule, which invalidates a value if there are exceptions during source updates.

- clause AND entre les Validation rules
- appelées dans le sens Target -> Source, avant la conversion, faite ssi la validation réussit

#### [ValidationRule class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.validationrule?view=windowsdesktop-7.0)

- public abstract class ValidationRule
- Inheritance : Object ValidationRule
- Derived : DataErrorValidationRule, ExceptionValidationRule, NotifyDataErrorValidationRule
- Properties
    - public bool ValidatesOnTargetUpdated { get; set; }
    - public ValidationStep ValidationStep { get; set; }
- possibilité de définir ses propres ValidationRules

### [DataErrorValidationRule](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.dataerrorvalidationrule?view=windowsdesktop-7.0)

Represents a rule that checks for errors that are raised by the IDataErrorInfo implementation of the source object.

#### [NotifyDataErrorValidationRule](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.notifydataerrorvalidationrule?view=windowsdesktop-8.0)

Represents a rule that checks for errors that are raised by a data source that implements INotifyDataErrorInfo.

### [ExceptionValidationRule](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.exceptionvalidationrule?view=windowsdesktop-8.0)

Represents a rule that checks for exceptions that are thrown during the update of the binding source property.

### [ValidationStep Enum](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.validationstep?view=windowsdesktop-7.0)

| Value      | Description |
| - | - |
| CommittedValue | Runs the ValidationRule after the value has been committed to the source.|
| ConvertedProposedValue | Runs the ValidationRule after the value is converted.|
| RawProposedValue | Runs the ValidationRule before any conversion occurs.|
| UpdatedValue | Runs the ValidationRule after the source is updated.|

### Displaying Validation Errors

#### Validation.ErrorTemplate + AdornedElementPlaceholder

- gets or sets the ControlTemplate used to generate validation error feedback on the adorner layer.
- le template sert à produire un bloc d'UI apparaissant dans le AdornedElementPlaceholder du Template du Target Element. 

##### Exemple

    <TextBox ... Validation.ErrorTemplate="{StaticResource ValidatedTextBoxTemplate}">

#### Tooltip

##### Exemple

        <Style TargetType="{x:Type local:CustomTextBox}">
            <Style.Triggers>
                <Trigger Property="Validation.HasError" Value="true">
                    <Setter Property="ToolTip" Value="{Binding RelativeSource={x:Static RelativeSource.Self}, Path=(Validation.Errors)[0].ErrorContent}"/>
                    <Setter Property="Tag" Value="{Binding RelativeSource={x:Static RelativeSource.Self}, Path=(Validation.Errors)[0].ErrorContent}"/>
                </Trigger>
            </Style.Triggers>
        </Style>

#### ContentPresenter

##### Exemple

	<TextBox Name="yInput" ... TextBox>
	<ContentPresenter ... Content="{Binding ElementName=yInput, Path=(Validation.Errors).CurrentItem}" />

#### Exemple

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

### [UpdateSourceExceptionFilter](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.updatesourceexceptionfilter?view=windowsdesktop-6.0) 

- Gets or sets a handler you can use to provide custom logic for handling exceptions that the binding engine 
  encounters during the update of the binding source value. 

- - This is only applicable if you have associated an __ExceptionValidationRule__ with your binding 
  in its __ValidationRules__ property.

- public UpdateSourceExceptionFilterCallback UpdateSourceExceptionFilter { get; set; }

    If an UpdateSourceExceptionFilter is not specified on the Binding, the binding engine creates a 
    ValidationError with the exception and adds it to the Validation.Errors collection of the bound element.

    public delegate object UpdateSourceExceptionFilterCallback(object bindExpression, Exception exception);

    - return value :

        - null :
                	- To ignore any exceptions. 
                    - The default behavior (if there is no UpdateSourceExceptionFilterCallback) 
                      is to create a ValidationError with the exception and adds it to the Errors collection 
                      of the bound element.

        - any object : 
                    - To create a ValidationError object with the ErrorContent set to that object.
                    - The ValidationError object is added to Errors collection of the bound element.

        - a ValidationError object :
                    - To set the BindingExpression or MultiBindingExpression object as the BindingInError. 
                    - The ValidationError object is added to Errors collection of the bound element.

#### [Article](https://www.codeproject.com/Articles/863291/Validation-in-WPF)

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

### [ValidatesOnDataErrors](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.validatesondataerrors?view=windowsdesktop-6.0)

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

### [ValidatesOnExceptions](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.validatesonexceptions?view=windowsdesktop-6.0)

	public bool ValidatesOnExceptions { get; set; }

> Setting this property provides an alternative to using the ExceptionValidationRule element explicitly. The ExceptionValidationRule is a built-in validation rule that checks for exceptions that are thrown during the update of the source property. If an exception is thrown, the binding engine creates a ValidationError with the exception and adds it to the Validation.Errors collection of the bound element. The lack of an error clears this validation feedback, unless another rule raises a validation issue.

#### Exemple

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

### [ValidatesOnNotifyDataErrors](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.binding.validatesonnotifydataerrors?view=windowsdesktop-6.0)

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

## Update Events

- Binding class attached events

    public class Binding : System.Windows.Data.BindingBase
    {
        public static readonly System.Windows.RoutedEvent SourceUpdatedEvent;
        public static readonly System.Windows.RoutedEvent TargetUpdatedEvent;

    }

- events reflétés par FrameworkElement :

    public event EventHandler<System.Windows.Data.DataTransferEventArgs> SourceUpdated;
    public event EventHandler<System.Windows.Data.DataTransferEventArgs> TargetUpdated;

    avec:

    [DataTransferEventArgs](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.datatransfereventargs?view=windowsdesktop-7.0)

    public System.Windows.DependencyObject TargetObject { get; }
    public System.Windows.DependencyProperty Property { get; }

## [BindingGroup](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.bindinggroup?view=windowsdesktop-6.0)

> Contains a collection of bindings and ValidationRule objects that are used to validate an object.

Un ensemble de Bindings peuvent faire référence à un __BindingGroup__ par son nom en assignant leur 
propriété __BindingGroupName__.

Ce BindingGroup doit être explicitement initié (__BeginEdit__) puis commité (__CommitEdit__) ou abandonné (__CancelEdit__)
pour que les Bindings qui s'y sont rattachés soient exécutés. 

### Exemple

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

#### Code:

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

#### ValidationRule:

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

### Binding.NotifyOnSourceUpdated  / NotifyOnTargetUpdated 

  Si __NotifyOnSourceUpdated__ / __NotifyOnTargetUpdated__ == true un évènement __SourceUpdated__ / __TargetUpdated__ est généré

	<TextBox x:Name="day"
			 Grid.Row="0"
			 Margin="2"
			 Text="{Binding Path=Value, 
					NotifyOnSourceUpdated=True, 
					NotifyOnTargetUpdated=True, 
					UpdateSourceTrigger=PropertyChanged}"
			 SourceUpdated="OnSourceUpdated"
			 TargetUpdated="OnTargetUpdated" />

### Binding.NotifyOnValidationError : attached event Validation.Error

   détermine si l'Attached Event Validation.Error doit être déclenché en cas d'erreur de validation

	<TextBox Grid.Row="0"
				Margin="2"
				Text="{Binding Path=Value, 
						NotifyOnValidationError=True, 
						UpdateSourceTrigger=PropertyChanged, 
						ValidatesOnExceptions=True}"
				Validation.Error="OnError" />

## Multibinding et multivalue converters

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

## [Priority Binding](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/how-to-implement-prioritybinding?view=netframeworkdesktop-4.8)

### Exemple

````
    <TextBlock.Text>
      <PriorityBinding FallbackValue="defaultvalue">
        <Binding Path="SlowestDP" IsAsync="True"/>
        <Binding Path="SlowerDP" IsAsync="True"/>
        <Binding Path="FastDP" />
      </PriorityBinding>
    </TextBlock.Text>
````

````
public class AsyncDataSource
{
  private string _fastDP;
  private string _slowerDP;
  private string _slowestDP;

  public AsyncDataSource()
  {
  }

  public string FastDP
  {
    get { return _fastDP; }
    set { _fastDP = value; }
  }

  public string SlowerDP
  {
    get
    {
      // This simulates a lengthy time before the
      // data being bound to is actualy available.
      Thread.Sleep(3000);
      return _slowerDP;
    }
    set { _slowerDP = value; }
  }

  public string SlowestDP
  {
    get
    {
      // This simulates a lengthy time before the
      // data being bound to is actualy available.
      Thread.Sleep(5000);
      return _slowestDP;
    }
    set { _slowestDP = value; }
  }
}
````

## [BindingBase.FallbackValue](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.bindingbase.fallbackvalue?view=windowsdesktop-6.0)

	public object FallbackValue { get; set; }

> Gets or sets the value to use when the binding source is unable to return a value.

## [TargetNullValue](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.bindingbase.targetnullvalue?view=windowsdesktop-6.0)

	public object TargetNullValue { get; set; }

> Gets or sets the value that is used in the target when the value of the source is null.
