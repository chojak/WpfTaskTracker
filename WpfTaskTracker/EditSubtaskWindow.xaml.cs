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
using System.Windows.Shapes;

namespace WpfTaskTracker
{
    /// <summary>
    /// Logika interakcji dla klasy EditSubtaskWindow.xaml
    /// </summary>
    public partial class EditSubtaskWindow : Window
    {
        public EditSubtaskWindow()
        {
            InitializeComponent();
        }

        private void SaveSubtask_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DeleteSubtask_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
