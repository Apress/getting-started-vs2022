using SQLite;

namespace HelloMAUI.Model
{
    [Table("todo")]
    public class ToDoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string ToDoText { get; set; }                
        
    }
}