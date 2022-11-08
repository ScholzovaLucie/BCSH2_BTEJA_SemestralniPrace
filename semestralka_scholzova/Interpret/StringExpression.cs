using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public class StringExpression : Expression
    {
        public object literal;

        public StringExpression(object literal)
        {
            this.literal = literal;
        }
        public override double Evaluate(ExecutionContext ex)
        {

            return (double)literal;
        }
    }
}
