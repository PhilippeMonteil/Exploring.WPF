
# .Net Core MSBuild Project files

## Key elements in an MSBuild project file

[Understanding the Project File](https://docs.microsoft.com/en-us/aspnet/web-forms/overview/deployment/web-deployment-in-the-enterprise/understanding-the-project-file)

![key elements in an MSBuild project file](https://docs.microsoft.com/en-us/aspnet/web-forms/overview/deployment/web-deployment-in-the-enterprise/understanding-the-project-file/_static/image2.png)

## The Project Element

The Project element is the root element of every project file.

In addition to identifying the XML schema for the project file,
the Project element can include attributes to specify the entry points for the build process.

Exemple:

    <Project ToolsVersion="4.0" DefaultTargets="FullPublish" 
        xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
    </Project>

## Properties and Conditions

### Properties : PropertyGroup

    <PropertyGroup>    
        <ServerName>FABRIKAM\TEST1</ServerName>
        <ConnectionString>
            Data Source=FABRIKAM\TESTDB;InitialCatalog=ContactManager,...
        </ConnectionString>
    </PropertyGroup>

To retrieve a property value, use the format $(PropertyName). 

#### Other sources of information

- command-line parameter : 

    msbuild.exe Publish.proj /p:ServerName=FABRIKAM\TESTWEB1

  [MSBuild Command-Line Reference](https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2015/msbuild/msbuild-command-line-reference?view=vs-2015&redirectedfrom=MSDN)

- [Common macros for MSBuild commands and properties](https://docs.microsoft.com/en-us/cpp/build/reference/common-macros-for-build-commands-and-properties?redirectedfrom=MSDN&view=msvc-170)

- [Common MSBuild Project Properties](https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2015/msbuild/common-msbuild-project-properties?view=vs-2015&redirectedfrom=MSDN)

- [MSBuild Reserved and Well-Known Properties](https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2015/msbuild/msbuild-reserved-and-well-known-properties?view=vs-2015&redirectedfrom=MSDN)

### [MSBuild Conditions](https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2015/msbuild/msbuild-conditions?view=vs-2015&redirectedfrom=MSDN)

Most MSBuild elements support the Condition attribute, which lets you specify the criteria upon which MSBuild should evaluate the element. 

    <PropertyGroup>
        <OutputRoot Condition=" '$(OutputRoot)'=='' ">..\Publish\Out\</OutputRoot>
    </PropertyGroup>

## Items and Item Groups

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

### Item Metadata

Item elements can also include ItemMetadata child elements. 

    <Compile Include="Global.asax.cs">
    <DependentUpon>Global.asax</DependentUpon>
    </Compile>

[MSBuild Well-known Item Metadata](https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2015/msbuild/msbuild-well-known-item-metadata?view=vs-2015&redirectedfrom=MSDN)
%(FullPath), %(RootDir), ...

You can create ItemGroup elements within the root-level Project element or within specific Target elements.
ItemGroup elements also support Condition attributes

## Targets and Tasks

### Tasks

In the MSBuild schema, a Task element represents an individual build instruction (or task). 
MSBuild includes a multitude of predefined tasks. 

For example:
- The __Copy__ task copies files to a new location.
- The __Csc__ task invokes the Visual C# compiler.
- The __Exec__ task runs a specified program.
- The __Message__ task writes a message to a logger.

#### [MSBuild Task Reference](https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2015/msbuild/msbuild-task-reference?view=vs-2015&redirectedfrom=MSDN)

### Targets

Tasks must always be contained within Target elements
A Target element is a set of one or more tasks that are executed sequentially.

    <Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
        <Target Name="LogMessage">
            <Message Text="Hello world!" />
        </Target>
    </Project>

You can invoke the target from the command line, by using the /t switch to specify the target.

    msbuild.exe Publish.proj /t:LogMessage

Alternatively, you can add a __DefaultTargets__ attribute to the __Project__ element, 
to specify the targets that you want to invoke.

    <Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003" 
            DefaultTargets="FullPublish">
        <Target Name="LogMessage">
            <Message Text="Hello world!" />
        </Target>
    </Project>

### Conditions

Both targets and tasks can include Condition attributes. 

### Properties and Items

Generally speaking, when you create useful tasks and targets, you'll need to refer to the properties and items that you've defined elsewhere in the project file:

- To use a property value, type $(PropertyName), where PropertyName is the name of the Property element or the name of the parameter.
- To use an item, type @(ItemName), where ItemName is the name of the Item element.

### Exemple

    <Target Name="BuildProjects" Condition=" '$(BuildingInTeamBuild)'!='true' ">
        <MSBuild Projects="@(ProjectsToBuild)"           
            Properties="OutDir=$(OutputRoot);
                        Configuration=$(Configuration);
                        DeployOnBuild=true;
                        DeployTarget=Package"
            Targets="Build" />
    </Target>

## Splitting Project Files to Support Multiple Environments

MSBuild lets you split your build configuration across multiple project files.

    <Import Project="$(TargetEnvPropsFile)"/>

    msbuild.exe Publish.proj /p:TargetEnvPropsFile=EnvConfig\Env-Dev.proj

## [.NET project SDKs](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/overview)

.NET Core and .NET 5 and later projects are associated with a software development kit (SDK). 
Each project SDK is a set of MSBuild targets and associated tasks that are responsible for compiling, 
packing, and publishing code. 

A project that references a project SDK is sometimes referred to as an SDK-style project.


| ID | Description | Repo |
|-|-|-|
| Microsoft.NET.Sdk | The .NET SDK |https://github.com/dotnet/sdk
| Microsoft.NET.Sdk.WindowsDesktop | The .NET Desktop SDK, which includes Windows Forms (WinForms) and Windows Presentation Foundation (WPF). |https://github.com/dotnet/winforms and https://github.com/dotnet/wpf


## [Common MSBuild project items](https://docs.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-items?view=vs-2022#compile)

## [EmbeddedResource](https://docs.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-items?view=vs-2022#embeddedresource)

## [MSBuild reference for .NET Desktop SDK projects](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props-desktop#wpf-default-includes-and-excludes)

## [Default includes and excludes](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/overview#default-includes-and-excludes)
## [WPF default includes and excludes](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props-desktop#wpf-default-includes-and-excludes)

## Book [Inside the Microsoft® Build Engine: Using MSBuild and Team Foundation Build](https://www.amazon.com/Inside-Microsoft%C2%AE-Build-Engine-Foundation/dp/0735626286)
