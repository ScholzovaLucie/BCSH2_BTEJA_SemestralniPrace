using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    internal class CallExpression : Expression
    {
        public object literal;

        public CallExpression(object literal)
        {
            this.literal = literal;
        }
        public override double Evaluate(ExecutionContext ex)
        {
            throw new NotImplementedException();
        }
    }
}
