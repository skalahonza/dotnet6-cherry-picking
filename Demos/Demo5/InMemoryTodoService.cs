using System.Collections.Concurrent;

namespace Demo5
{
    public class InMemoryTodoService : ITodoService
    {
        private readonly ConcurrentDictionary<int, ToDoItem> db = new ();

        public List<ToDoItem> GetAll() =>
            db.Values
                .OrderByDescending(x => x.DueDate)
                .ToList();

        public ToDoItem? GetById(int id) =>
            db.TryGetValue(id, out var item)
                ? item
                : null;

        public ToDoItem Upsert(int id, PutToDoItem item)
        {
            var (title, dueDate) = item;
            var data = new ToDoItem(id, title, dueDate);
            return db.AddOrUpdate(id, data, (_, _) => data);
        }
    }
}
