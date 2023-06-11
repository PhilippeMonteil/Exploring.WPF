
# [Input](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/input-wpf?view=netframeworkdesktop-4.8)

## En résumé

- interface IInputElement : events keyboard/mouse/stylus/focus, add/remove event handler, raise event ...
- classe Keyboard : Keyboard up/down, focus, event handlers / DependencyObject
- classe FocusManager : logical focus manager : Focus scope ...
- classe KeyboardNavigation : attached properties AcceptsReturn, IsTabStop, TabNavigation ...

- IInputElement

    - events : 
        - Stylus, Mouse, Keyboard
        - Preview : PreviewKeyDown, KeyDown, Focus, MouseCapture ...
    - methods :
        - RoutedEvent : AddHandler, RemoveHandler, RaiseEvent
        - CaptureMouse, Stylus
        - Focus
    - propriétés :
        - IsEnabled, Focusable, IsKeyboardFocusWithin, IsMouse/StylusOver, IsKeyboardFocused ...
    - derived : UIElement, FrameworkElement ...

- Keyboard Class

    - public static class Keyboard

    - events

        - PreviewGotKeyboardFocusEvent / GotKeyboardFocusEvent
        - PreviewLostKeyboardFocusEvent / LostKeyboardFocusEvent
        - KeyDownEvent / PreviewKeyDownEvent
        - ...

    - methods

        public static IInputElement FocusedElement { get; }
        public static void ClearFocus();
        public static IInputElement Focus(IInputElement element);

        public static KeyStates GetKeyStates(Key key);

        public static bool IsKeyDown(Key key);
        public static bool IsKeyToggled(Key key);
        public static bool IsKeyUp(Key key);

        public static void AddGotKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler);
        ...

- FocusManager : FocusedElement, FocusScope, FocusHandler, RoutedEvent Focus

    - public static class FocusManager

    - events

        public static readonly RoutedEvent GotFocusEvent;
        public static readonly RoutedEvent LostFocusEvent;

    - methods

        public static void SetFocusedElement(DependencyObject element, IInputElement value);
        public static IInputElement GetFocusedElement(DependencyObject element);

        public static void SetIsFocusScope(DependencyObject element, bool value);
        public static bool GetIsFocusScope(DependencyObject element);
        public static DependencyObject GetFocusScope(DependencyObject element);

        public static void AddGotFocusHandler(DependencyObject element, RoutedEventHandler handler);
        public static void RemoveGotFocusHandler(DependencyObject element, RoutedEventHandler handler);

        public static void AddLostFocusHandler(DependencyObject element, RoutedEventHandler handler);
        public static void RemoveLostFocusHandler(DependencyObject element, RoutedEventHandler handler);

- KeyboardNavigation
 
    - public sealed class KeyboardNavigation
    - méthodes :
        - set/get attached properties / DependencyObject
             - AcceptsReturn
             - IsTabStop
             - TabIndex
             - TabNavigation
             - ControlTabNavigation
             - DirectionalNavigation

- Styling for Focus in Controls
    - Focus visual styles act exclusively for keyboard focus.
    - Focus adorner
    - FocusVisualStyleKey
    - FrameworkElement.FocusVisualStyle
    - alternative : trigger on IsKeyboardFocused

- Button.IsDefault, .IsCancel
- Mnemonics, AccessText
- Label.Target

## [Focus Managment](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/focus-overview?view=netframeworkdesktop-4.8)

### Keyboard Focus

#### [IInputElement Interface](https://learn.microsoft.com/en-us/dotnet/api/system.windows.iinputelement?view=windowsdesktop-7.0)

- Derived

System.Windows.ContentElement
System.Windows.FrameworkContentElement
System.Windows.FrameworkElement
System.Windows.IFrameworkInputElement
System.Windows.UIElement
System.Windows.UIElement3D

- signature

