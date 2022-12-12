using System.Collections.Generic;

namespace semestralka_scholzova.Model
{
    internal class IfStatement : Statement
    {
        private List<Statement> statement;
        private List<ElseIfStatement> elseifstatement;
        private Statement elseStatement;
        private Condition con;
        private bool returnDone = false;

        public IfStatement(List<Statement> statement, Condition con)
        {
            this.statement = statement;
            this.con = con;
        }

        public IfStatement(List<Statement> statement, Condition con, List<ElseIfStatement> elseifstatemant, Statement elsestatement)
        {
            this.statement = statement;
            this.con = con;
            elseifstatement = elseifstatemant;
            elseStatement = elsestatement;
        }

        public override void Execute(ExecutionContext ex)
        {
            if (con.Evaluate(ex) == true)
            {
                foreach (Statement s in statement)
                {
                    if (s.GetType() == typeof(ReturnStatement)|| s.GetType() == typeof(ContinueStatemant)|| s.GetType() == typeof(BreakeStatemant)) {
                        s.Execute(ex);
                        if(s.GetType() == typeof(BreakeStatemant)) breakDone= true;
                        if(s.GetType() == typeof(ContinueStatemant)) continuDone= true;
                        returnDone = true;
                    }
                    if(returnDone!=true) s.Execute(ex);
                }
            }
            else
            {
                if (elseifstatement.Count != 0)
                {
                    foreach (ElseIfStatement s in elseifstatement)
                    {
                        if (s.con.Evaluate(ex) == true)
                        {
                            s.Execute(ex);
                            goto splneno;
                        }
                    }
                }

                if (elseStatement != null) elseStatement.Execute(ex);
                splneno:;
            }

        }
    }
}