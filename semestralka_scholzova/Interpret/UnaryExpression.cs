using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public class UnaryExpression : Expression
    {
        public Token OperatorVar;
        public Expression Right;

        public UnaryExpression(Token operatorVar, Expression right)
        {
            OperatorVar = operatorVar;
            Right = right;
        }

        public override double Evaluate(ExecutionContext ex)
        {
            if (OperatorVar.type.Equals(EnumTokens.MINUS)) return -Right.Evaluate(ex);

            return Right.Evaluate(ex);
        }
    }
}
