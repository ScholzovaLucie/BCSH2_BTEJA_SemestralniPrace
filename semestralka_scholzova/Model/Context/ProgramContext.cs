using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class ProgramContext
    {
        public List<Function> Functions { get; set; }

        public void Call(string proc, ExecutionContext context)
        {
            foreach (Function Function in Functions)
            {
                if (Function.iden.Equals(proc)) { }
            }
        }

    }
}
