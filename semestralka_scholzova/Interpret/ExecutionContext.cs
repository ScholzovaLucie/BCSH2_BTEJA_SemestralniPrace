using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public class ExecutionContext
    {
        public ProgramContext pc { get; set; }
        public List<Let> vars { get; set; }
        public ExecutionContext global { get; set; }

        public ExecutionContext(ProgramContext pc, List<Let> vars, ExecutionContext global)
        {
            this.pc = pc;
            this.vars = vars;
            this.global = global;
        }

    }
}
