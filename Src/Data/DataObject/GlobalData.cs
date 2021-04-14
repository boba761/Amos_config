using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using Calculations.Variables;
using Tools;

namespace Data.DataObject
{
    [XmlRoot]
    public class GlobalData
    {
        List<Variable> _variables;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public GlobalData( )
        {
            _variables = new List<Variable>( );
        }

        /// <summary>
        /// Возвращаем список переменных
        /// </summary>
        public List<Variable> Variables
        {
            get { return _variables; }
            set { _variables = value; }
        }
    }
}
