using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern bool InsertMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags, Int32 wIDNewItem,
                string lpNewItem);

        private readonly Int32 MF_BYPOSITION = 0x400;
        private readonly Int32 MF_SEPARATOR = 0x800;
        private const Int32 ITEMONEID = 1000;
        private const Int32 ITEMTWOID = 1001;

        private readonly Int32 WM_SYSCOMMAND = 0x112;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr windowhandle = new WindowInteropHelper(this).Handle;
            HwndSource hwndSource = HwndSource.FromHwnd(windowhandle);
            IntPtr systemMenuHandle = GetSystemMenu(windowhandle, false);
            InsertMenu(systemMenuHandle,5, MF_BYPOSITION| MF_SEPARATOR, 0, string.Empty);
            InsertMenu(systemMenuHandle,6, MF_BYPOSITION, ITEMONEID, "Item 1");
            InsertMenu(systemMenuHandle,7, MF_BYPOSITION, ITEMTWOID, "Item 2");

            hwndSource.AddHook(new HwndSourceHook(WndProc));
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if(msg == WM_SYSCOMMAND)
            {
                switch(wparam.ToInt32())
                {
                    case ITEMONEID:
                    {
                        MessageBox.Show("Item 1 was clicked");
                        handled = true;
                        break;
                    }
                    case ITEMTWOID:
                    {
                        MessageBox.Show("Item 2 was clicked");
                        handled = true;
                        break;
                    }
                }
            }

            return IntPtr.Zero;
        }
    }
}
