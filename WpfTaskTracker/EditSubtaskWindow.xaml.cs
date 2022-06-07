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
        public Subtask Subtask { get; set; }
        public List<Subtask> Subtasks { get; set; }

        public EditSubtaskWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            TaskNameInput.Text = Subtask == null ? "empty name (error)" : Subtask.Name;
        }

        private void SaveSubtask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskNameInput.Text.Length < 4)
            {
                MessageBox.Show("Subtask name must contain at least 4 characters");
                return;
            }

            Subtasks[Subtasks.IndexOf(Subtask)].Name = TaskNameInput.Text;
            DialogResult = true;
            Close();
        }

        private void DeleteSubtask_Click(object sender, RoutedEventArgs e)
        {
            Subtasks.Remove(Subtask);
            DialogResult = true;
            Close();
        }
    }
}
