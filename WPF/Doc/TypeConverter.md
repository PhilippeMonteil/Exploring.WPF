
# [TypeConverter](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.typeconverter?view=net-6.0)

## [TypeConverters and XAML](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/typeconverters-and-xaml?view=netframeworkdesktop-4.8)

## Class

	public class TypeConverter

## Inheritance

- Object
- TypeConverter

## Derived

Microsoft.VisualBasic.Activities.XamlIntegration.VisualBasicSettingsConverter
Microsoft.VisualBasic.ApplicationServices.BuiltInRoleConverter
System.Activities.XamlIntegration.AssemblyReferenceConverter
System.Activities.XamlIntegration.DynamicUpdateMapConverter
System.Activities.XamlIntegration.DynamicUpdateMapItemConverter
System.Activities.XamlIntegration.ImplementationVersionConverter
System.Activities.XamlIntegration.TypeConverterBase
System.Activities.XamlIntegration.WorkflowIdentityConverter
System.ComponentModel.BaseNumberConverter
System.ComponentModel.BooleanConverter
System.ComponentModel.CharConverter
System.ComponentModel.CollectionConverter
System.ComponentModel.CultureInfoConverter
System.ComponentModel.DateOnlyConverter
System.ComponentModel.DateTimeConverter
System.ComponentModel.DateTimeOffsetConverter
System.ComponentModel.EnumConverter
System.ComponentModel.ExpandableObjectConverter
System.ComponentModel.GuidConverter
System.ComponentModel.MultilineStringConverter
System.ComponentModel.NullableConverter
System.ComponentModel.ReferenceConverter
System.ComponentModel.StringConverter
System.ComponentModel.TimeOnlyConverter
System.ComponentModel.TimeSpanConverter
System.ComponentModel.TypeListConverter
System.ComponentModel.VersionConverter
System.Configuration.ConfigurationConverterBase
System.Diagnostics.Design.LogConverter
System.Drawing.ColorConverter
System.Drawing.FontConverter
System.Drawing.FontConverter.FontNameConverter
System.Drawing.ImageConverter
System.Drawing.ImageFormatConverter
System.Drawing.PointConverter
System.Drawing.RectangleConverter
System.Drawing.SizeConverter
System.Drawing.SizeFConverter
System.Resources.ResXFileRef.Converter
System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicyTypeConverter
System.ServiceModel.Discovery.Configuration.DiscoveryVersionConverter
System.ServiceModel.XamlIntegration.EndpointIdentityConverter
System.ServiceModel.XamlIntegration.ServiceXNameTypeConverter
System.ServiceModel.XamlIntegration.XPathMessageContextTypeConverter
System.UriTypeConverter
System.Web.Configuration.LowerCaseStringConverter
System.Web.UI.Design.DataBindingCollectionConverter
System.Web.UI.Design.DataColumnSelectionConverter
System.Web.UI.Design.DataFieldConverter
System.Web.UI.Design.DataMemberConverter
System.Web.UI.Design.DataSourceConverter
System.Web.UI.Design.DataSourceViewSchemaConverter
System.Web.UI.Design.ExpressionsCollectionConverter
System.Web.UI.Design.MobileControls.Converters.DataFieldConverter
System.Web.UI.Design.MobileControls.Converters.DataMemberConverter
System.Web.UI.Design.SkinIDTypeConverter
System.Web.UI.Design.WebControls.DataSourceIDConverter
System.Web.UI.WebControls.FontNamesConverter
System.Web.UI.WebControls.FontUnitConverter
System.Web.UI.WebControls.StringArrayConverter
System.Web.UI.WebControls.UnitConverter
System.Windows.Controls.DataGridLengthConverter
System.Windows.Controls.Ribbon.RibbonControlLengthConverter
System.Windows.Controls.Ribbon.StringCollectionConverter
System.Windows.Controls.VirtualizationCacheLengthConverter
System.Windows.CornerRadiusConverter
System.Windows.CultureInfoIetfLanguageTagConverter
System.Windows.DeferrableContentConverter
System.Windows.DialogResultConverter
System.Windows.DurationConverter
System.Windows.DynamicResourceExtensionConverter
System.Windows.ExpressionConverter
System.Windows.FigureLengthConverter
System.Windows.FontSizeConverter
System.Windows.FontStretchConverter
System.Windows.FontStyleConverter
System.Windows.FontWeightConverter
System.Windows.Forms.AxHost.StateConverter
System.Windows.Forms.CursorConverter
System.Windows.Forms.DataGridPreferredColumnWidthTypeConverter
System.Windows.Forms.DataGridViewCellStyleConverter
System.Windows.Forms.KeysConverter
System.Windows.Forms.Layout.TableLayoutSettingsTypeConverter
System.Windows.Forms.LinkArea.LinkAreaConverter
System.Windows.Forms.LinkConverter
System.Windows.Forms.ListBindingConverter
System.Windows.Forms.OpacityConverter
System.Windows.Forms.PaddingConverter
System.Windows.Forms.ScrollableControl.DockPaddingEdgesConverter
System.Windows.Forms.SelectionRangeConverter
System.Windows.Forms.TreeNodeConverter
System.Windows.GridLengthConverter
System.Windows.Input.CommandConverter
System.Windows.Input.CursorConverter
System.Windows.Input.InputScopeConverter
System.Windows.Input.InputScopeNameConverter
System.Windows.Input.KeyConverter
System.Windows.Input.KeyGestureConverter
System.Windows.Input.ModifierKeysConverter
System.Windows.Input.MouseActionConverter
System.Windows.Input.MouseGestureConverter
System.Windows.Int32RectConverter
System.Windows.KeySplineConverter
System.Windows.KeyTimeConverter
System.Windows.LengthConverter
System.Windows.Markup.DependencyPropertyConverter
System.Windows.Markup.EventSetterHandlerConverter
System.Windows.Markup.NameReferenceConverter
System.Windows.Markup.RoutedEventConverter
System.Windows.Markup.SetterTriggerConditionValueConverter
System.Windows.Markup.TemplateKeyConverter
System.Windows.Markup.XmlLanguageConverter
System.Windows.Media.Animation.RepeatBehaviorConverter
System.Windows.Media.BrushConverter
System.Windows.Media.CacheModeConverter
System.Windows.Media.ColorConverter
System.Windows.Media.Converters.BaseIListConverter
System.Windows.Media.DoubleCollectionConverter
System.Windows.Media.FontFamilyConverter
System.Windows.Media.GeometryConverter
System.Windows.Media.ImageSourceConverter
System.Windows.Media.Int32CollectionConverter
System.Windows.Media.MatrixConverter
System.Windows.Media.Media3D.Matrix3DConverter
System.Windows.Media.Media3D.Point3DCollectionConverter
System.Windows.Media.Media3D.Point3DConverter
System.Windows.Media.Media3D.Point4DConverter
System.Windows.Media.Media3D.QuaternionConverter
System.Windows.Media.Media3D.Rect3DConverter
System.Windows.Media.Media3D.Size3DConverter
System.Windows.Media.Media3D.Vector3DCollectionConverter
System.Windows.Media.Media3D.Vector3DConverter
System.Windows.Media.PathFigureCollectionConverter
System.Windows.Media.PixelFormatConverter
System.Windows.Media.PointCollectionConverter
System.Windows.Media.RequestCachePolicyConverter
System.Windows.Media.TransformConverter
System.Windows.Media.VectorCollectionConverter
System.Windows.PointConverter
System.Windows.PropertyPathConverter
System.Windows.RectConverter
System.Windows.SizeConverter
System.Windows.StrokeCollectionConverter
System.Windows.TemplateBindingExpressionConverter
System.Windows.TemplateBindingExtensionConverter
System.Windows.TextDecorationCollectionConverter
System.Windows.ThicknessConverter
System.Windows.VectorConverter
System.Workflow.ComponentModel.Design.ActivityBindTypeConverter
System.Xaml.Schema.XamlTypeTypeConverter

## Exemples

### Exemple : assigning a TypeConverter to a Type

	[TypeConverter(typeof(ColorConverter))]
	public struct Color : IFormattable, IEquatable<Color>

### Exemple : implementing and using a TypeConverter

    public class CrazyClassTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var casted = value as string;
            return casted != null
                ? new CrazyClass(casted.ToCharArray())
                : base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var casted = value as CrazyClass;
            return destinationType == typeof (string) && casted != null
                ? String.Join("", casted.Charray)
                : base.ConvertTo(context, culture, value, destinationType);
        }

    }

    [TypeConverter(typeof(CrazyClassTypeConverter))]
    public class CrazyClass
    {
        public char[] Charray { get; }

        public CrazyClass(char[] charray)
        {
            Charray = charray;
        }

    }

    var crazyClass = new CrazyClass(new [] {'T', 'e', 's', 't'});
    var converter = TypeDescriptor.GetConverter(typeof(CrazyClass));

    // this should provide you the string "Test"        
    var crazyClassToString = converter.ConvertToString(crazyClass); 

    // provides you an instance of CrazyClass with property Charray set to {'W', 'h', 'a', 't' } 
    var stringToCrazyClass = converter.ConvertFrom("What"); 

