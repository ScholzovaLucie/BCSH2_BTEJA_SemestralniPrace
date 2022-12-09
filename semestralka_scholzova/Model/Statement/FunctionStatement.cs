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
                s.Execute(ex);
            }


        }
    }
}
