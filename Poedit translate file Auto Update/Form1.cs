using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Poedit_translate_file_Auto_Update
{
    public partial class Form1 : Form
    {
        string[] poFileList;
        static int z=0;
        static int getpot = 0;
        static string teampleFile = "";
        static string potfirst;
        static string pottwo;
        static string potthre;
        static string[,,,,] potfile;
        static string[] potkoment;
        static string[] potindex;
        static string[] potorg;
        static string[] pottrans;
        static string[,,,,,] pofile;
        static int running = 0;
        static string[] potfilelocated;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFile.Filter = "Szablon tłumaczenia|*.pot";
            openFile.Title = "Wskaż lokalizację szablonu tłumaczenia *.pot";
       
             // Show the dialog and get result.
            DialogResult result = this.openFile.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                this.textBox1.Text  = openFile.FileName;
                //App.Current.Properties["fileTranslateTeample"] = this.textBox1.Text
                if (openFile.FileName != "")
                {
                    string nullline = "";
                    this.button2.Enabled = true;
                    //rozpoczynamy wczytywanie pliku szablonu tłumaczenia
                    pictureBox1.Enabled = true;
                    pictureBox1.Visible = true;
                    label7.Visible = true;
                    label8.Visible = true;
                    timer1.Enabled = true;
                    running = 1;
                    teampleFile = textBox1.Text;
                    Thread t1 = new Thread(delegate ()
                    {
                        int idx = 0;
                       //sprawdzamy ile lini ma plik z szblonem tłumaczenia
                        z = File.ReadAllLines(teampleFile).Length;
                        int segments = ((z - 3) / 5);
                        potkoment = new string[segments];
                        potindex = new string[segments]; ;
                        potorg = new string[segments]; ;
                        pottrans = new string[segments];
                        potfilelocated = new string[segments];
                        //rozpoczynamy wczytywane pliku
                        try
                        {
                            StreamReader sr = new StreamReader(teampleFile);
                           
                            potfirst = sr.ReadLine();
                            pottwo= sr.ReadLine();
                            potthre= sr.ReadLine();
                            
                            nullline = sr.ReadLine();
                            while (!sr.EndOfStream)
                            {
                               
                                potkoment[idx] = sr.ReadLine();
                                potindex[idx] = sr.ReadLine();
                                potorg[idx] = sr.ReadLine();
                                pottrans[idx] = sr.ReadLine();
                                potfilelocated[idx] = "0";
                                nullline = sr.ReadLine();
                                getpot++;
                                idx++;
                               
                            }
       
                            sr.Close();
                            running = 0;
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                       

                    });

                    t1.Start();
                    
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
    
            this.folderBrowser.Description = "Wskaż lokalizację plików tłumaczeń *.po";
            DialogResult directchoosedlg = this.folderBrowser.ShowDialog();
            if (directchoosedlg == DialogResult.OK)
            {
                this.textBox2.Text = this.folderBrowser.SelectedPath;
                poFileList = Directory.GetFiles(textBox2.Text, "*.po");
                this.label4.Text= poFileList.Length.ToString();
                int getfile = 0;
                if (poFileList.Length>0 && openFile.FileName != "")
                {
                    //Wczytujemy pliki do pamięci
                    for (getfile =0; getfile< poFileList.Length; getfile++)
                    {
                        richTextBox1.Text += poFileList[getfile] + "\n";
                        pofile = new string[1, 1, 1, 1, 1, 1];
                    }


                    this.button3.Enabled = true;
                }
                if (poFileList.Length == 0)
                {
                    MessageBox.Show("Wpodanej lokalizacji nie znaleziono plików tłumaczeń.", "Nie znaleziono plików", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (running == 1)
            {
                label8.Text = "Wczytano: " + getpot.ToString() + " z " + ((z - 3) / 5).ToString();
            }
            else
            {
                pictureBox1.Enabled = false;
                pictureBox1.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                timer1.Enabled = false;
                label6.Text = getpot.ToString();
            }

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = potkoment[0] + "\n";
            richTextBox1.Text += potkoment[1] + "\n";
            richTextBox1.Text += potkoment[2] + "\n";
        }
    }
}
