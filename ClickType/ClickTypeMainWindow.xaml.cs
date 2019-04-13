using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace ClickType
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ClickTypeMainWindow : Window
    {
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int WS_MAXIMIZEBOX = 0x10000;

        // Window style
        private const int GWL_STYLE = -16;
        // Extended Window style
        private const int GWL_EXSTYLE = -20;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);


        public ClickTypeMainWindow()
        {
            InitializeComponent();
        }

        private void SnippetsListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = (ListBoxItem)sender;

            ((ClickTypeViewModel)this.DataContext).TypeSnippet(listBoxItem.DataContext as Snippet);
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            DisableMaximizeButton();
            SetWindowNoActivate();
        }

        private void DisableMaximizeButton()
        {
            WindowInteropHelper interopHelper = new WindowInteropHelper(this);
            int style = GetWindowLong(interopHelper.Handle, GWL_STYLE);
            SetWindowLong(interopHelper.Handle, GWL_STYLE, style & ~WS_MAXIMIZEBOX);
        }

        private void SetWindowNoActivate()
        {
            WindowInteropHelper interopHelper = new WindowInteropHelper(this);

            // Set WS_EX_NOACTIVATE - this will enable interaction with the window without grabbing focus
            int exStyle = GetWindowLong(interopHelper.Handle, GWL_EXSTYLE);
            SetWindowLong(interopHelper.Handle, GWL_EXSTYLE, exStyle | WS_EX_NOACTIVATE);
        }

        private void ResetWindowNoActivate()
        {
            WindowInteropHelper interopHelper = new WindowInteropHelper(this);
            
            // Reset WS_EX_NOACTIVATE - this will make the window normal
            int exStyle = GetWindowLong(interopHelper.Handle, GWL_EXSTYLE);
            SetWindowLong(interopHelper.Handle, GWL_EXSTYLE, exStyle & ~WS_EX_NOACTIVATE);
        }

        private void SnippetTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ResetWindowNoActivate();
            GetWindow(this).Activate();
        }

        private void SnippetTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SetWindowNoActivate();
        }
    }
}
