using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CodeParse
{
    public partial class Form1 : Form
    {
        private const string FindFunctions = @"((public|private|protected)\s|)((virtual|override)\s|)(static\s|)(\w+\[.*\]|\w+\<.*\>|\w+)\s[A-Z]\w+\(((\w+\s\w+|\w+\[.*\]\s\w+|\w+\<.*\>\s\w+)+|)\)";
        private const string FindVariable = @"((public|private|protected)\s|)(\w+\[.*\]|\w+\<.*\>|\w+)\s[a-z]\w*(;|\s=)";
        private const string FindClass = @"class\s[A-Z]\w+";

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|cs files (*.cs)|*.cs|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        input.Text = reader.ReadToEnd();
                    }
                }
            }
        }

        private void buttonProcessFile_Click(object sender, EventArgs e)
        {
            output.Text = "";
            PrintClass();
            PrintFunctions();
            PrintVariables();
        }

        private void PrintFunctions()
        {
            output.Text += "Functions:\n";
            foreach(Match match in Regex.Matches(input.Text, FindFunctions)){
                output.Text += '\t'+ match.Value+';' + "\n";
            }
            
        }

        private void PrintVariables()
        {
            output.Text += "Variables:\n";
            foreach (Match match in Regex.Matches(input.Text, FindVariable)){
                output.Text += '\t' + Regex.Replace(match.Value, "(;|=)$", "") + "\n";
            }

        }

        private void PrintClass()
        {
            output.Text += "Classes:\n";
            foreach (Match match in Regex.Matches(input.Text, FindClass)){
                output.Text += '\t' + match.Value + "\n";
            }

        }
    }
}
