using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace NMR.Variables
{
    public class GroupVariables : IEnumerable<VariableBase>
    {
        private List<VariableBase> _variableList;

        public GroupVariables(string name)
        {
            Name = name;
            _variableList = new List<VariableBase>();
        }

        public string Name { get; private set; }

        public void Add(VariableBase item)
        {
            item.Group = this;
            _variableList.Add(item);
        }

        public void Clear()
        {
            foreach (VariableBase var in _variableList)
                var.Group = null;
            _variableList.Clear();
        }

        #region IEnumerable<Variable> Members

        public IEnumerator<VariableBase> GetEnumerator()
        {
            return _variableList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _variableList.GetEnumerator();
        }

        #endregion
    }
}
