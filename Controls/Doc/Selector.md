
# [Selector](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.primitives.selector?view=windowsdesktop-6.0)

	Represents a control that allows a user to select items from among its child elements.

## Class

	[System.Windows.Localizability(System.Windows.LocalizationCategory.None, 
	Readability=System.Windows.Readability.Unreadable)]
	public abstract class Selector : System.Windows.Controls.ItemsControl

## Inheritance

Object
DispatcherObject
DependencyObject
Visual
UIElement
FrameworkElement
Control
ItemsControl
Selector

## Derived

System.Windows.Controls.ComboBox
System.Windows.Controls.ListBox
System.Windows.Controls.Primitives.MultiSelector
System.Windows.Controls.Ribbon.Ribbon
System.Windows.Controls.TabControl

## Properties

	[System.ComponentModel.Bindable(true)]
	public object SelectedItem { get; set; }

	[System.ComponentModel.Bindable(true)]
	[System.Windows.Localizability(System.Windows.LocalizationCategory.NeverLocalize)]
	[System.ComponentModel.TypeConverter("System.Windows.NullableBoolConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	public bool? IsSynchronizedWithCurrentItem { get; set; }

	[System.ComponentModel.Bindable(true)]
	public object SelectedItem { get; set; }

	[System.ComponentModel.Bindable(true)]
	[System.Windows.Localizability(System.Windows.LocalizationCategory.NeverLocalize)]
	public object SelectedValue { get; set; }

	[System.ComponentModel.Bindable(true)]
	[System.Windows.Localizability(System.Windows.LocalizationCategory.NeverLocalize)]
	public string SelectedValuePath { get; set; }

