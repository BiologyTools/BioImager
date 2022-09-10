using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Bio
{
    public class Win32
    {
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
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

        public static void MouseWheelUp()
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 120, 0);
        }
        public static void MouseWheelDown()
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -120, 0);
        }
        public static void SetFocus(string process)
        {
            Process pr = Process.GetProcessesByName(process)[0];
            SetForegroundWindow(pr.MainWindowHandle);
        }

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
        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

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
}
