
# [Properties](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/properties-wpf?view=netframeworkdesktop-4.8)

## [Dependency Object](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject?view=windowsdesktop-7.0)

### Inheritance : Object / DispatcherObject / DependencyObject

### [SetValue](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject.setvalue?view=windowsdesktop-7.0)

	public void SetValue (System.Windows.DependencyProperty dp, object value);

#### Exceptions

##### InvalidOperationException : Attempted to modify a read-only dependency property, or a property on a sealed DependencyObject.
##### ArgumentException : value was not the correct type as registered for the dp property.

	public void SetValue (System.Windows.DependencyPropertyKey key, object value);

#### cette version de SetValue, utilisant une DependencyPropertyKey, peut être invoquée sur une DP readonly

- Sets the local value of a dependency property
 
### [SetCurrentValue](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject.setcurrentvalue?view=windowsdesktop-7.0)

	public void SetCurrentValue (System.Windows.DependencyProperty dp, object value);

- This method is used by a component that programmatically sets the value of one of its own properties 
  without disabling an application's declared use of the property. 
- The SetCurrentValue method changes the effective value of the property, but existing triggers, 
  data bindings, and styles will continue to work.

### [GetValue](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject.getvalue?view=windowsdesktop-7.0)

	public object GetValue (System.Windows.DependencyProperty dp);

### [ReadLocalValue](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject.readlocalvalue?view=windowsdesktop-7.0)

	public object ReadLocalValue (System.Windows.DependencyProperty dp);

- You should use GetValue for most typical "get" operations for a dependency property. 
- ReadLocalValue does not return the effective value for a variety of circumstances where the value 
  was not locally set.
- Values that are set by styles, themes, templates, the default value from metadata, 
  or property value inheritance are not considered to be local values. 
- Bindings and other expressions are considered to be local values, after they have been evaluated.
- When no local value is set, this method returns DependencyProperty.UnsetValue.

### [ClearValue](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject.clearvalue?view=windowsdesktop-7.0)

	public void ClearValue (System.Windows.DependencyProperty dp);
	public void ClearValue (System.Windows.DependencyPropertyKey key);

### [InvalidateProperty](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject.invalidateproperty?view=windowsdesktop-7.0)

	public void InvalidateProperty (System.Windows.DependencyProperty dp);

- Re-evaluates the effective value for the specified dependency property.

### [OnPropertyChanged](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject.onpropertychanged?view=windowsdesktop-7.0)

	protected virtual void OnPropertyChanged (System.Windows.DependencyPropertyChangedEventArgs e);

### [GetLocalValueEnumerator](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject.getlocalvalueenumerator?view=windowsdesktop-7.0)

### 

## [Dependency properties overview](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/dependency-properties-overview?view=netframeworkdesktop-4.8)

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

### Register/Attached/Readonly -> DependencyProperty / DependencyPropertyKey

	public static System.Windows.DependencyProperty Register (string name, Type propertyType, Type ownerType, System.Windows.PropertyMetadata typeMetadata, System.Windows.ValidateValueCallback validateValueCallback);
	public static System.Windows.DependencyPropertyKey RegisterReadOnly (string name, Type propertyType, Type ownerType, System.Windows.PropertyMetadata typeMetadata, System.Windows.ValidateValueCallback validateValueCallback);

	public static System.Windows.DependencyProperty RegisterAttached (string name, Type propertyType, Type ownerType, System.Windows.PropertyMetadata defaultMetadata, System.Windows.ValidateValueCallback validateValueCallback);
	public static System.Windows.DependencyPropertyKey RegisterAttachedReadOnly (string name, Type propertyType, Type ownerType, System.Windows.PropertyMetadata defaultMetadata, System.Windows.ValidateValueCallback validateValueCallback);

### [PropertyMetadata](https://learn.microsoft.com/en-us/dotnet/api/system.windows.propertymetadata?view=windowsdesktop-7.0)

### [FrameworkPropertyMetadata](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkpropertymetadata?view=windowsdesktop-7.0)

### [DependencyProperty.OverrideMetadata](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/properties/how-to-override-metadata-for-a-dependency-property?view=netdesktop-7.0)

	public void OverrideMetadata (Type forType, System.Windows.PropertyMetadata typeMetadata);

Exemple d'usage : changer la valeur par défaut d'une Dependency Property

### [DependencyProperty.AddOwner](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyproperty.addowner?view=windowsdesktop-7.0)

	public System.Windows.DependencyProperty AddOwner (Type ownerType, System.Windows.PropertyMetadata typeMetadata);

## [Dependency Property Value Precedence](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/dependency-property-value-precedence?view=netframeworkdesktop-4.8)

