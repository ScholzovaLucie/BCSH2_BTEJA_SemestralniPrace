using System;

namespace semestralka_scholzova.Interpret
{
    public class VariableExpression : Expression
    {
        public object literal;

        public VariableExpression(object literal)
        {
            this.literal = literal;
        }

        public override double Evaluate(ExecutionContext ex)
        {
            
            return (double)literal;
        }
    }
}
