using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class Function
    {
        public string iden;

        public object type;

        public object returnvalue { get; set; }

        public List<Let> parameters;

        private bool returnDone = false;

        public List<object> parametersValues = new List<object>();

        public List<Let> vars { get; set; }

        public List<Statement> Statements { get; set; }

        public List<Function> Functions { get; set; }

        public Function(string ident, object type)
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

                if (st.GetType() == typeof(ReturnStatement) || st.GetType() == typeof(ContinueStatemant) || st.GetType() == typeof(BreakeStatemant))
                
                    {
                    st.Execute(ex);
                    returnDone = true;
                }
                if (returnDone != true) st.Execute(ex);
            }

        }

    }
}
