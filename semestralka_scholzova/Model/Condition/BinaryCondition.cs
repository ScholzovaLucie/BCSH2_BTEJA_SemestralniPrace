using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model

{
    internal class BinaryCondition : Condition
    {
        public Token operatorVar;
        public Expression right;
        public Expression left;

        public BinaryCondition(Expression left, Token operatorVar, Expression right)
        {
            this.left = left;
            this.operatorVar = operatorVar;
            this.right = right;
        }

        public override bool Evaluate(ExecutionContext ex)
        {
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
            throw new Exception("Not suported");
        }

        private bool Equal(object left, object right)
        {
            if (left.GetType() != right.GetType())
            {
                throw new Exception();
            }
            if (left.GetType() == typeof(string))
            {
                return left.Equals(right);
            }
            else
            {
                return left == right;
            }

        }

        private bool LesCompare(object left, object right)
        {
            if (left.GetType() != right.GetType())
            {
                throw new Exception();
            }
            if (left.GetType() == right.GetType())
            {
                if (left.GetType() == typeof(string)) throw new Exception();
                if (left.GetType() == typeof(int)) return (int)left < (int)right;
            }
            return false;
        }

        private bool GreaterCompare(object left, object right)
        {
            if (left.GetType() != right.GetType())
            {
                throw new Exception();
            }
            if (left.GetType() == right.GetType())
            {
                if (left.GetType() == typeof(string)) throw new Exception();
                if (left.GetType() == typeof(int)) return (int)left > (int)right;
            }
            return false;
        }

        private bool GreaterEqualCompare(object left, object right)
        {
            if (left.GetType() != right.GetType())
            {
                throw new Exception();
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
                throw new Exception();
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
