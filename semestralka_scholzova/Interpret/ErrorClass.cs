using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace semestralka_scholzova.Interpret
{
    public class ErrorClass : Exception
    {
        public ErrorClass(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK);
        }

        public ErrorClass()
        {
            MessageBox.Show("Unjnown error", "Error", MessageBoxButton.OK);
        }




    }
}
