
# Binding

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

        - public object Source { get; set; }
        - public string ElementName { get; set; }
        - public RelativeSource RelativeSource { get; set; }

        - public PropertyPath Path { get; set; }

        - public BindingMode Mode { get; set; }
        - public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

        - public IValueConverter Converter { get; set; }
        - public System.Globalization.CultureInfo ConverterCulture { get; set; }
        - public object ConverterParameter { get; set; }

        - public bool IsAsync { get; set; }

        - public bool NotifyOnSourceUpdated { get; set; }
        - public bool NotifyOnTargetUpdated { get; set; }
        - public bool NotifyOnValidationError { get; set; }

    - attached events

        - public static readonly System.Windows.RoutedEvent SourceUpdatedEvent;
        - public static readonly System.Windows.RoutedEvent TargetUpdatedEvent;



- IValueConverter

    // Source -> Target
    - public object Convert (object value, Type targetType, object parameter, 
                                System.Globalization.CultureInfo culture);
        - value : The value produced by the binding source.

    // Target -> Source
    - public object ConvertBack (object value, Type targetType, object parameter, 
                                System.Globalization.CultureInfo culture);

         - value : The value that is produced by the binding target.

- classe BindingOperations

    - SetBinding

        public static BindingExpressionBase SetBinding(DependencyObject target, 
                                                DependencyProperty dp, 
                                                BindingBase binding);

    - GetBinding

    public static Binding GetBinding (DependencyObject target, DependencyProperty dp);

- FrameworkElement
 
    - public BindingExpression GetBindingExpression (DependencyProperty dp);

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

Provides static methods to manipulate bindings, including Binding, MultiBinding, and PriorityBinding objects.



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
 
	monTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource() .

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

## Validation

### Validation.ErrorTemplate

	Validation.ErrorTemplate Attached Property

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

### ValidationRules

	public System.Collections.ObjectModel.Collection<Controls.ValidationRule> ValidationRules { get; }

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

