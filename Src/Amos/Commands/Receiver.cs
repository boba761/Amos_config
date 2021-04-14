using System.Collections.Generic;
using Tools.Interfaces;

namespace Amos.Commands
{
    public abstract class Receiver
    {
        private List<ICommand> _commands;
        private int _curretCommand;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        protected Receiver()
        {
            _curretCommand = 0;
            _commands = new List<ICommand>();
        }

        /// <summary>
        /// Возвращает может ли быть произведён откат команды
        /// </summary>
        public bool IsUndo { get { return _curretCommand > 0; } }

        /// <summary>
        /// Возвращает может ли быть произведён возврат откатившейся команды
        /// </summary>
        public bool IsRedo { get { return _curretCommand < _commands.Count; } }

        /// <summary>
        /// Обработчик события выполнения или отката команды
        /// </summary>
        protected abstract void OnCommandExecute( );

        /// <summary>
        /// Выполнение команды
        /// </summary>
        public void Run( ICommand command )
        {
            if ( command.IsExecute == false )
                return;
            command.Execute();
            if ( _curretCommand != _commands.Count )
                _commands.RemoveRange( _curretCommand, _commands.Count - _curretCommand );
            _commands.Add( command );
            _curretCommand = _commands.Count;
            OnCommandExecute( );
        }

        /// <summary>
        /// Добавляет команду в список выполненых
        /// </summary>
        public void Add( ICommand command )
        {
            if ( command.IsExecute == false )
                return;
            if ( _curretCommand != _commands.Count )
                _commands.RemoveRange( _curretCommand, _commands.Count - _curretCommand );
            _commands.Add( command );
            _curretCommand = _commands.Count;
            OnCommandExecute( );
        }

        /// <summary>
        /// Откат предидущей команды
        /// </summary>
        public void Undo()
        {
            ICommand command = _commands[_curretCommand - 1];
            command.Receive();
            _curretCommand--;
            OnCommandExecute( );
        }

        /// <summary>
        /// Возврат откатившейся команды
        /// </summary>
        public void Redo( )
        {
            ICommand command = _commands[_curretCommand];
            command.Execute( );
            _curretCommand++;
            OnCommandExecute( );
        }
    }
}
