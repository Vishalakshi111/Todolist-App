using System.ComponentModel.DataAnnotations;

namespace Todolist_App.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsDone { get; set; }

        public string? UserId { get; set; }
    }
}
