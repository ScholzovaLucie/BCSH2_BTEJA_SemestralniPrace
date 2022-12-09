using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class ReturnStatement : Statement
    {
        private Expression expression;
        private string ident { get; set; }

        public ReturnStatement(Expression ex, string ident)
        {
            expression = ex;

            this.ident = ident;
        }

        public override void Execute(ExecutionContext ex)
        {
            foreach (Function func in ex.pc.Functions)
            {
                if (func.iden.lexeme.Equals(ident))
                {
                    func.returnvalue = expression.Evaluate(ex);
                }
            }

        }
    }
}
