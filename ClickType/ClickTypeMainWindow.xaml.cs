using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
        public ClickTypeMainWindow()
        {
            InitializeComponent();
        }

        private void SnippetsListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
