using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Logika interakcji dla klasy AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public TaskTrackerDbContext DbContext;
        public List<Subtask> Subtasks { get; set; }
        public List<Button> SubtasksButtons { get; set; }

        private void LoadCategories()
        {
            List<string>categories = new List<string>();

            foreach (var category in DbContext.Categories)
                categories.Add(category.Name);

            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.SelectedIndex = 0;
        }

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

        public AddTaskWindow()
        {
            InitializeComponent();

            DbContext = new TaskTrackerDbContext();
            Subtasks = new List<Subtask>();
            LoadCategories();
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
            Task newTask = new Task();
            Category newCategory = new Category();

            if (TaskNameInput.Text.Length < 4)
            {
                MessageBox.Show("Task name must contain at least 4 characters");
                return;
            }
            newTask.Name = TaskNameInput.Text;

            if (NewCategoryCheckBox.IsChecked == true)
            {
                if (NewCategoryNameTextBox.Text.Length < 3)
                {
                    MessageBox.Show("New category name must contain at least 4 characters");
                    return;
                }
                newCategory.Name = NewCategoryNameTextBox.Text;
                newTask.Category = newCategory;
            }
            else
            {
                string selectedCategory = CategoryComboBox.SelectedValue.ToString();
                newTask.Category = DbContext.Categories.Where(c => c.Name == selectedCategory).FirstOrDefault();
            }

            newTask.Urgency = (int)DifficultySliderNewTask.Value;

            if (StartDateCheckBox.IsChecked == true)
            {
                if (StartDateDatePicker.Text == "")
                {
                    MessageBox.Show("Starting date cannot be empty");
                    return;
                }
                newTask.StartDate = StartDateDatePicker.SelectedDate.Value.Date;
            }

            if (EndDateCheckBox.IsChecked == true)
            {
                if (StartDateDatePicker.Text == "")
                {
                    MessageBox.Show("Starting date cannot be empty");
                    return;
                }
                newTask.EndDate = EndDateDatePicker.SelectedDate.Value.Date;
            }

            if (StartDateCheckBox.IsChecked == true && EndDateCheckBox.IsChecked == true &&
                DateTime.Compare((DateTime)newTask.StartDate, (DateTime)newTask.EndDate) > 0)
            {
                MessageBox.Show("Ending date cannot be earlier than starting date");
                return;
            }

            if (SubTaskCheckBox.IsChecked == true && Subtasks.Count > 0)
            {
                foreach (var subtask in Subtasks)
                {
                    subtask.IsCompleted = false;
                    subtask.Task = newTask;
                }
                newTask.Subtasks = Subtasks;
            }

            newTask.IsCompleted = false;

            DbContext.Tasks.Add(newTask);
            DbContext.SaveChanges();
            DialogResult = true;
            Close();
        }

        private void EditSubtask_Click(object sender, RoutedEventArgs e)
        {
            EditSubtaskWindow win = new EditSubtaskWindow();
            Button subtaskButton = (Button)sender;
            System.Diagnostics.Debug.WriteLine(subtaskButton.Content.ToString());
            System.Diagnostics.Debug.WriteLine(subtaskButton.Content);

            win.Subtask = Subtasks.Where(s => s.Name == subtaskButton.Content.ToString()).FirstOrDefault();
            win.Subtasks = Subtasks;

            if (win.ShowDialog() == true)
            {

            }

            LoadSubtasks();
        }
    }
}
