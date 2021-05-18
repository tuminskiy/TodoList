using System;

namespace TodoApi.Db.Models
{
    public enum TodoPriority
    {
        None = 0,
        Low,
        Medium,
        High
    }

    public class Todo
    {
        public int Id { get; set; }

        public TodoPriority Priority { get; set; } = TodoPriority.None;

        public string Case { get; set; }
    }
}
