using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
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

        public override object Evaluate(ExecutionContext ex)
        {
            object result;
            object leftnew = left.Evaluate(ex);
            object rightnew = right.Evaluate(ex);
            if (operatorVar.type == EnumTokens.PLUS) result = PlusEval(leftnew, rightnew);
            else if (operatorVar.type == EnumTokens.MINUS) result = MinusEval(leftnew, rightnew);
            else if (operatorVar.type == EnumTokens.STAR) result = StarEval(leftnew, rightnew);
            else if (operatorVar.type == EnumTokens.SLASH) result = SlashEval(leftnew, rightnew);
            else throw new Exception("Not suported");

            return result;
        }
        private object PlusEval(object left, object right)
        {
            if (left.GetType() != right.GetType()) throw new Exception();
            if (left.GetType() == right.GetType())
            {
                if (left.GetType() == typeof(string))
                {
                    return string.Concat(left, right);
                }
                if (left.GetType() == typeof(int)) return (int)left + (int)right;
            }
            return null;
        }

        private object MinusEval(object left, object right)
        {
            if (left.GetType() != right.GetType()) throw new Exception();
            if (left.GetType() == right.GetType())
            {
                if (left.GetType() == typeof(string)) throw new Exception();
                if (left.GetType() == typeof(int)) return (int)left - (int)right;
            }
            return null;
        }

        private object StarEval(object left, object right)
        {
            if (left.GetType() != right.GetType())
            {
                if (left.GetType() == typeof(string))
                {
                    string returnValue = "";
                    for (int i = 0; i < (int)right; i++)
                    {
                        returnValue += (string)left;
                    }
                    return returnValue;
                }
            }
            if (left.GetType() == right.GetType())
            {
                if (left.GetType() == typeof(string)) throw new Exception();
                if (left.GetType() == typeof(int)) return (int)left * (int)right;
            }
            return null;
        }

        private object SlashEval(object left, object right)
        {
            if (left.GetType() != right.GetType()) throw new Exception();
            if (left.GetType() == right.GetType())
            {
                if (left.GetType() == typeof(string)) throw new Exception();
                if (left.GetType() == typeof(int)) return (int)left / (int)right;
            }
            return null;
        }
    }
}