````
    public interface IInputElement
    {
        bool IsKeyboardFocusWithin { get; }
        bool IsStylusOver { get; }
        bool IsStylusDirectlyOver { get; }
        bool IsStylusCaptured { get; }
        bool IsMouseOver { get; }
        bool Focusable { get; set; }
        bool IsEnabled { get; }
        bool IsKeyboardFocused { get; }
        bool IsMouseDirectlyOver { get; }
        bool IsMouseCaptured { get; }

        event StylusEventHandler PreviewStylusMove;
        event StylusEventHandler PreviewStylusInRange;
        event StylusEventHandler PreviewStylusInAirMove;
        event MouseButtonEventHandler MouseRightButtonUp;
        event StylusDownEventHandler PreviewStylusDown;
        event StylusButtonEventHandler PreviewStylusButtonUp;
        event StylusButtonEventHandler PreviewStylusButtonDown;
        event MouseWheelEventHandler PreviewMouseWheel;
        event MouseButtonEventHandler PreviewMouseRightButtonUp;
        event MouseButtonEventHandler PreviewMouseRightButtonDown;
        event MouseEventHandler PreviewMouseMove;
        event StylusEventHandler PreviewStylusOutOfRange;
        event StylusSystemGestureEventHandler PreviewStylusSystemGesture;
        event StylusDownEventHandler StylusDown;
        event TextCompositionEventHandler PreviewTextInput;
        event StylusButtonEventHandler StylusButtonDown;
        event StylusButtonEventHandler StylusButtonUp;
        event MouseButtonEventHandler PreviewMouseLeftButtonUp;
        event StylusEventHandler StylusEnter;
        event StylusEventHandler StylusInAirMove;
        event StylusEventHandler StylusInRange;
        event StylusEventHandler StylusLeave;
        event StylusEventHandler StylusMove;
        event StylusEventHandler StylusOutOfRange;
        event StylusSystemGestureEventHandler StylusSystemGesture;
        event StylusEventHandler PreviewStylusUp;
        event MouseButtonEventHandler PreviewMouseLeftButtonDown;
        event TextCompositionEventHandler TextInput;
        event KeyEventHandler PreviewKeyUp;
        event KeyboardFocusChangedEventHandler GotKeyboardFocus;
        event MouseEventHandler GotMouseCapture;
        event StylusEventHandler GotStylusCapture;
        event KeyEventHandler KeyDown;
        event KeyEventHandler KeyUp;
        event KeyboardFocusChangedEventHandler PreviewLostKeyboardFocus;
        event MouseEventHandler LostMouseCapture;
        event StylusEventHandler LostStylusCapture;
        event MouseEventHandler MouseEnter;
        event KeyboardFocusChangedEventHandler LostKeyboardFocus;
        event MouseButtonEventHandler MouseLeftButtonDown;
        event MouseButtonEventHandler MouseLeftButtonUp;
        event MouseEventHandler MouseMove;
        event MouseButtonEventHandler MouseRightButtonDown;
        event StylusEventHandler StylusUp;
        event MouseWheelEventHandler MouseWheel;
        event KeyboardFocusChangedEventHandler PreviewGotKeyboardFocus;
        event KeyEventHandler PreviewKeyDown;
        event MouseEventHandler MouseLeave;

        void AddHandler(RoutedEvent routedEvent, Delegate handler);
        bool CaptureMouse();
        bool CaptureStylus();
        bool Focus();
        void RaiseEvent(RoutedEventArgs e);
        void ReleaseMouseCapture();
        void ReleaseStylusCapture();
        void RemoveHandler(RoutedEvent routedEvent, Delegate handler);
    }
````

- méthodes

        void AddHandler(RoutedEvent routedEvent, Delegate handler);
        void RemoveHandler(RoutedEvent routedEvent, Delegate handler);

        bool Focus();

        void RaiseEvent(RoutedEventArgs e);

#### [System.Windows.Input.Keyboard Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.keyboard?view=windowsdesktop-7.0)

- classe

    public static class Keyboard

- méthodes

        public static IInputElement FocusedElement { get; }

            - Gets the element that has keyboard focus.

        public static void ClearFocus();

        public static IInputElement Focus(IInputElement element);

            - Sets keyboard focus on the specified element.
            - returns the element with focus

public static class Keyboard

