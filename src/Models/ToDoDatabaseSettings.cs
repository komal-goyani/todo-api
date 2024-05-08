namespace ToDoAPI.Models
{
    public class ToDoDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ToDoItemsCollectionName  { get; set; } = null!;
    }
}
