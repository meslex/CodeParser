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
using System.Configuration;

namespace CodeParse
{
    public partial class Form1 : Form
    {
        //private const string FindFunctions = @"((public|private|protected)\s|)((virtual|override)\s|)(static\s|)(\w+\[.*\]|\w+\<.*\>|\w+)\s[A-Z]\w+\(((\w+\s\w+|\w+\[.*\]\s\w+|\w+\<.*\>\s\w+)+|)\)";
        //private const string FindVariable = @"((public|private|protected)\s|)(\w+\[.*\]|\w+\<.*\>|\w+)\s[a-z]\w*(;|\s=)";
        //private const string FindClass = @"class\s[A-Z]\w+";

        List<DataEntity> identifiersToFind = new List<DataEntity>();

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
            PrintIdentifiers(identifiersToFind);
        }

        private void PrintIdentifiers(List<DataEntity> list)
        {
            output.Text = "";
            foreach (DataEntity dt in list)
            {
                output.Text += "\n" + dt.Key+ ":\n";
                MatchCollection mc = Regex.Matches(input.Text, dt.Value);
                if(mc.Count == 0)
                {
                    output.Text += "***Couldn't find anything***\n";
                }
                else
                {
                    foreach (Match match in mc)
                    {
                        output.Text += Regex.Replace(match.Value, @"(;|\s=)$", "") + "\n";
                    }
                }
                
            }
        }

        private void ReadConfigurationFile()
        {
            identifiersToFind.Clear();
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count == 0)
                {
                    MessageBox.Show("Something happens with configuration file. Sorry...");
                    Application.Exit();
                }
                else
                {
                    foreach(string key in appSettings.AllKeys)
                    {
                        identifiersToFind.Add(new DataEntity(key, RegularExpressionCreator.CreateRegularExpression(appSettings[key], key)));
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadConfigurationFile();
        }
    }
}
