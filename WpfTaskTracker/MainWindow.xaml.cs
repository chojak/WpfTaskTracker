﻿using System;
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

        private void FillDatabase()
        {
            if (DbContext.Tasks.Count() == 0)
            {
                DbContext.Tasks.Add(new Task() 
                { 
                    CategoryId = 1, 
                    Name = "Sprzatanie domu", 
                    Urgency = 3, 
                    StartDate = DateTime.Now, 
                    EndDate = new DateTime(2137, 4, 20),
                    Subtasks = new List<Subtask> { 
                        new Subtask() { Name = "Wstac od komputera" },
                        new Subtask() { Name = "Zebrac smieci" },
                        new Subtask() { Name = "Wyrzucic smieci" },
                        new Subtask() { Name = "Wrocic do komputera" },
                    }
                });

                DbContext.Tasks.Add(new Task()
                {
                    CategoryId = 2,
                    Name = "Zaliczenie sesji",
                    Urgency = 0,
                    StartDate = DateTime.Now,
                    EndDate = new DateTime(2137, 4, 20),
                    Subtasks = new List<Subtask> {
                        new Subtask() { Name = "Pojsc na uczelnie" },
                        new Subtask() { Name = "Usiasc przy komputerze" },
                        new Subtask() { Name = "Stackoverflow" },
                        new Subtask() { Name = "Wstac od komputera" },
                        new Subtask() { Name = "Pojsc na modlitwe" },
                    }
                });

                DbContext.Tasks.Add(new Task()
                {
                    CategoryId = 3,
                    Name = "Zajecia BSK",
                    Urgency = 7,
                    StartDate = DateTime.Now,
                    EndDate = new DateTime(2137, 4, 20),
                    Subtasks = new List<Subtask> {
                        new Subtask() { Name = "Wyjsc z uczelni" },
                        new Subtask() { Name = "Zabka" },
                        new Subtask() { Name = "Las" },
                        new Subtask() { Name = "BSK" },
                    }
                });
            }
            if (DbContext.Categories.Count() == 0)
            {
                DbContext.Categories.Add(new Category() { Name = "Kategoria 1" });
                DbContext.Categories.Add(new Category() { Name = "Kategoria 2" });
                DbContext.Categories.Add(new Category() { Name = "Kategoria 3" });
            }
            DbContext.SaveChanges();
        }
        
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
                    Name = "Task" + task.TaskId,
                    Content = task.Name,
                    Background = Brushes.Transparent,
                    Margin = new Thickness(5, 0, 5, 0),
                    BorderThickness = new Thickness(0),
                };
                bt.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditTask_Click));

                CheckBox cb = new CheckBox()
                {
                    Name = "Task" + task.TaskId,
                };

                StackPanel sp = new StackPanel() { Orientation = Orientation.Horizontal };
                sp.Children.Add(new CheckBox() { Name = "Task" + task.TaskId });
                sp.Children.Add(bt);
                
                TreeViewItem TaskItem = new TreeViewItem();
                TaskItem.Header = sp;

                if (task.Subtasks != null)  
                    foreach (var subtask in task.Subtasks)
                    {
                        bt = new Button()
                        {
                            Name = "Subtask" + subtask.TaskId,
                            Content = subtask.Name,
                            Background = Brushes.Transparent,
                            Margin = new Thickness(5, 0, 5, 0),
                            BorderThickness = new Thickness(0),
                        };
                        bt.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditSubtask_Click));

                        sp = new StackPanel() { Orientation = Orientation.Horizontal };
                        sp.Children.Add(new CheckBox() { Name = "Subtask" + subtask.TaskId });
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

            FillDatabase();
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
