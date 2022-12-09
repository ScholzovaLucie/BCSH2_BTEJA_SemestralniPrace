using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace semestralka_scholzova.Model
{
    public class Perzistence
    {
        private string path;

        public string Import()
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
                    return sr.ReadToEnd();
                }

            }
            return "";
        }

        public void Save(string text)
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

                MessageBox.Show("Uloženo", "Uloženo", MessageBoxButton.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Neuloženo", MessageBoxButton.OK);
            }

            writer.Close();
        }
    }
}
