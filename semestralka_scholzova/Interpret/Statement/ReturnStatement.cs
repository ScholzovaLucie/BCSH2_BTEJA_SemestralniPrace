using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public class ReturnStatement: Statement
    {
        private Expression expression;

        public ReturnStatement(Expression ex)
        {
            this.expression = ex;
        }

        public override void Execute(ExecutionContext ex)
        {
            expression.Evaluate(ex);
        }
    }
}
