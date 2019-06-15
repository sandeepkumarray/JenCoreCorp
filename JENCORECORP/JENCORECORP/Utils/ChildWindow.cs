// Type: Xceed.Wpf.Toolkit.ChildWindow
// Assembly: WPFToolkit.Extended, Version=1.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4
// MVID: E0BE1B04-5147-44D2-A254-8D8C939A004B
// Assembly location: D:\off mat\All Mail attachments\CDC\CDC\bin\Debug\WPFToolkit.Extended.dll

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JENCORECORP
{
  public class ChildWindow : ContentControl
  {
    private TranslateTransform _moveTransform = new TranslateTransform();
    private Rectangle _modalLayer = new Rectangle();
    private Canvas _modalLayerPanel = new Canvas();
    public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof (object), typeof (ChildWindow), (PropertyMetadata) new UIPropertyMetadata((object) string.Empty));
    public static readonly DependencyProperty CaptionForegroundProperty = DependencyProperty.Register("CaptionForeground", typeof (Brush), typeof (ChildWindow), (PropertyMetadata) new UIPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty CloseButtonStyleProperty = DependencyProperty.Register("CloseButtonStyle", typeof (Style), typeof (ChildWindow), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty CloseButtonVisibilityProperty = DependencyProperty.Register("CloseButtonVisibility", typeof (Visibility), typeof (ChildWindow), new PropertyMetadata((object) Visibility.Visible));
    public static readonly DependencyProperty DesignerWindowStateProperty = DependencyProperty.Register("DesignerWindowState", typeof (WindowState), typeof (ChildWindow), new PropertyMetadata((object) WindowState.Closed, new PropertyChangedCallback(ChildWindow.OnDesignerWindowStatePropertyChanged)));
    public static readonly DependencyProperty FocusedElementProperty = DependencyProperty.Register("FocusedElement", typeof (FrameworkElement), typeof (ChildWindow), (PropertyMetadata) new UIPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty IsModalProperty = DependencyProperty.Register("IsModal", typeof (bool), typeof (ChildWindow), (PropertyMetadata) new UIPropertyMetadata((object) false, new PropertyChangedCallback(ChildWindow.OnIsModalPropertyChanged)));
    public static readonly DependencyProperty LeftProperty = DependencyProperty.Register("Left", typeof (double), typeof (ChildWindow), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(ChildWindow.OnLeftPropertyChanged)));
    public static readonly DependencyProperty OverlayBrushProperty = DependencyProperty.Register("OverlayBrush", typeof (Brush), typeof (ChildWindow), new PropertyMetadata((object) Brushes.Gray, new PropertyChangedCallback(ChildWindow.OnOverlayBrushChanged)));
    public static readonly DependencyProperty OverlayOpacityProperty = DependencyProperty.Register("OverlayOpacity", typeof (double), typeof (ChildWindow), new PropertyMetadata((object) 0.5, new PropertyChangedCallback(ChildWindow.OnOverlayOpacityChanged)));
    public static readonly DependencyProperty TopProperty = DependencyProperty.Register("Top", typeof (double), typeof (ChildWindow), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(ChildWindow.OnTopPropertyChanged)));
    public static readonly DependencyProperty WindowBackgroundProperty = DependencyProperty.Register("WindowBackground", typeof (Brush), typeof (ChildWindow), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty WindowBorderBrushProperty = DependencyProperty.Register("WindowBorderBrush", typeof (Brush), typeof (ChildWindow), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty WindowOpacityProperty = DependencyProperty.Register("WindowOpacity", typeof (double), typeof (ChildWindow), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty WindowStartupLocationProperty = DependencyProperty.Register("WindowStartupLocation", typeof (WindowStartupLocation), typeof (ChildWindow), (PropertyMetadata) new UIPropertyMetadata((object) WindowStartupLocation.Manual, new PropertyChangedCallback(ChildWindow.OnWindowStartupLocationChanged)));
    public static readonly DependencyProperty WindowStateProperty = DependencyProperty.Register("WindowState", typeof (WindowState), typeof (ChildWindow), (PropertyMetadata) new FrameworkPropertyMetadata((object) WindowState.Closed, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(ChildWindow.OnWindowStatePropertyChanged)));
    private const int _horizaontalOffset = 3;
    private const int _verticalOffset = 3;
    private Grid _root;
    private bool _startupPositionInitialized;
    private bool _isMouseCaptured;
    private Point _clickPoint;
    private Point _oldPosition;
    private Border _dragWidget;
    private FrameworkElement _parentContainer;
    private bool _ignorePropertyChanged;
    private bool? _dialogResult;

    internal Grid WindowRoot { get; private set; }

    internal Thumb DragWidget { get; private set; }

    internal Button MinimizeButton { get; private set; }

    internal Button MaximizeButton { get; private set; }

    internal Button CloseButton { get; private set; }

    public object Caption
    {
      get
      {
        return this.GetValue(ChildWindow.CaptionProperty);
      }
      set
      {
        this.SetValue(ChildWindow.CaptionProperty, value);
      }
    }

    public Brush CaptionForeground
    {
      get
      {
        return (Brush) this.GetValue(ChildWindow.CaptionForegroundProperty);
      }
      set
      {
        this.SetValue(ChildWindow.CaptionForegroundProperty, (object) value);
      }
    }

    public Style CloseButtonStyle
    {
      get
      {
        return (Style) this.GetValue(ChildWindow.CloseButtonStyleProperty);
      }
      set
      {
        this.SetValue(ChildWindow.CloseButtonStyleProperty, (object) value);
      }
    }

    public Visibility CloseButtonVisibility
    {
      get
      {
        return (Visibility) this.GetValue(ChildWindow.CloseButtonVisibilityProperty);
      }
      set
      {
        this.SetValue(ChildWindow.CloseButtonVisibilityProperty, (object) value);
      }
    }

    [TypeConverter(typeof (NullableBoolConverter))]
    public bool? DialogResult
    {
      get
      {
        return this._dialogResult;
      }
      set
      {
        bool? nullable1 = this._dialogResult;
        bool? nullable2 = value;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) == 0)
          return;
        this._dialogResult = value;
        this.Close();
      }
    }

    public WindowState DesignerWindowState
    {
      get
      {
        return (WindowState) this.GetValue(ChildWindow.DesignerWindowStateProperty);
      }
      set
      {
        this.SetValue(ChildWindow.DesignerWindowStateProperty, (object) value);
      }
    }

    public FrameworkElement FocusedElement
    {
      get
      {
        return (FrameworkElement) this.GetValue(ChildWindow.FocusedElementProperty);
      }
      set
      {
        this.SetValue(ChildWindow.FocusedElementProperty, (object) value);
      }
    }

    public bool IsModal
    {
      get
      {
        return (bool) this.GetValue(ChildWindow.IsModalProperty);
      }
      set
      {
        this.SetValue(ChildWindow.IsModalProperty, (object) (bool) (value ? true : false));
      }
    }

    public double Left
    {
      get
      {
        return (double) this.GetValue(ChildWindow.LeftProperty);
      }
      set
      {
        this.SetValue(ChildWindow.LeftProperty, (object) value);
      }
    }

    public Brush OverlayBrush
    {
      get
      {
        return (Brush) this.GetValue(ChildWindow.OverlayBrushProperty);
      }
      set
      {
        this.SetValue(ChildWindow.OverlayBrushProperty, (object) value);
      }
    }

    public double OverlayOpacity
    {
      get
      {
        return (double) this.GetValue(ChildWindow.OverlayOpacityProperty);
      }
      set
      {
        this.SetValue(ChildWindow.OverlayOpacityProperty, (object) value);
      }
    }

    public double Top
    {
      get
      {
        return (double) this.GetValue(ChildWindow.TopProperty);
      }
      set
      {
        this.SetValue(ChildWindow.TopProperty, (object) value);
      }
    }

    public Brush WindowBackground
    {
      get
      {
        return (Brush) this.GetValue(ChildWindow.WindowBackgroundProperty);
      }
      set
      {
        this.SetValue(ChildWindow.WindowBackgroundProperty, (object) value);
      }
    }

    public Brush WindowBorderBrush
    {
      get
      {
        return (Brush) this.GetValue(ChildWindow.WindowBorderBrushProperty);
      }
      set
      {
        this.SetValue(ChildWindow.WindowBorderBrushProperty, (object) value);
      }
    }

    public double WindowOpacity
    {
      get
      {
        return (double) this.GetValue(ChildWindow.WindowOpacityProperty);
      }
      set
      {
        this.SetValue(ChildWindow.WindowOpacityProperty, (object) value);
      }
    }

    public WindowStartupLocation WindowStartupLocation
    {
      get
      {
        return (WindowStartupLocation) this.GetValue(ChildWindow.WindowStartupLocationProperty);
      }
      set
      {
        this.SetValue(ChildWindow.WindowStartupLocationProperty, (object) value);
      }
    }

    public WindowState WindowState
    {
      get
      {
        return (WindowState) this.GetValue(ChildWindow.WindowStateProperty);
      }
      set
      {
        this.SetValue(ChildWindow.WindowStateProperty, (object) value);
      }
    }

    public event EventHandler Closed;

    public event EventHandler<CancelEventArgs> Closing;

    static ChildWindow()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (ChildWindow), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (ChildWindow)));
    }

    public ChildWindow()
    {
      this.DesignerWindowState = WindowState.Open;
      this.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.ChildWindow_IsVisibleChanged);
      this._modalLayer.Fill = this.OverlayBrush;
      this._modalLayer.Opacity = this.OverlayOpacity;
    }

    private static void OnDesignerWindowStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ChildWindow childWindow = d as ChildWindow;
      if (childWindow == null)
        return;
      childWindow.OnDesignerWindowStatePropertyChanged((WindowState) e.OldValue, (WindowState) e.NewValue);
    }

    protected virtual void OnDesignerWindowStatePropertyChanged(WindowState oldValue, WindowState newValue)
    {
      if (!DesignerProperties.GetIsInDesignMode((DependencyObject) this))
        return;
      this.Visibility = newValue == WindowState.Open ? Visibility.Visible : Visibility.Collapsed;
    }

    private static void OnIsModalPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ChildWindow childWindow = d as ChildWindow;
      if (childWindow == null)
        return;
      childWindow.OnIsModalChanged((bool) e.OldValue, (bool) e.NewValue);
    }

    private void OnIsModalChanged(bool oldValue, bool newValue)
    {
      if (newValue)
      {
        KeyboardNavigation.SetTabNavigation((DependencyObject) this, KeyboardNavigationMode.Cycle);
        this.ShowModalLayer();
      }
      else
      {
        KeyboardNavigation.SetTabNavigation((DependencyObject) this, KeyboardNavigationMode.Continue);
        this.HideModalLayer();
      }
    }

    private static void OnLeftPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      ChildWindow childWindow = obj as ChildWindow;
      if (childWindow == null)
        return;
      childWindow.OnLeftPropertyChanged((double) e.OldValue, (double) e.NewValue);
    }

    private void OnLeftPropertyChanged(double oldValue, double newValue)
    {
      this.Left = this.GetRestrictedLeft();
      this.ProcessMove(newValue - oldValue, 0.0);
    }

    private static void OnOverlayBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ChildWindow childWindow = d as ChildWindow;
      if (childWindow == null)
        return;
      childWindow.OnOverlayBrushChanged((Brush) e.OldValue, (Brush) e.NewValue);
    }

    protected virtual void OnOverlayBrushChanged(Brush oldValue, Brush newValue)
    {
      this._modalLayer.Fill = newValue;
    }

    private static void OnOverlayOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ChildWindow childWindow = d as ChildWindow;
      if (childWindow == null)
        return;
      childWindow.OnOverlayOpacityChanged((double) e.OldValue, (double) e.NewValue);
    }

    protected virtual void OnOverlayOpacityChanged(double oldValue, double newValue)
    {
      this._modalLayer.Opacity = newValue;
    }

    private static void OnTopPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      ChildWindow childWindow = obj as ChildWindow;
      if (childWindow == null)
        return;
      childWindow.OnTopPropertyChanged((double) e.OldValue, (double) e.NewValue);
    }

    private void OnTopPropertyChanged(double oldValue, double newValue)
    {
      this.Top = this.GetRestrictedTop();
      this.ProcessMove(0.0, newValue - oldValue);
    }

    private static void OnWindowStartupLocationChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      ChildWindow childWindow = o as ChildWindow;
      if (childWindow == null)
        return;
      childWindow.OnWindowStartupLocationChanged((WindowStartupLocation) e.OldValue, (WindowStartupLocation) e.NewValue);
    }

    protected virtual void OnWindowStartupLocationChanged(WindowStartupLocation oldValue, WindowStartupLocation newValue)
    {
    }

    private static void OnWindowStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ChildWindow childWindow = d as ChildWindow;
      if (childWindow == null)
        return;
      childWindow.OnWindowStatePropertyChanged((WindowState) e.OldValue, (WindowState) e.NewValue);
    }

    protected virtual void OnWindowStatePropertyChanged(WindowState oldValue, WindowState newValue)
    {
      if (!DesignerProperties.GetIsInDesignMode((DependencyObject) this))
      {
        if (this._ignorePropertyChanged)
          return;
        this.SetWindowState(newValue);
      }
      else
        this.Visibility = this.DesignerWindowState == WindowState.Open ? Visibility.Visible : Visibility.Collapsed;
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      if (this._dragWidget != null)
      {
        this._dragWidget.RemoveHandler(UIElement.MouseLeftButtonDownEvent, (Delegate) new MouseButtonEventHandler(this.HeaderLeftMouseButtonDown));
        this._dragWidget.RemoveHandler(UIElement.MouseLeftButtonUpEvent, (Delegate) new MouseButtonEventHandler(this.HeaderMouseLeftButtonUp));
        this._dragWidget.MouseMove -= (MouseEventHandler) ((o, e) => this.HeaderMouseMove(e));
      }
      this._dragWidget = this.GetTemplateChild("PART_DragWidget") as Border;
      if (this._dragWidget != null)
      {
        this._dragWidget.AddHandler(UIElement.MouseLeftButtonDownEvent, (Delegate) new MouseButtonEventHandler(this.HeaderLeftMouseButtonDown), true);
        this._dragWidget.AddHandler(UIElement.MouseLeftButtonUpEvent, (Delegate) new MouseButtonEventHandler(this.HeaderMouseLeftButtonUp), true);
        this._dragWidget.MouseMove += (MouseEventHandler) ((o, e) => this.HeaderMouseMove(e));
      }
      if (this.CloseButton != null)
        this.CloseButton.Click -= (RoutedEventHandler) ((o, e) => this.Close());
      this.CloseButton = this.GetTemplateChild("PART_CloseButton") as Button;
      if (this.CloseButton != null)
        this.CloseButton.Click += (RoutedEventHandler) ((o, e) => this.Close());
      this.WindowRoot = this.GetTemplateChild("PART_WindowRoot") as Grid;
      this.WindowRoot.RenderTransform = (Transform) this._moveTransform;
      this._parentContainer = VisualTreeHelper.GetParent((DependencyObject) this) as FrameworkElement;
      this._parentContainer.LayoutUpdated += new EventHandler(this.ParentContainer_LayoutUpdated);
      this._parentContainer.SizeChanged += new SizeChangedEventHandler(this.ParentContainer_SizeChanged);
      this._modalLayer.Height = this._parentContainer.ActualHeight;
      this._modalLayer.Width = this._parentContainer.ActualWidth;
      if (BrowserInteropHelper.IsBrowserHosted)
        this._parentContainer.Loaded += (RoutedEventHandler) ((o, e) => this.ExecuteOpen());
      this._root = this.GetTemplateChild("Root") as Grid;
      this.FocusVisualStyle = (Style) null;
      this._root.Children.Add((UIElement) this._modalLayerPanel);
    }

    protected override void OnGotFocus(RoutedEventArgs e)
    {
        this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, (Action)(() =>
        {
            if (this.FocusedElement == null)
                return;
            this.FocusedElement.Focus();
        }));
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (this.WindowState != WindowState.Open)
        return;
      switch (e.Key)
      {
        case Key.Left:
          this.Left -= 3.0;
          e.Handled = true;
          break;
        case Key.Up:
          this.Top -= 3.0;
          e.Handled = true;
          break;
        case Key.Right:
          this.Left += 3.0;
          e.Handled = true;
          break;
        case Key.Down:
          this.Top += 3.0;
          e.Handled = true;
          break;
      }
    }

    private void ChildWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      if (!(bool) e.NewValue)
        return;
      this.Focus();
    }

    private void HeaderLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
    {
      e.Handled = true;
      this.Focus();
      this._dragWidget.CaptureMouse();
      this._isMouseCaptured = true;
      this._clickPoint = e.GetPosition((IInputElement) null);
      this._oldPosition = new Point(this.Left, this.Top);
    }

    private void HeaderMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      e.Handled = true;
      this._dragWidget.ReleaseMouseCapture();
      this._isMouseCaptured = false;
    }

    private void HeaderMouseMove(MouseEventArgs e)
    {
      if (!this._isMouseCaptured || this.Visibility != Visibility.Visible)
        return;
      Point position1 = e.GetPosition((IInputElement) null);
      this.Left = this._oldPosition.X + (position1.X - this._clickPoint.X);
      this.Top = this._oldPosition.Y + (position1.Y - this._clickPoint.Y);
      Point position2 = e.GetPosition((IInputElement) this._dragWidget);
      if (position2.X < 0.0 || position2.X > this._dragWidget.ActualWidth || (position2.Y < 0.0 || position2.Y > this._dragWidget.ActualHeight))
        return;
      this._oldPosition = new Point(this.Left, this.Top);
      this._clickPoint = e.GetPosition((IInputElement) Window.GetWindow((DependencyObject) this));
    }

    private void ParentContainer_LayoutUpdated(object sender, EventArgs e)
    {
      if (DesignerProperties.GetIsInDesignMode((DependencyObject) this) || this._startupPositionInitialized)
        return;
      this.ExecuteOpen();
      this._startupPositionInitialized = true;
    }

    private void ParentContainer_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      this._modalLayer.Height = e.NewSize.Height;
      this._modalLayer.Width = e.NewSize.Width;
      this.Left = this.GetRestrictedLeft();
      this.Top = this.GetRestrictedTop();
    }

    private double GetRestrictedLeft()
    {
      if (this.Left < 0.0)
        return 0.0;
      if (this._parentContainer == null || this.Left + this.WindowRoot.ActualWidth <= this._parentContainer.ActualWidth || this._parentContainer.ActualWidth == 0.0)
        return this.Left;
      double num = this._parentContainer.ActualWidth - this.WindowRoot.ActualWidth;
      if (num >= 0.0)
        return num;
      else
        return 0.0;
    }

    private double GetRestrictedTop()
    {
      if (this.Top < 0.0)
        return 0.0;
      if (this._parentContainer == null || this.Top + this.WindowRoot.ActualHeight <= this._parentContainer.ActualHeight || this._parentContainer.ActualHeight == 0.0)
        return this.Top;
      double num = this._parentContainer.ActualHeight - this.WindowRoot.ActualHeight;
      if (num >= 0.0)
        return num;
      else
        return 0.0;
    }

    private void SetWindowState(WindowState state)
    {
      switch (state)
      {
        case WindowState.Closed:
          this.ExecuteClose();
          break;
        case WindowState.Open:
          this.ExecuteOpen();
          break;
      }
    }

    private void ExecuteClose()
    {
      CancelEventArgs e = new CancelEventArgs();
      this.OnClosing(e);
      if (!e.Cancel)
      {
        if (!this._dialogResult.HasValue)
          this._dialogResult = new bool?(false);
        this.OnClosed(EventArgs.Empty);
      }
      else
        this.CancelClose();
    }

    private void CancelClose()
    {
      this._dialogResult = new bool?();
      this._ignorePropertyChanged = true;
      this.WindowState = WindowState.Open;
      this._ignorePropertyChanged = false;
    }

    private void ExecuteOpen()
    {
      this._dialogResult = new bool?();
      if (this.WindowStartupLocation == WindowStartupLocation.Center)
        this.CenterChildWindow();
      this.BringToFront();
    }

    private void BringToFront()
    {
      int num1 = 0;
      if (this._parentContainer != null)
        num1 = (int) this._parentContainer.GetValue(Panel.ZIndexProperty);
      int num2;
      this.SetValue(Panel.ZIndexProperty, (object) (num2 = num1 + 1));
      if (!this.IsModal)
        return;
      Panel.SetZIndex((UIElement) this._modalLayerPanel, num2 - 2);
    }

    private void CenterChildWindow()
    {
      if (this._parentContainer == null)
        return;
      this.Left = (this._parentContainer.ActualWidth - this.WindowRoot.ActualWidth) / 2.0;
      this.Top = (this._parentContainer.ActualHeight - this.WindowRoot.ActualHeight) / 2.0;
    }

    private void ShowModalLayer()
    {
      if (DesignerProperties.GetIsInDesignMode((DependencyObject) this))
        return;
      if (!this._modalLayerPanel.Children.Contains((UIElement) this._modalLayer))
        this._modalLayerPanel.Children.Add((UIElement) this._modalLayer);
      this._modalLayer.Visibility = Visibility.Visible;
    }

    private void HideModalLayer()
    {
      this._modalLayer.Visibility = Visibility.Collapsed;
    }

    private void ProcessMove(double x, double y)
    {
      this._moveTransform.X += x;
      this._moveTransform.Y += y;
      this.InvalidateArrange();
    }

    public void Show()
    {
      this.WindowState = WindowState.Open;
    }

    public void Close()
    {
      this.WindowState = WindowState.Closed;
    }

    protected virtual void OnClosed(EventArgs e)
    {
      if (this.Closed == null)
        return;
      this.Closed((object) this, e);
    }

    protected virtual void OnClosing(CancelEventArgs e)
    {
      if (this.Closing == null)
        return;
      this.Closing((object) this, e);
    }
  }
}
