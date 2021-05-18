using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoBlazor.Models
{
    public enum TodoPriority
    {
        None = 0, Low, Medium, High
    }

    public class Todo
    {
        public int Id { get; set; }

        [Required]
        public TodoPriority Priority { get; set; }

        [Required]
        public string Case { get; set; }
    }
}
