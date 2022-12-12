using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace semestralka_scholzova.Model
{
    public class ExecutionContext
    {
        public ProgramContext pc { get; set; }
        public List<Let> vars { get; set; }
        public ExecutionContext global { get; set; }

        public Program program;

  

        public ExecutionContext(ProgramContext pc, List<Let> vars, ExecutionContext global, Program program)
        {
            this.pc = pc;
            this.vars = vars;
            this.global = global;
            this.program = program;
        }

    }
}
