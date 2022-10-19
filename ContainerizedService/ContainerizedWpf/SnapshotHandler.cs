using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ContainerizedWpf
{
    public sealed class SnapshotHandler
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int m_left;
            public int m_top;
            public int m_right;
            public int m_bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

        public static void Savesnapshot(IntPtr handle_)
        {
            RECT windowRect = new RECT();
            GetWindowRect(handle_, ref windowRect);

            Int32 width = windowRect.m_right - windowRect.m_left;
            Int32 height = windowRect.m_bottom - windowRect.m_top;
            Point topLeft = new Point(windowRect.m_left, windowRect.m_top);

            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(topLeft, new Point(0, 0), new Size(width, height));

            var assembly = Assembly.GetEntryAssembly();
            var exe_location = assembly.Location;
            var dir = new FileInfo(exe_location).DirectoryName;

            b.Save(dir + @"\screenshot", ImageFormat.Jpeg);
        }
    }
}
