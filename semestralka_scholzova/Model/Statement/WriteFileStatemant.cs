using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace semestralka_scholzova.Model
{
    class WriteFileStatemant : Statement
    {
        private string fileName { get; set; }
        private Expression text { get; set; }
        public WriteFileStatemant(string fileName, Expression text)
        {
            this.fileName = fileName;
            this.text = text;

        }

        public override void Execute(ExecutionContext ex)
        {
            if (text != null)
            {
                try
                {
                    File.WriteAllText(fileName, text.Evaluate(ex).ToString());
                }catch(Exception) {
                    UserException exep = new UserException(ex.program, "Chyba v zápisu do souboru");
                  
                }
                
            }
                
             
           
        }
    }
}
