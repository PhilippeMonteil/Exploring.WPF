
# Binding

## En résumé

- Source / RelativeSource / ElementName
- Path / XPath
- UpdateSourceTrigger : Defaut (cf metadata) / PropertyChanged / LostFocus / Explicit
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

## UpdateSourceTrigger

- Default / contrôle: ex: perte de focus
- PropertyChanged
- LostFocus
- Explicit

- code:
 
	monTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource() .

- FrameworkPropertyMetadata.DefaultUpdateSourceTrigger 

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

## ValidationRules

	public System.Collections.ObjectModel.Collection<System.Windows.Controls.ValidationRule> ValidationRules { get; }

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

