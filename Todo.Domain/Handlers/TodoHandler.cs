using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler :
        Notifiable, 
        IHandler<CreateTodoCommand>,
        IHandler<UpdateTodoCommand>, 
        IHandler<MarkAsDoneCommand>,
        IHandler<MarkAsUndoneCommand>
    {
        private readonly ITodoRepository _repository;
        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult Handle(CreateTodoCommand command)
        {
            // Fail fast validation
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que a sua tarefa está errada", command.Notifications);

            // Gera o TodoItem
            var todo = new TodoItem(command.Title, command.User, command.Date);

            // Salva no banco
            _repository.Create(todo);

            // Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);

        }

        public ICommandResult Handle(UpdateTodoCommand command)
        {
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que a sua tarefa está errada", command.Notifications);

            // Recupera o Todo
            var todo = _repository.GetById(command.Id, command.User);

            // Altera o titulo
            todo.UpdateTitle(command.Title);

            _repository.Update(todo);

            // Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handle(MarkAsDoneCommand command)
        {
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que a sua tarefa está errada", command.Notifications);

            // Recupera o Todo
            var todo = _repository.GetById(command.Id, command.User);

            // Altera o estado
            todo.MarkAsDone();

            _repository.Update(todo);

            // Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handle(MarkAsUndoneCommand command)
        {
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que a sua tarefa está errada", command.Notifications);

            // Recupera o Todo
            var todo = _repository.GetById(command.Id, command.User);

            // Altera o estado
            todo.MarkAsUndone();

            _repository.Update(todo);

            // Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }
    }
}