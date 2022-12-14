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
        private Program pr;

        public BinaryExpression(Expression left, Token operatorVar, Expression right)
        {
            this.left = left;
            this.operatorVar = operatorVar;
            this.right = right;
        }

        public override object Evaluate(ExecutionContext ex)
        {
            pr = ex.program;
            object result = null;
            object leftnew = left.Evaluate(ex);
            object rightnew = right.Evaluate(ex);
            if (operatorVar.type == EnumTokens.PLUS) result = PlusEval(leftnew, rightnew);
            else if (operatorVar.type == EnumTokens.MINUS) result = MinusEval(leftnew, rightnew);
            else if (operatorVar.type == EnumTokens.STAR) result = StarEval(leftnew, rightnew);
            else if (operatorVar.type == EnumTokens.SLASH) result = SlashEval(leftnew, rightnew);
            else if (operatorVar.type == EnumTokens.ODD) result = OddEval(leftnew,rightnew);
            else { UserException exception = new UserException(pr,"Not suported"); }

            return result;
        }

        private object OddEval(object left, object right)
        {
            if(left.GetType() == typeof(int))
            {
                return (int)left%(int)right;
            }
            return null;
        }

        private object PlusEval(object left, object right)
        {
            if (left.GetType() != right.GetType()) { UserException exception = new UserException(pr,"Nelze sečíst"); }
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
            if (left.GetType() != right.GetType()) { UserException exception = new UserException(pr, "Nelze odečíst"); }
            if (left.GetType() == right.GetType())
            {
                if (left.GetType() == typeof(string)) { UserException exception = new UserException(pr, "Nelze odečíst"); }
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
                if (left.GetType() == typeof(string)) { UserException exception = new UserException(pr, "Nelze Vynásobit"); }
                if (left.GetType() == typeof(int)) return (int)left * (int)right;
            }
            return null;
        }

        private object SlashEval(object left, object right)
        {
            if (left.GetType() != right.GetType()) { UserException exception = new UserException(pr, "Nelze vydělit"); }
            if (left.GetType() == right.GetType())
            {
                if (left.GetType() == typeof(string)) { UserException exception = new UserException(pr, "Nelze vydělit"); }
                if (left.GetType() == typeof(int)) { 
                    if((int)right == 0)
                    {
                        UserException exception = new UserException(pr, "Nelze dělit nulou");
                        return null;
                    }
                    return (int)left / (int)right; 
                }
            }
            return null;
        }
    }
}
