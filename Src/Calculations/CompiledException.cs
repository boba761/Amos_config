using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculations
{
    class CompiledException : Exception
    {
        public CompiledException(string text)
            : base(text)
        {

        }
    }

    class ProcessorException : Exception
    {
        public ProcessorException(string text)
            : base(text)
        {

        }
    }
}
