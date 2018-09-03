using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            WindowInteropHelper interopHelper = new WindowInteropHelper(this);

            // Disable the maximize button
            int style = GetWindowLong(interopHelper.Handle, GWL_STYLE);
            SetWindowLong(interopHelper.Handle, GWL_STYLE, style & ~WS_MAXIMIZEBOX);

            // Set WS_EX_NOACTIVATE - this will enable interaction with the window without grabbing focus
            int exStyle = GetWindowLong(interopHelper.Handle, GWL_EXSTYLE);
            SetWindowLong(interopHelper.Handle, GWL_EXSTYLE, exStyle | WS_EX_NOACTIVATE);
        }
    }
}
