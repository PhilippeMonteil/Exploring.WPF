
# [Properties](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/properties-wpf?view=netframeworkdesktop-4.8)

## En résumé

- DependencyProperty, DependencyPropertyKey

    - Register/Attached/Readonly -> DependencyProperty / DependencyPropertyKey
    	public static DependencyProperty DependencyProperty.Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback);

	- Register -> DependencyProperty

	- RegisterReadOnly -> DependencyPropertyKey
    	- une DependencyPropertyKey peut être utilisée pour assigner une valeur à une DependencyProperty
    		public void DependencyObject.SetValue(DependencyPropertyKey key, object value);
        - elle expose une propriété .DependencyProperty qui peut être utilisée pour lire 
    		public object DependencyObject.GetValue(DependencyProperty dp);
          mais pas pour écrire une valeur dans la DependencyProperty : SetValue
    		  public void DependencyObject.SetValue (DependencyProperty dp, object value);
          provoquerait une exception

    - OverrideMetaData
    	- modifier la MetaData d'une DependencyProperty pour un type dérivant de celui
		  ayant déclaré ladite DependencyProperty.
    	public void OverrideMetadata (Type forType, PropertyMetadata typeMetadata, DependencyPropertyKey key);
		- modifier la valeur défaut d'une DependencyProperty pour un sous-type donné, par exemple

    - AddOwner
        - public DependencyProperty DependencyProperty.AddOwner(Type ownerType, PropertyMetadata typeMetadata);

- PropertyMetadata 


## [DependencyProperty](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyproperty?view=windowsdesktop-7.0)

### [Dependency properties overview](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/dependency-properties-overview?view=netframeworkdesktop-4.8)

- The purpose of dependency properties is to provide a way to compute the value of a property based 
  on the value of other inputs. 
- These other inputs might include system properties such as themes and user preference, 
  just-in-time property determination mechanisms such as data binding and animations/storyboards, 
  multiple-use templates such as resources and styles, 
  or values known through parent-child relationships with other elements in the element tree. 
- In addition, a dependency property can be implemented to provide self-contained validation, 
  default values, callbacks that monitor changes to other properties, 
  and a system that can coerce property values based on potentially runtime information. 
- Derived classes can also change some specific characteristics of an existing property 
  by overriding dependency property metadata, rather than overriding the actual implementation 
  of existing properties or creating new properties.

### Classe

- Classe : public sealed class DependencyProperty
- TypeConverter : DependencyPropertyConverter

### Register/Attached/Readonly -> DependencyProperty / DependencyPropertyKey

	public static DependencyProperty Register (string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback);
	public static DependencyPropertyKey RegisterReadOnly (string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback);

	public static DependencyProperty RegisterAttached (string name, Type propertyType, Type ownerType, PropertyMetadata defaultMetadata, ValidateValueCallback validateValueCallback);
	public static DependencyPropertyKey RegisterAttachedReadOnly (string name, Type propertyType, Type ownerType, PropertyMetadata defaultMetadata, ValidateValueCallback validateValueCallback);

### [DependencyPropertyKey](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencypropertykey?view=windowsdesktop-7.0)

- DependencyProperty.RegisterReadOnly -> DependencyPropertyKey
- DependencyPropertyKey.DependencyProperty 
    - public DependencyProperty DependencyProperty { get; }

## [DependencyProperty metadata](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/properties/dependency-property-metadata?view=netdesktop-7.0)

- données assignées à une DependencyProperty :
    - lors de son enregistrement
    - surchargeable par un type dérivé (OverrideMetadata)
    - surchargeable par un type 'prenant possession' d'une DependencyProperty (AddOwner)

- contenu :
    - valeur par défaut
	- coercion value callbacks and property change callbacks on the owner type
	- WPF framework-level dependency property characteristics : the framework layout engine and the property inheritance logic

- DependencyProperty.GetMetadata

    - d'une DependencyProperty donnée, exposée par une Type donné ou instance de DependencyObject donnée :

	public PropertyMetadata GetMetadata (Type forType);
	public PropertyMetadata GetMetadata (DependencyObject dependencyObject);

- classes dérivées de PropertyMetadata : PropertyMetadata -> UIPropertyMetadata -> FrameworkPropertyMetadata 

- FrameworkPropertyMetadata
    - Affects/Parent/Measure/Arrange
	- Inherits
	- Binding : NotDataBindable, BindsTwoWayByDefault

### [PropertyMetadata](https://learn.microsoft.com/en-us/dotnet/api/propertymetadata?view=windowsdesktop-7.0)

### [UIPropertyMetadata](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uipropertymetadata?view=windowsdesktop-7.0)

### [FrameworkPropertyMetadata](https://learn.microsoft.com/en-us/dotnet/api/frameworkpropertymetadata?view=windowsdesktop-7.0)

### [FrameworkPropertyMetadataOptions](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkpropertymetadataoptions?view=windowsdesktop-7.0)

### [Overriding metadata](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/properties/dependency-property-metadata?view=netdesktop-7.0#overriding-metadata)

#### [DependencyProperty.OverrideMetadata](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/properties/how-to-override-metadata-for-a-dependency-property?view=netdesktop-7.0)

	public void OverrideMetadata (Type forType, PropertyMetadata typeMetadata);
	public void OverrideMetadata (Type forType, PropertyMetadata typeMetadata, DependencyPropertyKey key);

#### Comments

