using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Title.Views
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TitleBar : UserControl
    {
        private Point startPos;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        static extern int TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y, int nReserved, IntPtr hWnd, IntPtr prcRect);

        public TitleBar()
        {
            InitializeComponent();
        }

        private void System_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window thisWin = Window.GetWindow(this);

            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount >= 2)
                    WinState_Click(null, null);
                else
                    startPos = e.GetPosition(null);
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                var pos = PointToScreen(e.GetPosition(this));
                IntPtr hWnd = new System.Windows.Interop.WindowInteropHelper(thisWin).Handle;
                IntPtr hMenu = GetSystemMenu(hWnd, false);
                int cmd = TrackPopupMenu(hMenu, 0x100, (int)pos.X, (int)pos.Y, 0, hWnd, IntPtr.Zero);
                if (cmd > 0) SendMessage(hWnd, 0x112, (IntPtr)cmd, IntPtr.Zero);
            }
        }

        private void System_MouseMove(object sender, MouseEventArgs e)
        {
            Window thisWin = Window.GetWindow(this);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (thisWin.WindowState == WindowState.Maximized && Math.Abs(startPos.Y - e.GetPosition(null).Y) > 2)
                {
                    var point = PointToScreen(e.GetPosition(null));

                    WinState_Click(null, null);

                    thisWin.Top = point.Y - 10;
                    thisWin.Left = point.X - 500;
                }
                thisWin.DragMove();
            }
        }

        private void Minimized_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void WinState_Click(object sender, RoutedEventArgs e)
        {
            Window thisWin = Window.GetWindow(this);

            if (thisWin.WindowState == WindowState.Normal)
            {
                thisWin.BorderThickness = new Thickness(5);
                thisWin.WindowState = WindowState.Maximized;
                rectMax.Visibility = Visibility.Hidden;
                rectMin.Visibility = Visibility.Visible;
            }
            else
            {
                thisWin.BorderThickness = new Thickness(0);
                thisWin.WindowState = WindowState.Normal;
                rectMax.Visibility = Visibility.Visible;
                rectMin.Visibility = Visibility.Hidden;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
