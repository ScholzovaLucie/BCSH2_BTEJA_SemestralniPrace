using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class Function
    {
        public Token iden;

        public object type;

        public object returnvalue { get; set; }

        public List<Let> parameters;

        public List<object> parametersValues = new List<object>();

        public List<Let> vars { get; set; }

        public List<Statement> Statements { get; set; }

        public List<Function> Functions { get; set; }

        public Function(Token ident, object type)
        {
            iden = ident;
            this.type = type;
        }

        public void Execute(ExecutionContext ex)
        {
            int i = 0;
            foreach (Let let in parameters)
            {
                let.value = parametersValues[i];
                vars.Add(let);
                i++;
            }
            foreach (Statement st in Statements)
            {
                st.Execute(ex);
            }

        }

    }
}
