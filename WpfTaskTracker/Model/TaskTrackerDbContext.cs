using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTaskTracker.Model
{
    public class TaskTrackerDbContext : DbContext
    {
        public TaskTrackerDbContext() : base("TaskTrackerDb")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<TaskTrackerDBContext>());
        }

        public DbSet<Task> Tasks { get; set; }  
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
