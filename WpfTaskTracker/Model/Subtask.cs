using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTaskTracker
{
    public class Subtask
    {
        public int SubtaskId { get; set; }
        public string Name { get; set; }

        [ForeignKey("TaskId")]
        public Task Task { get; set; }
        public int TaskId { get; set; }
    }
}
