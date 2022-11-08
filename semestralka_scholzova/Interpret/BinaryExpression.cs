using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    internal class BinaryExpression : Expression
    {
        public Token operatorVar;
        public Expression right;
        public Expression left;

        public BinaryExpression(Expression left, Token operatorVar, Expression right)
        {
            this.left = left;
            this.operatorVar = operatorVar;
            this.right = right;
        }

        public override double Evaluate(ExecutionContext ex)
        {
            double result = 0;
            if (operatorVar.type == EnumTokens.PLUS) result = left.Evaluate(ex) + right.Evaluate(ex);
            else if (operatorVar.type == EnumTokens.MINUS) result = left.Evaluate(ex) - right.Evaluate(ex);
            else if (operatorVar.type == EnumTokens.STAR) result = left.Evaluate(ex) * right.Evaluate(ex);
            else if (operatorVar.type == EnumTokens.SLASH) result = left.Evaluate(ex) / right.Evaluate(ex);
            else throw new Exception("Not suported");

            return result;
        }
    }
}
