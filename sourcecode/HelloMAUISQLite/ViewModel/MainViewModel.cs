using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelloMAUI.Model;
using HelloMAUI.Services;

namespace HelloMAUI.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        readonly IToDoService _service;
        public ObservableCollection<ToDoItem> ToDoItems { get; set; } = new();

        [ObservableProperty]
        bool isRefreshing;

        public MainViewModel(IToDoService service)
        {
            _service = service;
        }

        [RelayCommand]
        async Task AddTodo()
        {
            var item = await Application.Current.MainPage.DisplayPromptAsync("To Do Item", "Enter To-Do Item.", "Add", "Cancel");
            if (item != null)
            {
                await _service.AddToDo(item);
                await Refresh();
            }
        }

        [RelayCommand]
        async Task GetAllTodoItems()
        {
            if (IsRefreshing)
                return;

            var todoItems = await _service.GetToDoItems();

            if (todoItems.Count() > 0)
                ToDoItems.Clear();

            foreach (var item in todoItems)
            {
                var todo = new ToDoItem()
                {
                    ToDoText = item.ToDoText,
                    Id = item.Id
                };

                ToDoItems.Add(todo);
            }
        }

        [RelayCommand]
        async Task Refresh()
        {
            if (!IsRefreshing)
            {
                IsRefreshing = true;
                ToDoItems.Clear();

                var todoItems = await _service.GetToDoItems();

                foreach (var item in todoItems)
                {
                    var todo = new ToDoItem()
                    {
                        ToDoText = item.ToDoText,
                        Id = item.Id
                    };

                    ToDoItems.Add(todo);
                }

                IsRefreshing = false;
            }                
        }
    }
}