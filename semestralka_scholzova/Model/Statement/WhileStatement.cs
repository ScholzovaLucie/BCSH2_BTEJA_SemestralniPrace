

namespace semestralka_scholzova.Model
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
            while (con.Evaluate(ex) == true)
            {
                statement.Execute(ex);
                if (statement.breakDone == true)
                {
                    
                    statement.breakDone = false;
                    return;
                }
                if (statement.continuDone == true)
                {
                    statement.continuDone = false;
                    
                }
                if(statement.returnDone== true)
                {
                    returnDone = true;
                    return;
                }
                
                
            }
            statement.breakDone = false;
            statement.continuDone = false;
        }



    }
}