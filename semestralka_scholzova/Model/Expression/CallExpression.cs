using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    internal class CallExpression : Expression
    {
        public object literal;

        public CallExpression(object literal)
        {
            this.literal = literal;
        }
        public override object Evaluate(ExecutionContext ex)
        {
            foreach (Function fun in ex.pc.Functions)
            {
                if (fun.iden.lexeme.Equals(literal))
                {
                    fun.Execute(ex);
                    return fun.returnvalue;
                }

            }
            return null;
        }
    }
}
