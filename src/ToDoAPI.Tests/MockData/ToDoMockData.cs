using ToDoAPI.Models;

namespace ToDoAPI.Tests.MockData
{
    public class ToDoMockData
    {
        public static List<ToDoItem> GetTodos()
        {
            return new List<ToDoItem>{
             new ToDoItem{
                 Id = "663bdfd8d92503a0e90e19db",
                 Name = "Go To Shopping",
                 IsCompleted = true
             },
             new ToDoItem{
                 Id = "663bdfd8d92503a0e90e19dd",
                 Name = "Cook Food",
                 IsCompleted = true
             },
             new ToDoItem{
                 Id = "663bdfd8d92503a0e90e19df",
                 Name = "Play Games",
                 IsCompleted = false
             }
         };
        }
    }
}
