using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Amos.TypeFiles;
using Amos.TypeFiles.FileBuilders;
using Calculations.Variables;

namespace Amos.Data
{
    public class GlobalData //: IData
    {
        List<GlobalVariable> _variables;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public GlobalData( )
        {
            _variables = new List<GlobalVariable>( );
        }

        /// <summary>
        /// Возвращаем список переменных
        /// </summary>
        public List<GlobalVariable> Variables
        {
            get { return _variables; }
            set { _variables = value; }
        }

        /// <summary>
        /// Выполнения действий необходимых для работы объекта
        /// </summary>
        public void Create( )
        {
            try
            {
                LoadVariable( );
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, Program.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        /// <summary>
        /// Клонирует текущий объект
        /// </summary>
        public void Close( )
        {
        }

        /// <summary>
        /// Обновление состояния объекта
        /// </summary>
        public void Refresh( )
        {
        }

        /// <summary>
        /// Загрузка переменных из файла
        /// </summary>
        public void LoadVariable( )
        {
            if ( File.Exists( Program.GlobalVariableConfig ) == false )
                SaveVariable( );
            ( new FileConverter( null, new GlobalVariableFileBuilder( ) ) ).Load( Program.GlobalVariableConfig );
        }

        /// <summary>
        /// Сохранение переменных в файл.
        /// </summary>
        public void SaveVariable( )
        {
            ( new FileConverter( null, new GlobalVariableFileBuilder( ) ) ).Save( Program.GlobalVariableConfig );
        }
    }
}
