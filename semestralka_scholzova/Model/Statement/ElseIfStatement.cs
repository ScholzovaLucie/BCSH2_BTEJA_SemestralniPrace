using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class ElseIfStatement : Statement
    {
        private Statement statement;
        public Condition con;

        public ElseIfStatement(Statement statement, Condition con)
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
