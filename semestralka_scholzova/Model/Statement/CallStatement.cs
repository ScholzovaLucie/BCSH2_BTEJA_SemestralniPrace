using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    internal class CallStatement : Statement
    {
        private Token token;
        public List<object> parameters;

        public CallStatement(Token token)
        {
            this.token = token;
        }

        public override void Execute(ExecutionContext ex)
        {

            foreach (Function st in ex.pc.Functions)
            {
                if (st.iden.lexeme.Equals(token.lexeme))
                {
                    foreach (object obj in parameters)
                    {
                        foreach (Let let in ex.vars)
                        {
                            if (let.ident.Equals(obj))
                            {
                                st.parametersValues.Add(let.value);
                                goto find;
                            }
                        }
                        foreach (Let let in st.vars)
                        {
                            if (let.ident.Equals(obj))
                            {
                                st.parametersValues.Add(let.value);
                                goto find;
                            }
                        }

                        st.parametersValues.Add(obj);

                    find:;

                    }


                    st.Execute(ex);
                }
            }
        }
    }
}
