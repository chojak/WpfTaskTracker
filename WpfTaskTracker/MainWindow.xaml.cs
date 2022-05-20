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
        public TaskTrackerDbContext DbContext;

        private void LoadCategories()
        {
            List<string> categories = new List<string>();
            categories.Add("All");

            foreach (var category in DbContext.Categories)
                categories.Add(category.Name);

            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.SelectedIndex = 0;
        }

        private void LoadTasks(string category = "All")
        {
            List<Task> tasks = new List<Task>();
            List<TreeViewItem> tasksForView = new List<TreeViewItem>();

            if (category == "All")
                tasks = DbContext.Tasks.Include("Subtasks").ToList();
            else 
                tasks = DbContext.Tasks.Include("Subtasks").Where(t => t.Category.Name == category).ToList();

            foreach (var task in tasks)
            {
                Button bt = new Button()
                {
                    Name = task.Name + task.TaskId,
                    Content = task.Name,
                    Background = Brushes.Transparent,
                    Margin = new Thickness(5, 0, 5, 0),
                    BorderThickness = new Thickness(0),
                };
                bt.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditTask_Click));

                StackPanel sp = new StackPanel() { Orientation = Orientation.Horizontal };
                sp.Children.Add(new CheckBox() { Name = task.Name + task.TaskId });
                sp.Children.Add(bt);
                
                TreeViewItem TaskItem = new TreeViewItem();
                TaskItem.Header = sp;

                if (task.Subtasks != null)  
                    foreach (var subtask in task.Subtasks)
                    {
                        bt = new Button()
                        {
                            Name = subtask.Name + subtask.TaskId,
                            Content = subtask.Name,
                            Background = Brushes.Transparent,
                            Margin = new Thickness(5, 0, 5, 0),
                            BorderThickness = new Thickness(0),
                        };
                        bt.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditSubtask_Click));

                        sp = new StackPanel() { Orientation = Orientation.Horizontal };
                        sp.Children.Add(new CheckBox() { Name = subtask.Name + subtask.TaskId });
                        sp.Children.Add(bt);

                        TreeViewItem SubtaskItem = new TreeViewItem();
                        SubtaskItem.Header = sp;

                        TaskItem.Items.Add(SubtaskItem);
                    }
                tasksForView.Add(TaskItem);
            }
            TasksTreeView.ItemsSource = tasksForView; 
        }

        public MainWindow()
        {
            InitializeComponent();

            DbContext = new TaskTrackerDbContext();
            
            LoadCategories();
            LoadTasks();
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

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadTasks((string)CategoryComboBox.SelectedValue);
        }
    }
}
