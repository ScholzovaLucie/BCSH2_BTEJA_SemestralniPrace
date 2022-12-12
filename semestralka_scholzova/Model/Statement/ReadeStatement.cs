﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    internal class ReadeStatement : Statement
    {
        private Token token;

        public ReadeStatement(Token token)
        {
            this.token = token;
        }

        public override void Execute(ExecutionContext ex)
        {
            foreach (Let var in ex.vars)
            {
                if (var.ident.Equals(token.lexeme))
                {
                    ex.program.Editable = false;

                    ex.program.CustomConsole += "Zadej hodnotu \n";
                    while (true)
                    {
                        Thread.Sleep(10);
                        if (ex.program.ImportConsole != null)
                        {
                            break;
                        }

                    }


                    var.value = ex.program.ImportConsole;
                    ex.program.CustomConsole += "Konec načítání \n";
                    ex.program.Editable = true;

                    ex.program.ImportConsole = "Konzole ukončena";
                }
            }

            foreach (Function fc in ex.pc.Functions)
            {
                foreach (Let var in fc.vars)
                {
                    if (var.ident.Equals(token.lexeme))
                    {
                        ex.program.Editable = false;

                        ex.program.CustomConsole += "Zadej hodnotu \n";
                        while (true)
                        {
                            Thread.Sleep(10);
                            if (ex.program.ImportConsole != null)
                            {
                                break;
                            }

                        }


                        var.value = ex.program.ImportConsole;
                        ex.program.CustomConsole += "Konec načítání \n";
                        ex.program.Editable = true;

                        ex.program.ImportConsole = "Konzole ukončena";
                    }
                }
            }
        }


    }
}
