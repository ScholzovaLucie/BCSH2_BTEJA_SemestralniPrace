using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class StringExpression : Expression
    {
        public object literal;

        public StringExpression(object literal)
        {
            this.literal = literal;
        }

        public override object Evaluate(ExecutionContext ex)
        {
            return literal;
        }
    }
}