````
    public static class Keyboard
    {
        public static readonly RoutedEvent GotKeyboardFocusEvent;
        public static readonly RoutedEvent KeyboardInputProviderAcquireFocusEvent;
        public static readonly RoutedEvent KeyDownEvent;
        public static readonly RoutedEvent KeyUpEvent;
        public static readonly RoutedEvent LostKeyboardFocusEvent;
        public static readonly RoutedEvent PreviewGotKeyboardFocusEvent;
        public static readonly RoutedEvent PreviewKeyboardInputProviderAcquireFocusEvent;
        public static readonly RoutedEvent PreviewKeyDownEvent;
        public static readonly RoutedEvent PreviewKeyUpEvent;
        public static readonly RoutedEvent PreviewLostKeyboardFocusEvent;

        public static IInputElement FocusedElement { get; }
        public static RestoreFocusMode DefaultRestoreFocusMode { get; set; }
        public static ModifierKeys Modifiers { get; }
        public static KeyboardDevice PrimaryDevice { get; }

        public static void AddGotKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler);
        public static void AddKeyboardInputProviderAcquireFocusHandler(DependencyObject element, KeyboardInputProviderAcquireFocusEventHandler handler);
        public static void AddKeyDownHandler(DependencyObject element, KeyEventHandler handler);
        public static void AddKeyUpHandler(DependencyObject element, KeyEventHandler handler);
        public static void AddLostKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler);
        public static void AddPreviewGotKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler);
        public static void AddPreviewKeyboardInputProviderAcquireFocusHandler(DependencyObject element, KeyboardInputProviderAcquireFocusEventHandler handler);
        public static void AddPreviewKeyDownHandler(DependencyObject element, KeyEventHandler handler);
        public static void AddPreviewKeyUpHandler(DependencyObject element, KeyEventHandler handler);
        public static void AddPreviewLostKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler);
        public static void ClearFocus();
        public static IInputElement Focus(IInputElement element);
        public static KeyStates GetKeyStates(Key key);
        public static bool IsKeyDown(Key key);
        public static bool IsKeyToggled(Key key);
        public static bool IsKeyUp(Key key);
        public static void RemoveGotKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler);
        public static void RemoveKeyboardInputProviderAcquireFocusHandler(DependencyObject element, KeyboardInputProviderAcquireFocusEventHandler handler);
        public static void RemoveKeyDownHandler(DependencyObject element, KeyEventHandler handler);
        public static void RemoveKeyUpHandler(DependencyObject element, KeyEventHandler handler);
        public static void RemoveLostKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler);
        public static void RemovePreviewGotKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler);
        public static void RemovePreviewKeyboardInputProviderAcquireFocusHandler(DependencyObject element, KeyboardInputProviderAcquireFocusEventHandler handler);
        public static void RemovePreviewKeyDownHandler(DependencyObject element, KeyEventHandler handler);
        public static void RemovePreviewKeyUpHandler(DependencyObject element, KeyEventHandler handler);
        public static void RemovePreviewLostKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler);
    }
````

### [Logical Focus](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/focus-overview?view=netframeworkdesktop-4.8#logical-focus)

#### [FocusManager Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.focusmanager?view=windowsdesktop-7.0)

- Logical Focus Manager

- Provides a set of static methods, attached properties, and events for determining and setting 
  focus scopes and for setting the focused element within the scope.

- Logical focus pertains to the FocusManager.FocusedElement within a specific focus scope.

- A focus scope is a container element that keeps track of the FocusManager.FocusedElement within its scope.
  By default, the Window class is a focus scope as are the Menu, ContextMenu, and ToolBar classes. 
  An element that is a focus scope has IsFocusScope set to true.

- classe

````
    public static class FocusManager
    {
        public static readonly DependencyProperty FocusedElementProperty;
        public static readonly DependencyProperty IsFocusScopeProperty;

        public static readonly RoutedEvent GotFocusEvent;
        public static readonly RoutedEvent LostFocusEvent;

        // Set/Get IsFocusScope
        public static void SetIsFocusScope(DependencyObject element, bool value);
        public static bool GetIsFocusScope(DependencyObject element);

        // Sets logical focus on an element in a scope.
        public static void SetFocusedElement(DependencyObject element, IInputElement value);
        public static IInputElement GetFocusedElement(DependencyObject element);

        // Determines the closest ancestor of the specified element that has IsFocusScope set to true.
        public static DependencyObject GetFocusScope(DependencyObject element);

        public static void AddGotFocusHandler(DependencyObject element, RoutedEventHandler handler);
        public static void RemoveGotFocusHandler(DependencyObject element, RoutedEventHandler handler);

        public static void AddLostFocusHandler(DependencyObject element, RoutedEventHandler handler);
        public static void RemoveLostFocusHandler(DependencyObject element, RoutedEventHandler handler);

    }
 ````

 ### [KeyboardNavigation](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/focus-overview?view=netframeworkdesktop-4.8#keyboard-navigation)

 > The KeyboardNavigation class is responsible for implementing default keyboard focus navigation when one of the navigation keys is pressed. The navigation keys are: TAB, SHIFT+TAB, CTRL+TAB, CTRL+SHIFT+TAB, UPARROW, DOWNARROW, LEFTARROW, and RIGHTARROW keys.

 - [classe](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.keyboardnavigation?redirectedfrom=MSDN&view=windowsdesktop-7.0)
 
     public sealed class KeyboardNavigation

 - attached properties
     - AcceptsReturn
     - IsTabStop
     - TabIndex
     - TabNavigation
     - ControlTabNavigation
     - DirectionalNavigation

