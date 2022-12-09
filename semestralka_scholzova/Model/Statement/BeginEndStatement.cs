using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    internal class BeginEndStatement : Statement
    {
        private List<Statement> statements;

        public BeginEndStatement(List<Statement> statements)
        {
            this.statements = statements;
        }

        public override void Execute(ExecutionContext ex)
        {
            foreach (var statement in statements)
            {
                statement.Execute(ex);
            }
        }
    }
}
