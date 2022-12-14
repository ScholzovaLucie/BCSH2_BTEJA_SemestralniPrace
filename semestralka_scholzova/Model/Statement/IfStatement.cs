using System.Collections.Generic;

namespace semestralka_scholzova.Model
{
    internal class IfStatement : Statement
    {
        private List<Statement> statement;
        public List<ElseIfStatement> elseifstatement;
        public ElseStatement elseStatement;
        private Condition con;

        public IfStatement(List<Statement> statement, Condition con)
        {
            this.statement = statement;
            this.con = con;
            
        }

        public IfStatement(List<Statement> statement, Condition con, List<ElseIfStatement> elseifstatemant, ElseStatement elsestatement)
        {
            this.statement = statement;
            this.con = con;
            elseifstatement = elseifstatemant;
            elseStatement = elsestatement;
        }
        public IfStatement(List<Statement> statement, Condition con, List<ElseIfStatement> elseifstatemant, ElseStatement elsestatement, List<Let> variables)
        {
            this.statement = statement;
            this.con = con;
            elseifstatement = elseifstatemant;
            elseStatement = elsestatement;
            vars = variables;
        }

        public override void Execute(ExecutionContext ex)
        {
            continuDone = false;
            returnDone = false;
            breakDone = false;
            if (con.Evaluate(ex) == true)
            {
                foreach (Statement s in statement)
                {
                    if (s.GetType() == typeof(EmptyReturnStatemant)|| s.GetType() == typeof(ContinueStatemant)|| s.GetType() == typeof(BreakeStatemant)) {
                        s.Execute(ex);
                        if(s.GetType() == typeof(BreakeStatemant)) breakDone= true;
                        if(s.GetType() == typeof(ContinueStatemant)) continuDone= true;
                        if(s.GetType()== typeof(EmptyReturnStatemant)) returnDone = true;
                    }
                    if(returnDone!=true&& breakDone!=true&& continuDone!=true) s.Execute(ex);
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
                            if (s.GetType() == typeof(ReturnStatement) || s.GetType() == typeof(ContinueStatemant) || s.GetType() == typeof(BreakeStatemant))
                            {
                                s.Execute(ex);
                                if (s.GetType() == typeof(BreakeStatemant)) breakDone = true;
                                if (s.GetType() == typeof(ContinueStatemant)) continuDone = true;
                                returnDone = true;
                            }
                            if (returnDone != true) s.Execute(ex);
                            if (s.continuDone == true) continuDone = true;
                            if (s.breakDone == true) breakDone = true;
                            goto splneno;
                        }
                    }
                }

                if (elseStatement != null)
                {

                        elseStatement.Execute(ex);
                        if (elseStatement.continuDone == true) continuDone = true;
                        if(elseStatement.breakDone == true) breakDone = true;
                }
                splneno:;
            }

        }
    }
}