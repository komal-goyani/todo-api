using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoAPI.Models;

namespace ToDoAPI.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IMongoCollection<ToDoItem> todoItems;

        public ToDoService(IOptions<ToDoDatabaseSettings> todoDatabaseSettings)
        {
            var mongoClient = new MongoClient(todoDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(todoDatabaseSettings.Value.DatabaseName);

            todoItems = mongoDatabase.GetCollection<ToDoItem>(todoDatabaseSettings.Value.ToDoItemsCollectionName);

        }

        public async Task<List<ToDoItem>> GetAsync()
        {
            return await todoItems.Find(_ => true).ToListAsync();
        }

        public async Task<ToDoItem?> GetAsync(string id)
        {
            return await todoItems.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(ToDoItem newToDo)
        {
            if (newToDo == null)
            {
                throw new ArgumentNullException(nameof(newToDo));
            }
            await todoItems.InsertOneAsync(newToDo);
        }

        public async Task UpdateAsync(string id, ToDoItem updatedToDo)
        {
            if (updatedToDo == null)
            {
                throw new ArgumentNullException(nameof(updatedToDo));
            }
            await todoItems.ReplaceOneAsync(x => x.Id == id, updatedToDo);
        }

        public async Task DeleteAsync(string id)
        {
            await todoItems.DeleteOneAsync(x => x.Id == id);
        }
    }
}
