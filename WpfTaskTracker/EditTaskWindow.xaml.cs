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
    /// Logika interakcji dla klasy EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        public EditTaskWindow()
        {
            InitializeComponent();
        }

        private void AddSubTask_Click(object sender, RoutedEventArgs e)
        {
            AddSubTaskWindow win = new AddSubTaskWindow();
            win.Show();
        }

        private void SaveTask_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditSubtask_Click(object sender, RoutedEventArgs e)
        {
            EditSubtaskWindow win = new EditSubtaskWindow();
            win.Show();
        }
    }
}
