using Microsoft.Win32;
using semestralka_scholzova.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace semestralka_scholzova
{
    public class Perzistence
    {
        private string path;

        public string Import(Program program)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "ts files (*.ts)|*.ts|All files (*.*)|*.*";
            fileDialog.DefaultExt = ".ts";
            bool? result = fileDialog.ShowDialog();

            if (result == true)
            {
                path = fileDialog.FileName;
                using (StreamReader sr = new StreamReader(path))
                {
                    program.CustomConsole += "Načteno\n";
                    return sr.ReadToEnd();
                }

            }
            return "";
        }

        public void Save(string text, Program program)
        {
            if (path == null)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = false;
                fileDialog.Filter = "ts files (*.ts)|*.ts|All files (*.*)|*.*";
                fileDialog.DefaultExt = ".ts";
                bool? result = fileDialog.ShowDialog();

                path = fileDialog.FileName;
            }
            File.WriteAllText(path, string.Empty);
            FileStream sb = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(sb);
            try
            {

                writer.Write(text);

                program.CustomConsole += "Uloženo\n";
            }
            catch (Exception e)
            {
                program.CustomConsole += "Uloženo\n";
            }

            writer.Close();
        }
    }
}
