using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace TodoBlazor.Models
{
    public class TodoService
    {
        private readonly HttpClient _httpClient;
        private HubConnection _hubConnection;

        public event Action OnChange;

        public TodoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync()
        {
            var response = await _httpClient.GetAsync("/api/todo");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<IEnumerable<Todo>>(json, jsonOptions);
        }

        public async Task AddTodoAsync(Todo todo)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/todo", todo);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateTodoAsync(Todo todo)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/todo", todo);

            response.EnsureSuccessStatusCode();
        }

        public async Task InitializeSignalR()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{_httpClient.BaseAddress.AbsoluteUri}TodoApiHub")
                .Build();

            _hubConnection.On<Todo>("NotifyTodosChange", TodosChange);
        

            await _hubConnection.StartAsync();
        }

        private void TodosChange(Todo todo)
        {
            OnChange?.Invoke();
        }
    }
}
