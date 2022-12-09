using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
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

        public override object Evaluate(ExecutionContext ex)
        {
            object right = Right.Evaluate(ex);
            if (OperatorVar.type.Equals(EnumTokens.MINUS))
            {
                if (right.GetType() == typeof(int)) return -(int)right;
            }
            if (OperatorVar.type.Equals(EnumTokens.PLUS))
            {
                return int.Parse(right.ToString());
            }

            return right;
        }
    }
}
