﻿using ToDoAPI.Models;

namespace ToDoAPI.Services
{
    public interface IToDoService
    {
        Task CreateAsync(ToDoItem newToDo);
        Task<List<ToDoItem>> GetAsync();
        Task<ToDoItem?> GetAsync(string id);
        Task DeleteAsync(string id);
        Task UpdateAsync(string id, ToDoItem updatedToDo);
    }
}