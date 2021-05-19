using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Db.Models;

namespace TodoApi.Db
{
    public class TodoDbService
    {
        private UnitOfWork _uow;

        public TodoDbService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<TodoXp> Todos()
        {
            return _uow.Query<TodoXp>();
        }

        public bool UpdateTodo(Todo todo)
        {
            var todoUpdate = _uow.Query<TodoXp>().FirstOrDefault(o => o.Id == todo.Id);

            if (todoUpdate is null)
            {
                return false;
            }

            todoUpdate.Priority = todo.Priority;
            todoUpdate.Case = todo.Case;

            _uow.CommitChanges();

            return true;
        }

        public void AddTodo(Todo todo)
        {
            var todoNew = new TodoXp(_uow)
            {
                Id = _uow.Query<TodoXp>().Select(o => o.Id).Max() + 1,
                Priority = todo.Priority,
                Case = todo.Case
            };

            _uow.CommitChanges();
        }
    }
}
