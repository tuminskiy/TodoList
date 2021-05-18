using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using TodoBlazor.Models;

namespace TodoBlazor.Pages
{
    public partial class Index : IDisposable
    {
        private Todo Todo { get; set; }

        private bool IsEditMode { get; set; }

        private IEnumerable<Todo> Todos { get; set; }

        public void Dispose()
        {
            _todoService.OnChange -= HandleChange;
        }

        protected override async Task OnInitializedAsync()
        {
            Todo = new Todo();

            IsEditMode = false;

            await _todoService.InitializeSignalR();

            Todos = await _todoService.GetTodosAsync();

            _todoService.OnChange += HandleChange;
        }

        private async void HandleChange()
        {
            Todos = await _todoService.GetTodosAsync();
            StateHasChanged();
        }

        private async void HandleSubmit()
        {
            if (IsEditMode)
            {
                await _todoService.UpdateTodoAsync(Todo);
            }
            else
            {
                await _todoService.AddTodoAsync(Todo);
            }
        }

        private void SelectTodo(int id)
        {
            Todo = Todos.FirstOrDefault(o => o.Id.Equals(id));
            IsEditMode = true;
        }
    }
}
