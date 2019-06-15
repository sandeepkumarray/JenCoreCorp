using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Diagnostics;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for HostParent.xaml
    /// </summary>
    public partial class HostParent : UserControlClass
    {
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct HWND__
        {
            /// int
            public int unused;
        }
        public HostParent()
        {
            InitializeComponent();
            this.SizeChanged += new SizeChangedEventHandler(OnSizeChanged);
            this.Loaded += new RoutedEventHandler(OnVisibleChanged);
            this.SizeChanged += new SizeChangedEventHandler(OnResize);
        }
        ~HostParent()
        {
            this.Dispose();
        }
        /// <summary>
        /// Track if the application has been created
        /// </summary>
        private bool _iscreated = false;
        /// <summary>
        /// Track if the control is disposed
        /// </summary>
        private bool _isdisposed = false;
        /// <summary>
        /// Handle to the application Window
        /// </summary>
        IntPtr _appWin;
        private Process _childp;
        /// <summary>
        /// The name of the exe to launch
        /// </summary>
        private string exeName = "";
        public string ExeName
        {
            get
            {
                return exeName;
            }
            set
            {
                exeName = value;
            }
        }
        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true,
   CharSet = CharSet.Unicode, ExactSpelling = true,
  CallingConvention = CallingConvention.StdCall)]
        private static extern long GetWindowThreadProcessId(long hWnd, long lpdwProcessId);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
        private static extern long GetWindowLong(IntPtr hwnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true)]
        public static extern int SetWindowLongA([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);
        private const int SWP_NOOWNERZORDER = 0x200;
        private const int SWP_NOREDRAW = 0x8;
        private const int SWP_NOZORDER = 0x4;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int WS_EX_MDICHILD = 0x40;
        private const int SWP_FRAMECHANGED = 0x20;
        private const int SWP_NOACTIVATE = 0x10;
        private const int SWP_ASYNCWINDOWPOS = 0x4000;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOSIZE = 0x1;
        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        private const int WS_CHILD = 0x40000000;
        /// <summary>
        /// Force redraw of control when size changes
        /// </summary>
        /// <param name="e">Not used</param>
        protected void OnSizeChanged(object s, SizeChangedEventArgs e)
        {
            this.InvalidateVisual();
        }
        /// <summary>
        /// Create control when visibility changes
        /// </summary>
        /// <param name="e">Not used</param>
        protected void OnVisibleChanged(object s, RoutedEventArgs e)
        {
            // If control needs to be initialized/created
            if (_iscreated == false)
            {
                // Mark that control is created
                _iscreated = true;
                // Initialize handle value to invalid
                _appWin = IntPtr.Zero;
                try
                {
                    //string path = @"C:/MyOwn/OOPConceptsSamples/OOPConceptsSamples/bin/Debug/" + this.exeName;
                    string path = this.exeName;
                    var procInfo = new System.Diagnostics.ProcessStartInfo(path);
                    procInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(path);
                   // procInfo.WorkingDirectory = @"C:/MyOwn/OOPConceptsSamples/OOPConceptsSamples/bin/Debug";
                    procInfo.UseShellExecute = false;   
                    // Start the process
                    //_childp = System.Diagnostics.Process.Start(@"C:/MyOwn/OOPConceptsSamples/OOPConceptsSamples/bin/Debug/" + this.exeName);
                    _childp = System.Diagnostics.Process.Start(procInfo);
                    // Wait for process to be created and enter idle condition
                    _childp.WaitForInputIdle();
                    // Get the main handle
                    _appWin = _childp.MainWindowHandle;                    
                }
                catch (Exception ex)
                {
                    //Debug.Print(ex.Message + "Error");
                }
                // Put it into this form
                var helper = new WindowInteropHelper(Window.GetWindow(this.AppContainer));
                SetParent(_appWin, helper.Handle);
                // Remove border and whatnot
                //SetWindowLongA(_appWin, GWL_STYLE, WS_VISIBLE);
                // Move the window to overlay it on this window
                //MoveWindow(_appWin, 0, 0, (int)this.ActualWidth, (int)this.ActualHeight, true);
            }
        }
        /// <summary>
        /// Update display of the executable
        /// </summary>
        /// <param name="e">Not used</param>
        protected void OnResize(object s, SizeChangedEventArgs e)
        {
            if (this._appWin != IntPtr.Zero)
            {
                MoveWindow(_appWin, 0, 0, (int)this.ActualWidth, (int)this.ActualHeight, true);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_isdisposed)
            {
                if (disposing)
                {
                    if (_iscreated && _appWin != IntPtr.Zero && !_childp.HasExited)
                    {
                        // Stop the application
                        _childp.Kill();
                        // Clear internal handle
                        _appWin = IntPtr.Zero;
                    }
                }
                _isdisposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
//        public HostParent()
//        {
//            InitializeComponent();
//        }
//        [DllImport("USER32.DLL")]
//        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

//        //Sets a window to be a child window of another window
//        [DllImport("USER32.DLL")]
//        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

//        //Sets window attributes
//        [DllImport("USER32.DLL")]
//        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

//        //Gets window attributes
//        [DllImport("USER32.DLL")]
//        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

//        //assorted constants needed
//        public static int GWL_STYLE = -16;
//        public static int WS_CHILD = 0x40000000; //child window
//        public static int WS_BORDER = 0x00800000; //window with border
//        public static int WS_DLGFRAME = 0x00400000; //window with double border but no title
//        public static int WS_CAPTION = WS_BORDER | WS_DLGFRAME; //window with a title bar

//        /*
//         This function sets the parent of the window with class
//         ClassClass to the form/control the method is in.
//        */
//        public void Reparent()
//        {
//            //get handle of parent form (.net property)
//            IntPtr par = new WindowInteropHelper(this).Handle; ;
//            //get handle of child form (win32)
//            IntPtr child = FindWindow("ClassClass", null);
//            //set parent of child form
//            SetParent(child, par);
//            //get current window style of child form
//            int style = GetWindowLong(child, GWL_STYLE);

//            //take current window style and remove WS_CAPTION from it
//            SetWindowLong(child, GWL_STYLE, (style & ~WS_CAPTION));
//        }
//    }
}
