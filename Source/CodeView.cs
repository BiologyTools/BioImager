using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bio
{
    public partial class CodeView : UserControl
    {
        private ScrollTextBox textBox = new ScrollTextBox();
        private ScrollTextBox lineBox = new ScrollTextBox();
        private int tabSize = 15;
        public CodeView()
        {
            InitializeComponent();
            textBox.Dock = DockStyle.Fill;
            textBox.MouseWheel += new MouseEventHandler(Code_MouseWheel);
            textBox.TextChanged += new EventHandler(textBox_TextChanged);
            textBox.VScroll += new EventHandler(textBox_Scroll);
            textBox.FontChanged += new EventHandler(textBox_FontChanged);
            textBox.WordWrap = false;
            textBox.AcceptsTab = true;
            panel.Controls.Add(textBox);
            lineBox.Dock = DockStyle.Fill;
            lineBox.ScrollBars = RichTextBoxScrollBars.None;
            panel2.Controls.Add(lineBox);
            //MouseWheel += new MouseEventHandler(Code_MouseWheel);
            textBox.SelectionTabs = new int[] { tabSize, tabSize * 2, tabSize * 3, tabSize * 4, tabSize * 5, tabSize * 6 };
            UpdateScroll();
        }
        public RichTextBox TextBox
        {
            get
            {
                return textBox;
            }
        }

        public bool WordWrap
        {
            get
            {
                return textBox.WordWrap;
            }
            set
            {
                textBox.WordWrap = value;
            }
        }
        public void UpdateScroll()
        {
            lineBox.VerticalScrollPosition = textBox.VerticalScrollPosition;
        }

        /// <summary>
        /// TextBox with support for getting and setting the vertical scroll bar
        /// position, as well as listening to vertical scroll events.
        /// </summary>
        public class ScrollTextBox : RichTextBox
        {

            public ScrollTextBox()
            {
                _components = new System.ComponentModel.Container();
                // Calculate width of "W" to set as the small horizontal increment
                OnFontChanged(null);
            }

            [System.ComponentModel.DefaultValue(0)
            , System.ComponentModel.Category("Appearance")
            , System.ComponentModel.Description
               ("Gets or sets the vertical scroll bar's position"
               )
            ]
            public int VerticalScrollPosition
            {
                set { SetScroll(value, Win32.WM_VSCROLL, Win32.SB_VERT); }
                get { return GetScroll(Win32.SB_VERT); }
            }

            [System.ComponentModel.DefaultValue(0)
            , System.ComponentModel.Category("Appearance")
            , System.ComponentModel.Description
               ("Gets or sets the horizontal scroll bar's position"
               )
            ]
            public int HorizontalScrollPosition
            {
                set { SetScroll(value, Win32.WM_HSCROLL, Win32.SB_HORZ); }
                get { return GetScroll(Win32.SB_HORZ); }
            }

            [System.ComponentModel.Description
               ("Fired when the scroll bar's vertical position changes"
               )
            , System.ComponentModel.Category("Property Changed")
            ]
            public event System.Windows.Forms.ScrollEventHandler ScrollChanged;

            // Fire scroll event if the scroll-bars are moved
            protected override void WndProc
            (ref Message message
            )
            {
                base.WndProc(ref message);
                if (message.Msg == Win32.WM_VSCROLL
                || message.Msg == Win32.WM_HSCROLL
                || message.Msg == Win32.WM_MOUSEWHEEL
                ) TryFireScrollEvent();
            }

            // Key-down includes navigation keys, but also typing and pasting can
            // cause a scroll event
            protected override void OnKeyDown
            (System.Windows.Forms.KeyEventArgs e
            )
            {
                base.OnKeyDown(e);
                TryFireScrollEvent();
            }

            protected override void OnKeyUp
            (System.Windows.Forms.KeyEventArgs e
            )
            {
                base.OnKeyUp(e);
                TryFireScrollEvent();
            }

            // Resizing can alter the word-wrap or fit the content causing the text
            // to scroll
            protected override void OnResize
            (System.EventArgs e
            )
            {
                base.OnResize(e);
                TryFireScrollEvent();
            }

            // Clicking an empty space can move the carot to the beginning of the
            // line causing a scroll event
            protected override void OnMouseDown
            (System.Windows.Forms.MouseEventArgs e
            )
            {
                base.OnMouseDown(e);
                TryFireScrollEvent();
            }

            protected override void OnMouseUp
            (System.Windows.Forms.MouseEventArgs e
            )
            {
                base.OnMouseUp(e);
                TryFireScrollEvent();
            }

            // If the mouse button is down, a text selection is probably being done
            // where dragging the selection can caues scroll events
            protected override void OnMouseMove
            (System.Windows.Forms.MouseEventArgs e
            )
            {
                base.OnMouseMove(e);
                if (e.Button != System.Windows.Forms.MouseButtons.None)
                    TryFireScrollEvent();
            }

            // Changing the size of the font can cause a scroll event. Also, when
            // scrolling horizontally, the event will notify whether the scroll
            // was a large or small change. For vertical, small increments are 1
            // line, but for horizontal, it is several pixels. To guess what a
            // small increment is, get the width of the W character and anything
            // smaller than that will be represented as a small increment
            protected override void OnFontChanged
            (System.EventArgs e
            )
            {
                base.OnFontChanged(e);
                using (System.Drawing.Graphics graphics = this.CreateGraphics())
                    _fontWidth = (int)graphics.MeasureString("W", this.Font).Width;
                TryFireScrollEvent();
            }

            protected override void Dispose
            (bool disposing
            )
            {
                if (disposing && (_components != null))
                    _components.Dispose();
                base.Dispose(disposing);
            }

            private void SetScroll
            (int value
            , uint windowsMessage
            , int scrollBarMessage
            )
            {
                Win32.SetScrollPos
                ((System.IntPtr)this.Handle
                , scrollBarMessage
                , value
                , true
                );
                Win32.PostMessage
                ((System.IntPtr)this.Handle
                , windowsMessage
                , 4 + 0x10000 * value
                , 0
                );
            }

            private int GetScroll
            (int scrollBarMessage
            )
            {
                return Win32.GetScrollPos
                ((System.IntPtr)this.Handle
                , scrollBarMessage
                );
            }

            // Fire both horizontal and vertical scroll events seperately, one
            // after the other. These first test if a scroll actually occurred
            // and won't fire if there was no actual movement
            private void TryFireScrollEvent()
            {  // Don't do anything if there is no event handler
                if (ScrollChanged == null)
                    return;
                TryFireHorizontalScrollEvent();
                TryFireVerticalScrollEvent();
            }

            private void TryFireHorizontalScrollEvent()
            {

                // Don't do anything if there is no event handler
                if (ScrollChanged == null)
                    return;

                int lastScrollPosition = _lastHorizontalScrollPosition;
                int scrollPosition = HorizontalScrollPosition;

                // Don't do anything if there was no change in position
                if (scrollPosition == lastScrollPosition)
                    return;

                _lastHorizontalScrollPosition = scrollPosition;

                ScrollChanged
                (this
                , new System.Windows.Forms.ScrollEventArgs
                   (scrollPosition < lastScrollPosition - _fontWidth
                      ? System.Windows.Forms.ScrollEventType.LargeDecrement
                      : scrollPosition > lastScrollPosition + _fontWidth
                      ? System.Windows.Forms.ScrollEventType.LargeIncrement
                      : scrollPosition < lastScrollPosition
                      ? System.Windows.Forms.ScrollEventType.SmallDecrement
                      : System.Windows.Forms.ScrollEventType.SmallIncrement
                   , lastScrollPosition
                   , scrollPosition
                   , System.Windows.Forms.ScrollOrientation.HorizontalScroll
                   )
                );

            }

            private void TryFireVerticalScrollEvent()
            {
                // Don't do anything if there is no event handler
                if (ScrollChanged == null)
                    return;
                int lastScrollPosition = _lastVerticalScrollPosition;
                int scrollPosition = VerticalScrollPosition;

                // Don't do anything if there was no change in position
                if (scrollPosition == lastScrollPosition)
                    return;

                _lastVerticalScrollPosition = scrollPosition;

                ScrollChanged
                (this
                , new System.Windows.Forms.ScrollEventArgs
                   (scrollPosition < lastScrollPosition - 1
                      ? System.Windows.Forms.ScrollEventType.LargeDecrement
                      : scrollPosition > lastScrollPosition + 1
                      ? System.Windows.Forms.ScrollEventType.LargeIncrement
                      : scrollPosition < lastScrollPosition
                      ? System.Windows.Forms.ScrollEventType.SmallDecrement
                      : System.Windows.Forms.ScrollEventType.SmallIncrement
                   , lastScrollPosition
                   , scrollPosition
                   , System.Windows.Forms.ScrollOrientation.VerticalScroll
                   )
                );

            }

            private int _lastVerticalScrollPosition;
            private int _lastHorizontalScrollPosition;
            private int _fontWidth;
            private System.ComponentModel.IContainer _components = null;

            private static class Win32
            {

                public const uint WM_HSCROLL = 0x114;
                public const uint WM_VSCROLL = 0x115;
                public const uint WM_MOUSEWHEEL = 0x20A;
                public const int SB_VERT = 0x1;
                public const int SB_HORZ = 0x0;

                [System.Runtime.InteropServices.DllImport("user32.dll")]
                public static extern int SetScrollPos
                (System.IntPtr hWnd
                , int nBar
                , int nPos
                , bool bRedraw
                );
                [System.Runtime.InteropServices.DllImport("user32.dll")]
                public static extern int GetScrollPos
                (System.IntPtr hWnd
                , int nBar
                );

                [System.Runtime.InteropServices.DllImport
                   ("User32.Dll"
                   , EntryPoint = "PostMessageA")
                ]
                public static extern bool PostMessage
                (System.IntPtr hWnd
                , uint msg
                , int wParam
                , int lParam
                );

            }

        }
        private void Code_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            UpdateScroll();
        }
        private void textBox_Scroll(object sender, EventArgs e)
        {
            UpdateScroll();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            lineBox.Text = "";
            for (int i = 0; i < textBox.Lines.Length; i++)
            {
                lineBox.Text += (i + 1).ToString() + Environment.NewLine;
            }
            UpdateScroll();
        }

        private void textBox_FontChanged(object sender, EventArgs e)
        {
            lineBox.Font = textBox.Font;
        }

        private void CodeView_Resize(object sender, EventArgs e)
        {
            UpdateScroll();
        }
    }
}
