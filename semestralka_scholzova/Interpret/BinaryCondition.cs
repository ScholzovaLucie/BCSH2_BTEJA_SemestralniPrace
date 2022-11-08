using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret

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

        public override Boolean Evaluate(ExecutionContext ex)
        {
            if(operatorVar.type == EnumTokens.LESS) 
            {
                if (left.Evaluate(ex) < right.Evaluate(ex)) return true;
                else return false;
            }
            else if(operatorVar.type == EnumTokens.GREATER) 
            {
                if (left.Evaluate(ex) > right.Evaluate(ex)) return true;
                else return false;
            }
            else if(operatorVar.type == EnumTokens.LESS_EQUAL) 
            {
                if (left.Evaluate(ex) <= right.Evaluate(ex)) return true;
                else return false;
            }
            else if (operatorVar.type == EnumTokens.GREATER_EQUAL)
            {
                if (left.Evaluate(ex) >= right.Evaluate(ex)) return true;
                else return false;
            }
            else if (operatorVar.type == EnumTokens.EQUAL)
            {
                if (left.Evaluate(ex) == right.Evaluate(ex)) return true;
                else return false;
            }
            else if(operatorVar.type == EnumTokens.HASHTAG || operatorVar.type == EnumTokens.BANG_EQUAL)
            {
                if (left.Evaluate(ex) != right.Evaluate(ex)) return true;
                else return false;
            }
            throw new Exception("Not suported");
        }
    }
}
