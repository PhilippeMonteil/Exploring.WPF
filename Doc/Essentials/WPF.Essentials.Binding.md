
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

- classe Data.Binding (1/2)

    - Inheritance : Object - MarkupExtension - BindingBase - Binding

    - properties

        - public object Source { get; set; } // Default : DataContext du DependencyObject Target du Binding
        - public string ElementName { get; set; }
        - public RelativeSource RelativeSource { get; set; }

        - public PropertyPath Path { get; set; }

        - public BindingMode Mode { get; set; }
        - public UpdateSourceTrigger UpdateSourceTrigger { get; set; } // si Mode == TwoWay ou OneWayToSource

        - public IValueConverter Converter { get; set; }
        - public System.Globalization.CultureInfo ConverterCulture { get; set; }
        - public object ConverterParameter { get; set; }

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
 
    - public BindingExpression GetBindingExpression (DependencyProperty dp);

- classe Data.Binding (2/2)

        - public bool IsAsync { get; set; }

        - public bool NotifyOnSourceUpdated { get; set; }
        - public bool NotifyOnTargetUpdated { get; set; }
        - public bool NotifyOnValidationError { get; set; }

            - attached events

                - public static readonly System.Windows.RoutedEvent SourceUpdatedEvent;
                - public static readonly System.Windows.RoutedEvent TargetUpdatedEvent;

                events reflétés par FrameworkElement :

                public event EventHandler<System.Windows.Data.DataTransferEventArgs> SourceUpdated;
                public event EventHandler<System.Windows.Data.DataTransferEventArgs> TargetUpdated;

                avec:

                [DataTransferEventArgs](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.datatransfereventargs?view=windowsdesktop-7.0)

                    public System.Windows.DependencyObject TargetObject { get; }
                    public System.Windows.DependencyProperty Property { get; }

        - public bool BindsDirectlyToSource { get; set; }

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

- IValueConverter

    // Source -> Target
    - public object Convert (object value, Type targetType, object parameter, 
                                System.Globalization.CultureInfo culture);

        - value : The value produced by the binding source.

    // Target -> Source
    - public object ConvertBack (object value, Type targetType, object parameter, 
                                System.Globalization.CultureInfo culture);

         - value : The value that is produced by the binding target.

- Validation



- 4 elements : Target object (DependencyObject) / Target property (DependencyProperty) / Source object / Source property (Path) 

- Source / RelativeSource / ElementName
    - Source : par défaut DataContext
    - RelativeSource = Self / TemplatedParent / FindAncestor
    - ElementName
- Path / XPath
- UpdateSourceTrigger
    - Defaut (cf FrameworkPropertyMetadata.DefaultUpdateSourceTrigger )
    - PropertyChanged
    - LostFocus / 
    - Explicit
- Mode : Default (cf metadata) / OneTime / OneWay / OneWayToSource / TwoWay
- Converter, ConverterCulture , ConverterParameter
- Validation:
    - dépend de 
        - Binding.ValidationRules
        - Binding.ValidatesOnDataErrors
        - Binding.ValidatesOnExceptions
        - Binding.ValidatesOnNotifyDataErrors
    - appelle Binding.UpdateSourceExceptionFilter en cas d'exception déclenchée par la Source lors d'un update 
    - met à jour BoundElement.Validation.HasErrors, BoundElement.Validation.Errors
    - produit un UI d'affichage des erreurs de validation avec BoundElement.Validation.ErrorTemplate

## [BindingBase](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.bindingbase?view=windowsdesktop-7.0)

- public abstract class BindingBase : Markup.MarkupExtension
- Inheritance : Object, MarkupExtension, BindingBase
- Derived : Binding / MultiBinding / PriorityBinding
- properties
    - public string BindingGroupName { get; set; }
    - public int Delay { get; set; }
    - public object FallbackValue { get; set; }
    - public string StringFormat { get; set; }
    - public object TargetNullValue { get; set; }

## [Binding](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.binding?view=windowsdesktop-7.0)

## [BindingOperations](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.bindingoperations?view=windowsdesktop-7.0)

    public static BindingExpressionBase SetBinding (DependencyObject target, DependencyProperty dp, BindingBase binding);
    public static Binding GetBinding (DependencyObject target, DependencyProperty dp);

    public static BindingExpression GetBindingExpression (DependencyObject target, DependencyProperty dp);
    public static bool IsDataBound (System.Windows.DependencyObject target, System.Windows.DependencyProperty dp);

## [BindingExpressionBase](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.bindingexpressionbase?view=netframework-4.8)

    public abstract class BindingExpressionBase : System.Windows.Expression, 
                                                    System.Windows.IWeakEventListener

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

- Default / contrôle: ex: perte de focus
- PropertyChanged
- LostFocus
- Explicit

- code:
 
	monTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource()

- FrameworkPropertyMetadata.DefaultUpdateSourceTrigger 

### [FrameworkPropertyMetadata](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkpropertymetadata?view=windowsdesktop-7.0)

## Converter, ConverterCulture , ConverterParameter

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
- Validation occurs during binding target-to-binding source value transfer before the converter is called

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

- if no error then call the Binding.Converter, if one exists.

- if the Binding.Converter passes, call the setter of the Source property.

- if the Binding.ValidationRules has an ExceptionValidationRule and an exception is thrown during the setter call: 
    - if there is a Binding.UpdateSourceExceptionFilter call it 
    - else create a ValidationError with the exception and add it to Target[Validation.Errors].

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

#### [ValidationStep Enum](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.validationstep?view=windowsdesktop-7.0)

| Value      | Description |
| - | - |
| CommittedValue | Runs the ValidationRule after the value has been committed to the source.|
| ConvertedProposedValue | Runs the ValidationRule after the value is converted.|
| RawProposedValue | Runs the ValidationRule before any conversion occurs.|
| UpdatedValue | Runs the ValidationRule after the source is updated.|

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
- This is only applicable if you have associated an __ExceptionValidationRule__ with your binding 
  in its __ValidationRules__ property.

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
