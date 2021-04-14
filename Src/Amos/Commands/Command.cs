using System.Collections.Generic;
using Tools.Interfaces;

namespace Amos.Commands
{
    abstract class Command : ICommand, ICommandList
    {
        private List<ICommand> _commands;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="document">Документ над которым выполняется команда</param>
        public Command( Document document )
        {
            Document = document;
            _commands = new List<ICommand>();
        }

        #region Члены ICommand

        /// <summary>
        /// Возврашает состояние может ли команда исполнится
        /// </summary>
        public virtual bool IsExecute { get { return true; } }

        /// <summary>
        /// Запускает на выполнение команду
        /// </summary>
        public virtual void Execute()
        {
            foreach ( ICommand command in _commands )
                command.Execute();
        }

        /// <summary>
        /// откатывает выполненую команду
        /// </summary>
        public virtual void Receive()
        {
            for ( int i = _commands.Count - 1; i >= 0; i-- )
                _commands[i].Receive();
        }

        #endregion

        #region Члены ICommandList

        /// <summary>
        /// Возвращает объект документа для команды
        /// </summary>
        public Document Document { get; private set; }

        /// <summary>
        /// Добавляет подкоманду для составной команды
        /// </summary>
        /// <param name="command">Объект подкоманды</param>
        public void Add( ICommand command )
        {
            _commands.Add( command );
        }

        #endregion
    }
}
