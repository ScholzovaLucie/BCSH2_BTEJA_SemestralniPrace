using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class FunctionStatement : Statement
    {
        public List<Statement> stmp;

        public List<Let> variables;
        public List<Function> functions;
        private bool returnDone = false;

        private bool brakeDone = false;

        public FunctionStatement(List<Statement> stmp, List<Let> variables, List<Function> functions)
        {
            this.stmp = stmp;
            this.variables = variables;

            this.functions = functions;
        }

        public List<Statement> GetList()
        {
            return stmp;
        }

        public override void Execute(ExecutionContext ex)
        {
            foreach (Statement s in stmp)
            {
                if (s.GetType() == typeof(ReturnStatement) || s.GetType() == typeof(ContinueStatemant) || s.GetType() == typeof(BreakeStatemant))
                {
                    
                    s.Execute(ex);
                    if (s.GetType() == typeof(BreakeStatemant))
                    {
                        brakeDone= true;
                    }
                    returnDone = true;
                }
                if (returnDone != true && continuDone != true) s.Execute(ex);
                if(s.breakDone == true)
                {
                    breakDone= true;
                    break;
                }
                if (s.continuDone == true)
                {
                    s.continuDone = false;
                    continuDone = true;
                    break;
                }
            }


        }
    }
}
