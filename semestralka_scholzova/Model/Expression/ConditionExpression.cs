using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    class ConditionExpression : Expression
    {
        public Token operatorVar;
        public Expression right;
        public Expression left;
        private Program pr;

        public ConditionExpression(Expression left, Token operatorVar, Expression right)
        {
            this.left = left;
            this.operatorVar = operatorVar;
            this.right = right;
        }

        public override object Evaluate(ExecutionContext ex)
        {
            pr = ex.program;
            object result = null;
            UserException exception;
            object leftnew = left.Evaluate(ex);
            object rightnew = right.Evaluate(ex);
            if (operatorVar.type == EnumTokens.LESS) return LesCompare(leftnew, rightnew);

            else if (operatorVar.type == EnumTokens.GREATER) return GreaterCompare(leftnew, rightnew);

            else if (operatorVar.type == EnumTokens.LESS_EQUAL) return LesEqualCompare(leftnew, rightnew);

            else if (operatorVar.type == EnumTokens.GREATER_EQUAL) return GreaterEqualCompare(leftnew, rightnew);

            else if (operatorVar.type == EnumTokens.EQUAL_EQUAL) return Equal(leftnew, rightnew);

            else if (operatorVar.type == EnumTokens.HASHTAG || operatorVar.type == EnumTokens.BANG_EQUAL)
            {
                if (!leftnew.Equals(rightnew)) return true;
                else return false;
            }
            else if (operatorVar.type == EnumTokens.ODD)
            {

                if (leftnew.GetType() == typeof(string)) {  exception = new UserException(pr, "Nelze porovnat"); }

                if ((int)leftnew % 2 == 0) return false;

                else return true;
            }
             exception = new UserException(pr,"Not suported");
            return false;
        }

        private bool Equal(object left, object right)
        {
            if (left.GetType() != right.GetType())
            {
                UserException exception = new UserException(pr, "Nelze porovnat");
            }
            if (left.GetType() == typeof(string))
            {
                return left.Equals(right);
            }
            else
            {
                return (int)left == (int)right;
            }

        }

        private bool LesCompare(object left, object right)
        {
            if (left.GetType() != right.GetType())
            {
                UserException exception = new UserException(pr, "Nelze porovnat");
            }
            if (left.GetType() == right.GetType())
            {
                if (left.GetType() == typeof(string)) { UserException exception = new UserException(pr, "Nelze porovnat"); }
                if (left.GetType() == typeof(int)) return (int)left < (int)right;
            }
            return false;
        }

        private bool GreaterCompare(object left, object right)
        {
            if (left.GetType() != right.GetType())
            {
                UserException exception = new UserException(pr, "Nelze porovnat");
            }
            if (left.GetType() == right.GetType())
            {
                if (left.GetType() == typeof(string)) { UserException exception = new UserException(pr, "Nelze porovnat"); }
                if (left.GetType() == typeof(int)) return (int)left > (int)right;
            }
            return false;
        }

        private bool GreaterEqualCompare(object left, object right)
        {
            if (left.GetType() != right.GetType())
            {
                UserException exception = new UserException(pr, "Nelze porovnat");
            }
            if (left.GetType() == right.GetType())
            {
                if (left.GetType() == typeof(string))
                {
                    string newleft = (string)left;
                    string newright = (string)right;
                    return newleft.Contains(newright);
                }
                if (left.GetType() == typeof(int)) return (int)left >= (int)right;
            }
            return false;
        }

        private bool LesEqualCompare(object left, object right)
        {
            if (left.GetType() != right.GetType())
            {
                UserException exception = new UserException(pr, "Nelze porovnat");
            }
            if (left.GetType() == right.GetType())
            {
                if (left.GetType() == typeof(string))
                {
                    string newleft = (string)left;
                    string newright = (string)right;
                    return newright.Contains(newleft);
                }
                if (left.GetType() == typeof(int)) return (int)left <= (int)right;
            }
            return false;
        }
    }
}
