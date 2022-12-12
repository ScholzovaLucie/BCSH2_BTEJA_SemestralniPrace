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
                        breakDone= true;
                    }
                    if(s.GetType() == typeof(ContinueStatemant)) continuDone= true;
                    returnDone = true;
                }
                if (returnDone != true && continuDone != true) s.Execute(ex);
                if(breakDone == true)
                {
                    s.breakDone= true;
                    return;
                }
                if (continuDone == true)
                {
                    s.continuDone = true;
                    continuDone = true;
                    return;
                }
                if(returnDone == true)
                {
                    s.returnDone = true;
                    returnDone= true;
                    return;
                }
                if (s.continuDone == true){
                    s.continuDone = false;
                    return;
                }
                if (s.breakDone == true)
                {
                    breakDone= true;
                    return;
                }
                if(s.returnDone == true)
                {
                    returnDone = true;
                    return;
                }
            }


        }
    }
}
