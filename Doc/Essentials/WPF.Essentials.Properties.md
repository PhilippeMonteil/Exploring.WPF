
# [Properties](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/properties-wpf?view=netframeworkdesktop-4.8)

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

## [Dependency Property Value Precedence](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/dependency-property-value-precedence?view=netframeworkdesktop-4.8)

