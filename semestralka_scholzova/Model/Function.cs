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
        private bool continuDone = false;
        private bool breakDone = false;

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
                if(vars.Contains(let))
                {
                    foreach (Let var in vars)
                    {
                        if(var.ident == let.ident)
                        {
                            var.value = parametersValues[i];
                        }
                    }
                }
                else
                {
                    let.value = parametersValues[i];
                    vars.Add(let);
                i++;
                }
                
            }
            parametersValues = new List<object>();
            foreach (Statement st in Statements)
            {

                if (st.GetType() == typeof(ReturnStatement) || st.GetType() == typeof(ContinueStatemant) || st.GetType() == typeof(BreakeStatemant))
                    {
                    st.Execute(ex);
                    if (st.GetType() == typeof(BreakeStatemant)) breakDone = true;
                    if (st.GetType() == typeof(ContinueStatemant)) continuDone = true;
                    returnDone = true;
                }
                if (returnDone != true) st.Execute(ex);
                if (st.continuDone == true) { st.continuDone = false;  continuDone = true; }
                if (st.breakDone == true) { st.breakDone = false; return; }
                if (st.returnDone == true) { returnDone = true; return; }
            }

            returnDone = false;
            breakDone = false;
            continuDone = false;
            parametersValues = new List<object>();

        }

    }
}