- Changing the default value
- Changing or adding property-change callbacks
- Changing WPF framework property metadata options
- When you override a metadata characteristic, the new metadata value either replaces the original value 
  or they're merged :
    - DefaultValue, the new value will replace the existing default value
	- PropertyChangedCallback
        - merged
    	- the callback order is determined by class depth, where a callback registered by the base class 
		  in the hierarchy would run first.
    - CoerceValueCallback
        - the new value will replace the existing CoerceValueCallback value. 
		- If you don't specify a CoerceValueCallback in the override metadata, the value comes from the nearest ancestor that specified CoerceValueCallback in metadata.

- Merge
    protected virtual void Merge (System.Windows.PropertyMetadata baseMetadata, System.Windows.DependencyProperty dp);

## [DependencyProperty : Add a class as an owner](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/properties/dependency-property-metadata?view=netdesktop-7.0#add-a-class-as-an-owner)

### [DependencyProperty.AddOwner](https://learn.microsoft.com/en-us/dotnet/api/dependencyproperty.addowner?view=windowsdesktop-7.0)

	public DependencyProperty AddOwner (Type ownerType, PropertyMetadata typeMetadata);

### Comments

- This method is typically used when the adding class isn't derived from the type that registered the 
  dependency property.
- In the AddOwner call, the adding class can create and assign type-specific metadata for the inherited 
  dependency property.
  
## [Attached properties overview](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/properties/attached-properties-overview?view=netdesktop-7.0)


## [Dependency Property Value Precedence](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/dependency-property-value-precedence?view=netframeworkdesktop-4.8)







## [DependencyObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject?view=windowsdesktop-7.0)

### En résumé

	public void SetValue (DependencyProperty dp, object value);
	public void SetValue (DependencyPropertyKey key, object value);
	public void SetCurrentValue (DependencyProperty dp, object value);
	public object GetValue (DependencyProperty dp);
	public object ReadLocalValue (DependencyProperty dp);
	public void ClearValue (DependencyProperty dp);
	public void ClearValue (DependencyPropertyKey key);
	public void InvalidateProperty (DependencyProperty dp);
	protected virtual void OnPropertyChanged (DependencyPropertyChangedEventArgs e);
	public LocalValueEnumerator GetLocalValueEnumerator ();

### Inheritance : Object / DispatcherObject / DependencyObject

### [SetValue](https://learn.microsoft.com/en-us/dotnet/api/dependencyobject.setvalue?view=windowsdesktop-7.0)

	public void SetValue (DependencyProperty dp, object value);

#### Exceptions

##### InvalidOperationException : Attempted to modify a read-only dependency property, or a property on a sealed DependencyObject.
##### ArgumentException : value was not the correct type as registered for the dp property.

	public void SetValue (DependencyPropertyKey key, object value);

#### cette version de SetValue, utilisant une DependencyPropertyKey, peut être invoquée sur une DP readonly

- Sets the local value of a dependency property
 
### [SetCurrentValue](https://learn.microsoft.com/en-us/dotnet/api/dependencyobject.setcurrentvalue?view=windowsdesktop-7.0)

	public void SetCurrentValue (DependencyProperty dp, object value);

- This method is used by a component that programmatically sets the value of one of its own properties 
  without disabling an application's declared use of the property. 
- The SetCurrentValue method changes the effective value of the property, but existing triggers, 
  data bindings, and styles will continue to work.

### [GetValue](https://learn.microsoft.com/en-us/dotnet/api/dependencyobject.getvalue?view=windowsdesktop-7.0)

	public object GetValue (DependencyProperty dp);

### [ReadLocalValue](https://learn.microsoft.com/en-us/dotnet/api/dependencyobject.readlocalvalue?view=windowsdesktop-7.0)

	public object ReadLocalValue (DependencyProperty dp);

- You should use GetValue for most typical "get" operations for a dependency property. 
- ReadLocalValue does not return the effective value for a variety of circumstances where the value 
  was not locally set.
- Values that are set by styles, themes, templates, the default value from metadata, 
  or property value inheritance are not considered to be local values. 
- Bindings and other expressions are considered to be local values, after they have been evaluated.
- When no local value is set, this method returns DependencyProperty.UnsetValue.

### [ClearValue](https://learn.microsoft.com/en-us/dotnet/api/dependencyobject.clearvalue?view=windowsdesktop-7.0)

	public void ClearValue (DependencyProperty dp);
	public void ClearValue (DependencyPropertyKey key);

- Clearing the property value by calling ClearValue does not necessarily give a dependency property the 
  default value that is specified in the dependency property metadata. 
- Clearing the property only specifically clears whatever local value may have been applied.

### [InvalidateProperty](https://learn.microsoft.com/en-us/dotnet/api/dependencyobject.invalidateproperty?view=windowsdesktop-7.0)

	public void InvalidateProperty (DependencyProperty dp);

- Re-evaluates the effective value for the specified dependency property.

### [OnPropertyChanged](https://learn.microsoft.com/en-us/dotnet/api/dependencyobject.onpropertychanged?view=windowsdesktop-7.0)

	protected virtual void OnPropertyChanged (DependencyPropertyChangedEventArgs e);

### [GetLocalValueEnumerator](https://learn.microsoft.com/en-us/dotnet/api/dependencyobject.getlocalvalueenumerator?view=windowsdesktop-7.0)

	public LocalValueEnumerator GetLocalValueEnumerator ();