- [KeyboardNavigationMode](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.keyboardnavigationmode?view=windowsdesktop-7.0)

 ````
    public sealed class KeyboardNavigation
    {
        public static readonly DependencyProperty AcceptsReturnProperty;
        public static readonly DependencyProperty ControlTabNavigationProperty;
        public static readonly DependencyProperty DirectionalNavigationProperty;
        public static readonly DependencyProperty IsTabStopProperty;
        public static readonly DependencyProperty TabIndexProperty;
        public static readonly DependencyProperty TabNavigationProperty;

        public static void SetAcceptsReturn(DependencyObject element, bool enabled);
        public static void SetControlTabNavigation(DependencyObject element, KeyboardNavigationMode mode);
        public static void SetDirectionalNavigation(DependencyObject element, KeyboardNavigationMode mode);
        public static void SetIsTabStop(DependencyObject element, bool isTabStop);
        public static void SetTabIndex(DependencyObject element, int index);

        public static bool GetAcceptsReturn(DependencyObject element);
        public static KeyboardNavigationMode GetControlTabNavigation(DependencyObject element);
        public static KeyboardNavigationMode GetDirectionalNavigation(DependencyObject element);
        public static bool GetIsTabStop(DependencyObject element);
        public static int GetTabIndex(DependencyObject element);
        public static KeyboardNavigationMode GetTabNavigation(DependencyObject element);
    }
````

## [UIElement.InputBindings](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.inputbindings?view=windowsdesktop-7.0)

public System.Windows.Input.InputBindingCollection InputBindings { get; }

public sealed class InputBindingCollection : System.Collections.IList

### [InputBinding Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.inputbinding?view=windowsdesktop-7.0)

- Inheritance : Object -> DispatcherObject -> DependencyObject -> Freezable -> InputBinding
- Derived : KeyBinding, MouseBinding
- properties
    - public System.Windows.Input.ICommand Command { get; set; }
    - public object CommandParameter { get; set; }
    - public System.Windows.IInputElement CommandTarget { get; set; }
    - public virtual System.Windows.Input.InputGesture Gesture { get; set; }

### InputGesture

    public abstract class InputGesture

### Exemple

````
<Window.InputBindings>
  <KeyBinding Key="B"
              Modifiers="Control" 
              Command="ApplicationCommands.Open" />
</Window.InputBindings>
````

## [Styling for Focus in Controls, and FocusVisualStyle](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/styling-for-focus-in-controls-and-focusvisualstyle?view=netframeworkdesktop-4.8)

- Focus adorner

    > What the feature actually does is to overlay a different visual tree (an adorner) on top of the visual tree as created by a control's rendering through its template. You define this separate visual tree using a style that fills the FocusVisualStyle property.

- FocusVisualStyleKey
 
    > The themes for controls include a default focus visual style behavior that becomes the focus visual style for all controls in the theme. This theme style is identified by the value of the static key FocusVisualStyleKey.

- [FrameworkElement.FocusVisualStyle](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.focusvisualstyle?view=windowsdesktop-7.0)

    > Setting FocusVisualStyle on individual control styles that are not part of a theme is not the intended usage of focus visual styles.

- Focus visual styles act exclusively for keyboard focus. 

- Alternatives to Using a Focus Visual Style

    - trigger on IsKeyboardFocused

## Button.IsDefault, .IsCancel

## Mnemonics

### [Label.Target Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.label.target?view=windowsdesktop-7.0)

    [System.ComponentModel.TypeConverter(typeof(System.Windows.Markup.NameReferenceConverter))]
    public System.Windows.UIElement Target { get; set; }

Gets or sets the element that receives focus when the user presses the label's access key.

### Exemple

````
    <StackPanel Margin="32">
        <StackPanel.Background>
            <SolidColorBrush Color="{DynamicResource {x:Static SystemColors.ActiveCaptionColorKey}}"/>
        </StackPanel.Background>
        <Button x:Name="Button1"
                Click="Button1_Click"
                IsCancel="True">
            <AccessText>_AButton</AccessText>
        </Button>
        <Button Margin="8" Width="200" Height="100"
                Click="Button_Click" 
                IsDefault="True"
                Content="_Button">
        </Button>
        <Label Content="Your _Name" Target="{Binding ElementName=txtName}" Margin="8,0,0,0"  VerticalAlignment="Center" />
        <TextBox x:Name="txtName" Text="TextBox" Margin="8" AcceptsReturn="False"/>
    </StackPanel>
````
