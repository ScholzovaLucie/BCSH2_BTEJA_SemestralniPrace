namespace semestralka_scholzova.Interpret
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
            throw new System.NotImplementedException();
        }
    }
}