using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public class Function
    {
        public Token iden;

        public object type;

        public List<Let> parameters;

        public List<Let> vars { get; set; }

        public List<Statement> Statements { get; set; }

        public List<Function> Functions { get; set; }

        public Function(Token ident, object type)
        {
            this.iden = ident;
            this.type = type;
        }

        public void Execute(ExecutionContext ex)
        {
            foreach (Statement st in Statements)
            {
                st.Execute(ex);
            }
            foreach (Function func in Functions)
            {
                func.Execute(ex);
            }
        }

    }
}
