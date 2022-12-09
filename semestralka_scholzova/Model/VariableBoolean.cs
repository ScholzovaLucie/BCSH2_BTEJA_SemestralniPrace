
namespace semestralka_scholzova.Model
{
    internal class VariableBoolean : Expression
    {
        private object literal;

        public VariableBoolean(object literal)
        {
            this.literal = literal;
        }

        public override object Evaluate(ExecutionContext ex)
        {
            return literal;
        }
    }
}