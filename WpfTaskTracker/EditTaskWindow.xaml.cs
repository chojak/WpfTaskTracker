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
using WpfTaskTracker.Model;

namespace WpfTaskTracker
{
    /// <summary>
    /// Logika interakcji dla klasy EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        public Task Task { get; set; }
        public List<Subtask> Subtasks { get; set; }
        public List<Category> Categories { get; set; }
        public List<Button> SubtasksButtons { get; set; }
        public TaskTrackerDbContext DbContext { get; set; }

        public bool Delete { get; set; }

        private void LoadSubtasks()
        {
            SubtasksButtons = new List<Button>();

            foreach (var subtask in Subtasks)
            {
                Button btn = new Button();
                btn.Content = subtask.Name;
                btn.Style = Resources["StaticResource TaskButton"] as Style;
                btn.SetValue(Grid.RowProperty, 1);
                btn.Click += EditSubtask_Click;
                SubtasksButtons.Add(btn);
            }

            SubtasksListBox.ItemsSource = SubtasksButtons;
        }

        public EditTaskWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            TaskNameInput.Text = Task.Name;
            CategoryComboBox.ItemsSource = Categories;
            CategoryComboBox.SelectedIndex = Task.CategoryId != null ? (int)Task.CategoryId - 1 : 0;
            DifficultySliderNewTask.Value = Task.Urgency != null ? (int)Task.Urgency : 1;
            
            if (Task.StartDate != null)
            {
                StartDateCheckBox.IsChecked = true;
                StartDateDatePicker.SelectedDate = Task.StartDate;
            }

            if (Task.EndDate != null)
            {
                EndDateCheckBox.IsChecked = true;
                EndDateDatePicker.SelectedDate = Task.EndDate;
            }

            if (Task.Subtasks.Count > 0)
            {
                SubTaskCheckBox.IsChecked = true;
                LoadSubtasks();
            }
        }

        private void AddSubTask_Click(object sender, RoutedEventArgs e)
        {
            AddSubTaskWindow win = new AddSubTaskWindow();
            win.subtask = new Subtask();

            if (true == win.ShowDialog())
                if (win.subtask.Name != "")
                    Subtasks.Add(win.subtask);

            LoadSubtasks();
        }

        private void SaveTask_Click(object sender, RoutedEventArgs e)
        {
            Category newCategory = new Category();

            if (TaskNameInput.Text.Length < 4)
            {
                MessageBox.Show("Task name must contain at least 4 characters");
                return;
            }
            Task.Name = TaskNameInput.Text;

            if (NewCategoryCheckBox.IsChecked == true)
            {
                if (NewCategoryNameTextBox.Text.Length < 3)
                {
                    MessageBox.Show("New category name must contain at least 4 characters");
                    return;
                }
                newCategory.Name = NewCategoryNameTextBox.Text;
                Task.Category = newCategory;
            }
            else
            {
                string selectedCategoryName = CategoryComboBox.SelectedValue.ToString();
                Task.Category = Categories.Where(c => c.Name == selectedCategoryName).FirstOrDefault();
            }

            Task.Urgency = (int)DifficultySliderNewTask.Value;

            if (StartDateCheckBox.IsChecked == true)
            {
                if (StartDateDatePicker.Text == "")
                {
                    MessageBox.Show("Starting date cannot be empty");
                    return;
                }
                Task.StartDate = StartDateDatePicker.SelectedDate.Value.Date;
            }

            if (EndDateCheckBox.IsChecked == true)
            {
                if (StartDateDatePicker.Text == "")
                {
                    MessageBox.Show("Starting date cannot be empty");
                    return;
                }
                Task.EndDate = EndDateDatePicker.SelectedDate.Value.Date;
            }

            if (StartDateCheckBox.IsChecked == true && EndDateCheckBox.IsChecked == true &&
                DateTime.Compare((DateTime)Task.StartDate, (DateTime)Task.EndDate) > 0)
            {
                MessageBox.Show("Ending date cannot be earlier than starting date");
                return;
            }

            if (SubTaskCheckBox.IsChecked == true && Subtasks.Count > 0)
            {
                foreach (Subtask subtask in Task.Subtasks)
                {
                    if (!Subtasks.Contains(subtask))
                    {
                        DbContext.Subtasks.Remove(subtask);
                    }
                }

                foreach (var subtask in Subtasks)
                {
                    if (subtask.SubtaskId != null)
                    {
                        subtask.SubtaskId = subtask.SubtaskId;
                        subtask.IsCompleted = subtask.IsCompleted;
                    }
                    subtask.Task = Task;
                }


                Task.Subtasks = Subtasks;
            }
            else
            {
                foreach (var subtask in Subtasks)
                {
                    DbContext.Subtasks.Remove(subtask);
                }
            }

            Delete = false;
            DialogResult = true;
            DbContext.SaveChanges();
            Close();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            Delete = true;
            DialogResult = true;
            Close();
        }

        private void EditSubtask_Click(object sender, RoutedEventArgs e)
        {
            EditSubtaskWindow win = new EditSubtaskWindow();
            Button subtaskButton = (Button)sender;

            win.Subtask = Subtasks.Where(s => s.Name == subtaskButton.Content.ToString()).FirstOrDefault();
            win.Subtasks = Subtasks;

            if (win.ShowDialog() == true)
            {

            }

            LoadSubtasks();
        }
    }
}
