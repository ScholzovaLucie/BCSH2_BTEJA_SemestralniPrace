namespace semestralka_scholzova.Interpret
{
    internal class IfStatement : Statement
    {
        private Statement statement;
        private Condition con;

        public IfStatement(Statement statement, Condition con)
        {
            this.statement = statement;
            this.con = con;
        }

        public override void Execute(ExecutionContext ex)
        {
            if (con.Evaluate(ex)) statement.Execute(ex);
        }
    }
}