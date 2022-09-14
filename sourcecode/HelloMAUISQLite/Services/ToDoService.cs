using HelloMAUI.Model;
using SQLite;

namespace HelloMAUI.Services
{
    public class ToDoService : IToDoService
    {

        private SQLiteAsyncConnection _db;        
        readonly string _dbPath;

        public ToDoService()
        {
            _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "mydb.db");
        }

        public async Task Init()
        {            
            if (_db != null)
                return;

            _db = new SQLiteAsyncConnection(_dbPath);
            _ = await _db.CreateTableAsync<ToDoItem>();
        }

        public async Task AddToDo(string todoText)
        {
            await Init();

            var todoItem = new ToDoItem
            {
                ToDoText = todoText
            };

            var result = await _db.InsertAsync(todoItem);
        }

        public async Task<IEnumerable<ToDoItem>> GetToDoItems()
        {
            await Init();
            return await _db.Table<ToDoItem>().ToListAsync();
        }
    }
}
