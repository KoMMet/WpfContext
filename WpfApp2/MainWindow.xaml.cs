﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr windowhandle = new WindowInteropHelper(this).Handle;
            HwndSource hwndSource = HwndSource.FromHwnd(windowhandle);

            hwndSource.AddHook(new HwndSourceHook(WndProc));
        }
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;
            MessageBox.Show(item.Header.ToString());
        }
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if(msg == 0xa4)
            {
                ShowContextMenu();
                handled = true;
            }
            return IntPtr.Zero;
        }

        private void ShowContextMenu()
        {
            var contextMenu = Resources["contextMenu"] as ContextMenu;
            contextMenu.IsOpen = true;
        }
    }

}
