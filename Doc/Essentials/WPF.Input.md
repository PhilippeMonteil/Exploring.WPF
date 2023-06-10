
# [Input](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/input-wpf?view=netframeworkdesktop-4.8)

## En résumé

- IInputElement

    - derived : UIElement, FrameworkElement ...
    - events : 
        - Stylus, Mouse, Keyboard
        - Preview : PreviewKeyDown, KeyDown ...
    - methods :
        - RoutedEvent : AddHandler, RemoveHandler, RaiseEvent
        - Focus
    - propriétés :
        - Focusable, IsKeyboardFocusWithin, IsMouseOver, IsKeyboardFocused ...

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

### Logical Focus

#### [FocusManager Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.focusmanager?view=windowsdesktop-7.0)

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

        public static IInputElement GetFocusedElement(DependencyObject element);
        public static DependencyObject GetFocusScope(DependencyObject element);
        public static bool GetIsFocusScope(DependencyObject element);

        public static void SetFocusedElement(DependencyObject element, IInputElement value);
        public static void SetIsFocusScope(DependencyObject element, bool value);

        public static void AddGotFocusHandler(DependencyObject element, RoutedEventHandler handler);
        public static void RemoveGotFocusHandler(DependencyObject element, RoutedEventHandler handler);

        public static void AddLostFocusHandler(DependencyObject element, RoutedEventHandler handler);
        public static void RemoveLostFocusHandler(DependencyObject element, RoutedEventHandler handler);

    }
 ````

 ### [KeyboardNavigation Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.focusmanager.gotfocus?view=windowsdesktop-7.0)

 - classe

 - properties
     - AcceptsReturn
     - TabNavigation
     - ControlTabNavigation
     - DirectionalNavigation
     - IsTabStop
     - TabIndex

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
        public static void SetTabNavigation(DependencyObject element, KeyboardNavigationMode mode);

        public static bool GetAcceptsReturn(DependencyObject element);
        public static KeyboardNavigationMode GetControlTabNavigation(DependencyObject element);
        public static KeyboardNavigationMode GetDirectionalNavigation(DependencyObject element);
        public static bool GetIsTabStop(DependencyObject element);
        public static int GetTabIndex(DependencyObject element);

        public static KeyboardNavigationMode GetTabNavigation(DependencyObject element);
    }
````

