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

        public TodoPriority Priority { get; set; }

        public string Case { get; set; }
    }

    
}
