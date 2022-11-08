using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public class ElseStatement : Statement
    {
        private Statement statement;


        public ElseStatement(Statement statement)
        {
            this.statement = statement;
  
        }

        public override void Execute(ExecutionContext ex)
        {
            
        }
    }
}
