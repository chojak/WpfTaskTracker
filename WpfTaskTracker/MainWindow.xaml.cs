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
using WpfTaskTracker.Model;

namespace WpfTaskTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var ctx = new TaskTrackerDbContext())
            {
                var stud = new Task() { Name = "Bill" };

                ctx.Tasks.Add(stud);
                ctx.SaveChanges();
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow win2 = new AddTaskWindow();
            win2.Show();
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            EditTaskWindow win = new EditTaskWindow();
            win.Show();
        }

        private void EditSubtask_Click(object sender, RoutedEventArgs e)
        {
            EditSubtaskWindow win = new EditSubtaskWindow();
            win.Show();
        }
    }
}
