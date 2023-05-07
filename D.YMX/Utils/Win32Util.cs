using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace D.YMX.Utils
{
    /// <summary>
    /// W32 API，圆角,边框阴影等
    /// </summary>
    public class Win32Util
    {
        #region 禁用滚动条
        [DllImport("user32.dll")]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);
        private enum ScrollBarDirection { SB_HORZ = 0, SB_VERT = 1, SB_CTL = 2, SB_BOTH = 3 }
        //protected override void WndProc(ref System.Windows.Forms.Message m)
        //{
        //    ShowScrollBar(flowLayoutPanel.Handle, (int)ScrollBarDirection.SB_BOTH, false); base.WndProc(ref m);
        //}
        #endregion

        #region 圆角

        /// <summary>
        /// 设置窗体的圆⾓
        /// </summary>
        /// <param name="form">需要设置的窗体</param>
        /// <param name="rgnRadius">圆⾓矩形的半径</param>
        public static void SetFormRoundRectRgn(Form form, int rgnRadius = 2)
        {
            if (form != null)
            {
                int hRgn = 0;

                hRgn = Win32Util.CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, rgnRadius, rgnRadius);
                Win32Util.SetWindowRgn(form.Handle, hRgn, true);
                Win32Util.DeleteObject(hRgn);
            }

        }

        /// <summary>
        /// 设置控件的圆角
        /// </summary>
        /// <param name="control"></param>
        /// <param name="rgnRadius"></param>
        public static void SetControlRoundRectRgn(Control control, int rgnRadius = 2)
        {
            if (control != null)
            {
                int hRgn = 0;
                hRgn = Win32Util.CreateRoundRectRgn(0, 0, control.Width + 1, control.Height + 1, rgnRadius, rgnRadius);
                Win32Util.SetWindowRgn(control.Handle, hRgn, true);
                Win32Util.DeleteObject(hRgn);
            }

        }

        /// <summary>
        /// 创建一个圆角矩形，该矩形由X1，Y1-X2，Y2确定，并由X3，Y3确定的椭圆描述圆角弧度 返回值 Long，执行成功则为区域句柄，失败则为0
        /// </summary>
        /// <param name="x1">矩形左上角的X坐标</param>
        /// <param name="y1">矩形左上角的Y坐标</param>
        /// <param name="x2">矩形右下角的X</param>
        /// <param name="y2">矩形右下角的Y坐标</param>
        /// <param name="x3">圆角椭圆的宽。其范围从0（没有圆角）到矩形宽（全圆）</param>
        /// <param name="y3">圆角椭圆的高。其范围从0（没有圆角）到矩形高（全圆）</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);

        /// <summary>
        /// 创建圆角悬浮窗
        /// </summary>
        /// <param name="hwnd">窗口句柄及间接给出的窗口所属的类</param>
        /// <param name="hRgn"></param>
        /// <param name="bRedraw"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

        /// <summary>
        /// 把占用的内存释放回去给系统
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Ansi)]
        public static extern int DeleteObject(int hObject);

        #endregion

        #region 窗体边框阴影效果变量申明

        /// <summary>
        /// //API函数加载，实现窗体边框阴影效果
        /// </summary>
        /// <param name="form"></param>
        public static void SetFormShadow(Form form)
        {
            SetClassLong(form.Handle, GCL_STYLE, GetClassLong(form.Handle, GCL_STYLE) | CS_DropSHADOW);
        }

        const int CS_DropSHADOW = 0x20000;

        /// <summary>
        /// 替换窗口类的风格位
        /// </summary>
        const int GCL_STYLE = (-26);

        /// <summary>
        /// 它会替换存储空间中指定偏移量处的32位长整型值，或替换指定窗口所属类的WNDCLASSEX结构(应该是替换这个结构体中的值，并没有把结构体给换了
        /// </summary>
        /// <param name="hwnd">窗口句柄及间接给出的窗口所属的类</param>
        /// <param name="nIndex">指定将被替换的32位值。在额外类存储空间中设置32位值，应指定一个大于或等于0的偏移量。有效值的范围从0到额外类的存储空间的字节数一4</param>
        /// <param name="dwNewLong">指定的替换值</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        #endregion

        #region 窗体拖动

        /// <summary>
        /// 通过Windows的API控制窗体的拖动
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        public static void MouseDown(IntPtr hwnd)
        {
            Win32Util.ReleaseCapture();
            Win32Util.SendMessage(hwnd, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        /// <summary>
        /// The wm syscommand
        /// </summary>
        public const int WM_SYSCOMMAND = 0x0112;
        /// <summary>
        /// The sc move
        /// </summary>
        public const int SC_MOVE = 0xF010;
        /// <summary>
        /// The htcaption
        /// </summary>
        public const int HTCAPTION = 0x0002;

        /// <summary>
        /// Releases the capture.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="wMsg">The w MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);


        [DllImport("User32.DLL")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        public const uint WM_SETTEXT = 0x000C;

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);
        #endregion

        #region W32 Const

        public const int WM_ERASEBKGND = 0x0014;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_WINDOWPOSCHANGING = 0x46;
        public const int WM_PAINT = 0xF;
        public const int WM_CREATE = 0x0001;
        public const int WM_ACTIVATE = 0x0006;
        public const int WM_NCCREATE = 0x0081;
        public const int WM_NCCALCSIZE = 0x0083;
        public const int WM_NCPAINT = 0x0085;
        public const int WM_NCACTIVATE = 0x0086;
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_NCLBUTTONUP = 0x00A2;
        public const int WM_NCLBUTTONDBLCLK = 0x00A3;
        public const int WM_NCMOUSEMOVE = 0x00A0;
        public const int WM_NCHITTEST = 0x0084;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 0x10;
        public const int HTBOTTOMRIGHT = 17;
        public const int HTCLIENT = 1;
        public const int WM_FALSE = 0;
        public const int WM_TRUE = 1;
        #endregion 

        #region Public extern methods

        /// <summary>
        /// Struct Size
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            /// <summary>
            /// The cx
            /// </summary>
            public Int32 cx;
            /// <summary>
            /// The cy
            /// </summary>
            public Int32 cy;

            /// <summary>
            /// Initializes a new instance of the <see cref="Size" /> struct.
            /// </summary>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            public Size(Int32 x, Int32 y)
            {
                cx = x;
                cy = y;
            }
        }

        /// <summary>
        /// Struct BLENDFUNCTION
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            /// <summary>
            /// The blend op
            /// </summary>
            public byte BlendOp;
            /// <summary>
            /// The blend flags
            /// </summary>
            public byte BlendFlags;
            /// <summary>
            /// The source constant alpha
            /// </summary>
            public byte SourceConstantAlpha;
            /// <summary>
            /// The alpha format
            /// </summary>
            public byte AlphaFormat;
        }

        /// <summary>
        /// Struct Point
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            /// <summary>
            /// The x
            /// </summary>
            public Int32 x;
            /// <summary>
            /// The y
            /// </summary>
            public Int32 y;

            /// <summary>
            /// Initializes a new instance of the <see cref="Point" /> struct.
            /// </summary>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            public Point(Int32 x, Int32 y)
            {
                this.x = x;
                this.y = y;
            }
        }

        /// <summary>
        /// The ac source over
        /// </summary>
        public const byte AC_SRC_OVER = 0;
        /// <summary>
        /// The ulw alpha
        /// </summary>
        public const Int32 ULW_ALPHA = 2;
        /// <summary>
        /// The ac source alpha
        /// </summary>
        public const byte AC_SRC_ALPHA = 1;

        /// <summary>
        /// Creates the compatible dc.
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        /// <summary>
        /// Gets the dc.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// Selects the object.
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <param name="hObj">The h object.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObj);

        /// <summary>
        /// Releases the dc.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="hDC">The h dc.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// Deletes the dc.
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteDC(IntPtr hDC);

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="hObj">The h object.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteObject(IntPtr hObj);

        /// <summary>
        /// Updates the layered window.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="hdcDst">The HDC DST.</param>
        /// <param name="pptDst">The PPT DST.</param>
        /// <param name="psize">The psize.</param>
        /// <param name="hdcSrc">The HDC source.</param>
        /// <param name="pptSrc">The PPT source.</param>
        /// <param name="crKey">The cr key.</param>
        /// <param name="pblend">The pblend.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        /// <summary>
        /// Exts the create region.
        /// </summary>
        /// <param name="lpXform">The lp xform.</param>
        /// <param name="nCount">The n count.</param>
        /// <param name="rgnData">The RGN data.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr ExtCreateRegion(IntPtr lpXform, uint nCount, IntPtr rgnData);
        #endregion



    }
}
