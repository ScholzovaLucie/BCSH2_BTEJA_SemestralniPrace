using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public class Block
    {
        public List<Let> vars { get; set; }
        public List<Function> Functions { get; set; }
        public List<Statement> Statements { get; set; }

        public Block() { 
            vars = new List<Let>();
            Functions = new List<Function>();
            Statements = new List<Statement>();
        }

        public void execute()
        {
            ProgramContext programContext = new ProgramContext();
            programContext.Functions = Functions;
            ExecutionContext ex = new ExecutionContext(programContext, vars, null);
           // stmt.Execute(ex);
        }

        public void execute(ExecutionContext ex)
        {
            ProgramContext programContext = new ProgramContext();
            programContext.Functions = Functions;
            ExecutionContext newex = new ExecutionContext(programContext, vars, ex);
            //stmt.Execute(newex);
        }
    }
}
