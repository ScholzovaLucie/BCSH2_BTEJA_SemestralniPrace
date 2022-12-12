using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace semestralka_scholzova.Model
{
    public class ReadeFileStatemant : Statement
    {
        private string fileName { get; set; }
        private Token ident { get; set; }
        public ReadeFileStatemant(string fileName, Token ident) {
            this.fileName = fileName;
            this.ident = ident;
        
        }

        public override void Execute(ExecutionContext ex)
        {
            
            foreach (Let var in ex.vars)
            {
                if (var.ident.Equals(ident.lexeme))
                {
                    var.value = File.ReadAllText(fileName);
                }  
            }
            foreach (Function fc in ex.pc.Functions)
            {
                foreach (Let var in fc.vars)
                {
                    if (var.ident.Equals(ident.lexeme))
                    {
                        var.value = File.ReadAllText(fileName);
                    }
                }
            }
        }
    }

}
