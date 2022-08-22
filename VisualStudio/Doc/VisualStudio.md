# VisualStudio

## 1) Project files, MSBuild ...

### 1.1) [Understanding the Project File](https://docs.microsoft.com/en-us/aspnet/web-forms/overview/deployment/web-deployment-in-the-enterprise/understanding-the-project-file)

#### key elements in an MSBuild project file
![key elements in an MSBuild project file](https://docs.microsoft.com/en-us/aspnet/web-forms/overview/deployment/web-deployment-in-the-enterprise/understanding-the-project-file/_static/image2.png)

#### The Project Element

The Project element is the root element of every project file.

In addition to identifying the XML schema for the project file,
the Project element can include attributes to specify the entry points for the build process.

Exemple:

    <Project ToolsVersion="4.0" DefaultTargets="FullPublish" 
        xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
    </Project>

#### Properties and Conditions

##### Properties : PropertyGroup

    <PropertyGroup>    
        <ServerName>FABRIKAM\TEST1</ServerName>
        <ConnectionString>
            Data Source=FABRIKAM\TESTDB;InitialCatalog=ContactManager,...
        </ConnectionString>
    </PropertyGroup>

To retrieve a property value, use the format $(PropertyName). 

##### Other sources of information

- command-line parameter : 

    msbuild.exe Publish.proj /p:ServerName=FABRIKAM\TESTWEB1

  [MSBuild Command-Line Reference](https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2015/msbuild/msbuild-command-line-reference?view=vs-2015&redirectedfrom=MSDN)

- [Common macros for MSBuild commands and properties](https://docs.microsoft.com/en-us/cpp/build/reference/common-macros-for-build-commands-and-properties?redirectedfrom=MSDN&view=msvc-170)

- [Common MSBuild Project Properties](https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2015/msbuild/common-msbuild-project-properties?view=vs-2015&redirectedfrom=MSDN)

- [MSBuild Reserved and Well-Known Properties](https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2015/msbuild/msbuild-reserved-and-well-known-properties?view=vs-2015&redirectedfrom=MSDN)

##### [MSBuild Conditions](https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2015/msbuild/msbuild-conditions?view=vs-2015&redirectedfrom=MSDN)

Most MSBuild elements support the Condition attribute, which lets you specify the criteria upon which MSBuild should evaluate the element. 

    <PropertyGroup>
        <OutputRoot Condition=" '$(OutputRoot)'=='' ">..\Publish\Out\</OutputRoot>
    </PropertyGroup>

#### Items and Item Groups

One of the important roles of the project file is to define the inputs to the build process. 
In the MSBuild project schema, these inputs are represented by Item elements.
In a project file, items must be defined within an ItemGroup element.
You must specify an Include attribute to identify the file or wildcard that the item represents.

    <ItemGroup>
        <ProjectsToBuild Include="$(SourceRoot)ContactManager-WCF.sln"/>
    </ItemGroup>

    // 'Reference' : list of files 
    <ItemGroup>
        <Reference Include="Microsoft.CSharp" />
        <Reference Include="System.Runtime.Serialization" />
        <Reference Include="System.ServiceModel" />
        ...
    </ItemGroup>

##### Item Metadata

Item elements can also include ItemMetadata child elements. 

    <Compile Include="Global.asax.cs">
    <DependentUpon>Global.asax</DependentUpon>
    </Compile>


### 1.2) [.NET project SDKs](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/overview)

### 1.3) [Common MSBuild project items](https://docs.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-items?view=vs-2022#compile)

### [EmbeddedResource](https://docs.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-items?view=vs-2022#embeddedresource)

### [MSBuild reference for .NET Desktop SDK projects](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props-desktop#wpf-default-includes-and-excludes)

### [Default includes and excludes](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/overview#default-includes-and-excludes)
### [WPF default includes and excludes](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props-desktop#wpf-default-includes-and-excludes)

## Tricks

### Scope To This
### Pack
### Sync Namespaces
