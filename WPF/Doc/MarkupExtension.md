
# [MarkupExtension](https://docs.microsoft.com/en-us/dotnet/api/system.windows.markup.markupextension?view=windowsdesktop-6.0)

A markup extension can be implemented to provide values for properties in an attribute usage, 
properties in a property element usage, or both.

## [XAML-Defined Markup Extensions](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/markup-extensions-and-wpf-xaml?view=netframeworkdesktop-4.8#xaml-defined-markup-extensions)

- [x:Type](https://docs.microsoft.com/en-us/dotnet/desktop/xaml-services/xtype-markup-extension)
- [x:Static](https://docs.microsoft.com/en-us/dotnet/desktop/xaml-services/xstatic-markup-extension)
- [x:Null](https://docs.microsoft.com/en-us/dotnet/desktop/xaml-services/xnull-markup-extension)
- [x:Array](https://docs.microsoft.com/en-us/dotnet/desktop/xaml-services/xarray-markup-extension)

## [WPF-Specific Markup Extensions](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/markup-extensions-and-wpf-xaml?view=netframeworkdesktop-4.8#wpf-specific-markup-extensions)

- [StaticResource](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/staticresource-markup-extension?view=netframeworkdesktop-4.8)
- [DynamicResource](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/dynamicresource-markup-extension?view=netframeworkdesktop-4.8)
- [Binding](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/binding-markup-extension?view=netframeworkdesktop-4.8)
- [RelativeSource](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/relativesource-markupextension?view=netframeworkdesktop-4.8)
- [TemplateBinding](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/templatebinding-markup-extension?view=netframeworkdesktop-4.8)
- [ColorConvertedBitmap](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/colorconvertedbitmap-markup-extension?view=netframeworkdesktop-4.8)
- [ComponentResourceKey](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/componentresourcekey-markup-extension?view=netframeworkdesktop-4.8)
- [ThemeDictionary](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/themedictionary-markup-extension?view=netframeworkdesktop-4.8)

## [Overview of markup extensions for XAML](https://docs.microsoft.com/en-us/dotnet/desktop/xaml-services/markup-extensions-overview)

## [Markup Extensions and WPF XAML](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/markup-extensions-and-wpf-xaml?view=netframeworkdesktop-4.8)

## Class

	public abstract class MarkupExtension

## Inheritance

- Object
- MarkupExtension

## Derived

- System.Activities.Presentation.CachedResourceDictionaryExtension
- System.Activities.XamlIntegration.DynamicUpdateMapExtension
- System.Activities.XamlIntegration.PropertyReferenceExtension<T>
- System.ServiceModel.EndpointIdentityExtension
- System.ServiceModel.XamlIntegration.SpnEndpointIdentityExtension
- System.ServiceModel.XamlIntegration.UpnEndpointIdentityExtension
- System.ServiceModel.XamlIntegration.XPathMessageContextMarkupExtension
- System.Windows.ColorConvertedBitmapExtension
- System.Windows.Data.BindingBase
- System.Windows.Data.RelativeSource
- System.Windows.DynamicResourceExtension
- System.Windows.Markup.ArrayExtension
- System.Windows.Markup.NullExtension
- System.Windows.Markup.Reference
- System.Windows.Markup.StaticExtension
- System.Windows.Markup.TypeExtension
- System.Windows.ResourceKey
- System.Windows.StaticResourceExtension
- System.Windows.TemplateBindingExtension
- System.Windows.ThemeDictionaryExtension

## Method

	public abstract object ProvideValue (IServiceProvider serviceProvider);

## [Note](https://docs.microsoft.com/en-us/dotnet/api/system.windows.markup.markupextension.providevalue?view=windowsdesktop-6.0#notes-to-implementers)

Common services returned by the default service provider that is typically available to a 
custom or existing MarkupExtension implementation include the following primary services.

__IProvideValueTarget__ reports the object reference and a property identifier from the context 
where the markup extension is used.

	public object TargetObject { get; 

	public object TargetProperty { get; }

...

