using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BioImager
{
    public class Win32
    {
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        /// It returns true if the key is pressed, and false if it isn't
        /// 
        /// @param vKey The key you want to check.
        /// 
        /// @return The return value is a short integer with the high order bit set if the key is down
        /// and the low order bit set if the key was pressed after the previous call to
        /// GetAsyncKeyState.
        public static bool GetKeyState(System.Windows.Forms.Keys vKey)
        {
            int si = (int)GetAsyncKeyState(vKey);
            if (si == 0)
                return false;
            else
                return true;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int X;        // x position of upper-left corner
            public int Y;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;//The left button is down.
        public const int MOUSEEVENTF_LEFTUP = 0x0004;//The left button is up.
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;//The middle button is down.
        public const int MOUSEEVENTF_MIDDLEUP=0x0040;//The middle button is up.
        public const int MOUSEEVENTF_MOVE=0x0001;//Movement occurred.
        public const int MOUSEEVENTF_RIGHTDOWN=0x0008;//The right button is down.
        public const int MOUSEEVENTF_RIGHTUP=0x0010;//The right button is up.
        public const int MOUSEEVENTF_WHEEL = 0x0800;//The wheel has been moved, if the mouse has a wheel.The amount of movement is specified in dwData
        public const int MOUSEEVENTF_XDOWN=0x0080;//An X button was pressed.
        public const int MOUSEEVENTF_XUP=0x0100;//An X button was released.
        public const int MOUSEEVENTF_HWHEEL=0x01000; //The wheel button is tilted.

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        /// "MouseWheelUp()" is a function that simulates a mouse wheel up event.
        public static void MouseWheelUp()
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 120, 0);
        }
        /// MouseWheelDown() simulates a mouse wheel down event.
        public static void MouseWheelDown()
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -120, 0);
        }
        /// It gets the process by name, then sets the foreground window to the process's main window
        /// handle
        /// 
        /// @param process The name of the process you want to focus on.
        public static void SetFocus(string process)
        {
            Process pr = Process.GetProcessesByName(process)[0];
            SetForegroundWindow(pr.MainWindowHandle);
        }

        /// It gets the title of the active window
        /// 
        /// @return The title of the active window.
        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

    }
    public class MouseOperations
    {
        [Flags]
        /* Defining an enum. */
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void LeftClick()
        {
            MouseEvent(MouseEventFlags.LeftDown | MouseEventFlags.LeftUp);
        }
        public static void RightClick()
        {
            MouseEvent(MouseEventFlags.RightDown | MouseEventFlags.RightUp);
        }
        public static void LeftDown()
        {
            MouseEvent(MouseEventFlags.LeftDown);
        }
        public static void RightDown()
        {
            MouseEvent(MouseEventFlags.RightDown);
        }
        public static void LeftUp()
        {
            MouseEvent(MouseEventFlags.LeftUp);
        }
        public static void RightUp()
        {
            MouseEvent(MouseEventFlags.RightUp);
        }
        /// It sets the cursor position to the specified coordinates
        /// 
        /// @param x The x coordinate of the cursor.
        /// @param y The y coordinate of the cursor.
        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        /// It takes a MousePoint object and sets the cursor position to the X and Y coordinates of the
        /// MousePoint object
        /// 
        /// @param MousePoint A struct that contains the X and Y coordinates of the mouse.
        public static void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        /// GetCursorPosition() returns a MousePoint object that contains the current position of the
        /// mouse cursor
        /// 
        /// @return A MousePoint object.
        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        /// It simulates a mouse click at the current cursor position
        /// 
        /// @param MouseEventFlags This is the type of mouse event you want to simulate.
        public static void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();

            mouse_event
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0)
                ;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
    static public class FileUtil
    {
        [StructLayout(LayoutKind.Sequential)]
        struct RM_UNIQUE_PROCESS
        {
            public int dwProcessId;
            public System.Runtime.InteropServices.ComTypes.FILETIME ProcessStartTime;
        }

        const int RmRebootReasonNone = 0;
        const int CCH_RM_MAX_APP_NAME = 255;
        const int CCH_RM_MAX_SVC_NAME = 63;

        enum RM_APP_TYPE
        {
            RmUnknownApp = 0,
            RmMainWindow = 1,
            RmOtherWindow = 2,
            RmService = 3,
            RmExplorer = 4,
            RmConsole = 5,
            RmCritical = 1000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct RM_PROCESS_INFO
        {
            public RM_UNIQUE_PROCESS Process;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_RM_MAX_APP_NAME + 1)]
            public string strAppName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_RM_MAX_SVC_NAME + 1)]
            public string strServiceShortName;

            public RM_APP_TYPE ApplicationType;
            public uint AppStatus;
            public uint TSSessionId;
            [MarshalAs(UnmanagedType.Bool)]
            public bool bRestartable;
        }

        [DllImport("rstrtmgr.dll", CharSet = CharSet.Unicode)]
        static extern int RmRegisterResources(uint pSessionHandle,
                                              UInt32 nFiles,
                                              string[] rgsFilenames,
                                              UInt32 nApplications,
                                              [In] RM_UNIQUE_PROCESS[] rgApplications,
                                              UInt32 nServices,
                                              string[] rgsServiceNames);

        [DllImport("rstrtmgr.dll", CharSet = CharSet.Auto)]
        static extern int RmStartSession(out uint pSessionHandle, int dwSessionFlags, string strSessionKey);

        [DllImport("rstrtmgr.dll")]
        static extern int RmEndSession(uint pSessionHandle);

        [DllImport("rstrtmgr.dll")]
        static extern int RmGetList(uint dwSessionHandle,
                                    out uint pnProcInfoNeeded,
                                    ref uint pnProcInfo,
                                    [In, Out] RM_PROCESS_INFO[] rgAffectedApps,
                                    ref uint lpdwRebootReasons);
        static public List<Process> Locking(string path)
        {
            uint handle;
            string key = Guid.NewGuid().ToString();
            List<Process> processes = new List<Process>();

            int res = RmStartSession(out handle, 0, key);

            if (res != 0)
                throw new Exception("Could not begin restart session. Unable to determine file locker.");

            try
            {
                const int ERROR_MORE_DATA = 234;
                uint pnProcInfoNeeded = 0,
                     pnProcInfo = 0,
                     lpdwRebootReasons = RmRebootReasonNone;

                string[] resources = new string[] { path }; // Just checking on one resource.

                res = RmRegisterResources(handle, (uint)resources.Length, resources, 0, null, 0, null);

                if (res != 0)
                    throw new Exception("Could not register resource.");

                //Note: there's a race condition here -- the first call to RmGetList() returns
                //      the total number of process. However, when we call RmGetList() again to get
                //      the actual processes this number may have increased.
                res = RmGetList(handle, out pnProcInfoNeeded, ref pnProcInfo, null, ref lpdwRebootReasons);

                if (res == ERROR_MORE_DATA)
                {
                    // Create an array to store the process results
                    RM_PROCESS_INFO[] processInfo = new RM_PROCESS_INFO[pnProcInfoNeeded];
                    pnProcInfo = pnProcInfoNeeded;

                    // Get the list
                    res = RmGetList(handle, out pnProcInfoNeeded, ref pnProcInfo, processInfo, ref lpdwRebootReasons);

                    if (res == 0)
                    {
                        processes = new List<Process>((int)pnProcInfo);

                        // Enumerate all of the results and add them to the 
                        // list to be returned
                        for (int i = 0; i < pnProcInfo; i++)
                        {
                            try
                            {
                                processes.Add(Process.GetProcessById(processInfo[i].Process.dwProcessId));
                            }
                            // catch the error -- in case the process is no longer running
                            catch (ArgumentException) { }
                        }
                    }
                    else
                        throw new Exception("Could not list processes locking resource.");
                }
                else if (res != 0)
                    throw new Exception("Could not list processes locking resource. Failed to get size of result.");
            }
            finally
            {
                RmEndSession(handle);
            }

            return processes;
        }
    }
}
