using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests
{
    [TestClass]
    public class CreateTodoHandlerTests
    {
        private readonly CreateTodoCommand _commandInvalid = new CreateTodoCommand("", "", DateTime.Now);
        private readonly CreateTodoCommand _commandValid = new CreateTodoCommand("Titulo da tarefa", "Victor", DateTime.Now);
        private readonly TodoHandler _handler = new TodoHandler(new FakeTodoRepository());
        private GenericCommandResult _result = new GenericCommandResult();

        [TestMethod]
        public void Dado_um_comando_invalido_deve_interromper_execucao()
        {
            _result = (GenericCommandResult)_handler.Handle(_commandInvalid);

            Assert.AreEqual(_result.Success, false);
        }

        [TestMethod]
        public void Dado_um_comando_valido_deve_criar_tarefa()
        {
            _result = (GenericCommandResult)_handler.Handle(_commandValid);

            Assert.AreEqual(_result.Success, true);
        }
    }
}