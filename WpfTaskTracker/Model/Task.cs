using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTaskTracker
{
    public class Task
    {
        public int TaskId { get; set; }

        public string Name { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int? CategoryId { get; set; }

        public int? Urgency { get; set; }

        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsCompleted { get; set; }

        public ICollection<Subtask> Subtasks { get; set; }
    }
}
