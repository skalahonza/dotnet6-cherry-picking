namespace Demo5
{
    public interface ITodoService
    {
        List<ToDoItem> GetAll();
        ToDoItem? GetById(int id);
        ToDoItem Upsert(int id, PutToDoItem item);
    }
}
