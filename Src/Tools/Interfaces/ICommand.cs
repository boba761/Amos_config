using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tools.Interfaces
{
    public interface ICommand
    {
        bool IsExecute { get; }

        void Execute();
        void Receive();
    }
}
