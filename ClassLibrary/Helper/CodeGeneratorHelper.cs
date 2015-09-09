using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helper
{
    public class CodeGeneratorHelper
    {
        static public string returnCode(string prefix, int lastValue)
        {
            return (prefix + ++lastValue).ToString();
        }
    }
}
