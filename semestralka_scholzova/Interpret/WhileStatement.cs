namespace semestralka_scholzova.Interpret
{
    internal class WhileStatement : Statement
    {
        private Statement statement;
        private Condition con;

        public WhileStatement(Statement statement, Condition con)
        {
            this.statement = statement;
            this.con = con;
        }
        public override void Execute(ExecutionContext ex)
        {
            while (con.Evaluate(ex))
            {
                statement.Execute(ex);
            }
        }



    }
}