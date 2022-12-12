using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace semestralka_scholzova.Model
{
    public class Block
    {
        public List<Let> vars { get; set; }
        public List<Function> Functions { get; set; }
        public List<Statement> Statements { get; set; }

        public Block()
        {
            vars = new List<Let>();
            Functions = new List<Function>();
            Statements = new List<Statement>();
        }

        public string execute(string Console)
        {
            ProgramContext programContext = new ProgramContext();
            programContext.Functions = Functions;
            ExecutionContext ex = new ExecutionContext(programContext, vars, null, Console);
            if(Statements != null)
            {
                foreach (Statement st in Statements)
                {
                    if(st != null)
                    st.Execute(ex);
                }
            }
          
            return ex.Console;
        }
    }
}
