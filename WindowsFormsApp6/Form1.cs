using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            FileStream datovytok = new FileStream("Cisla.dat", FileMode.OpenOrCreate, FileAccess.Write);
            OpenFileDialog otevirac = new OpenFileDialog();
            otevirac.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            if (otevirac.ShowDialog() == DialogResult.OK)
            {
                double[] poledesetinnych = new double[3];
                int pocitadlo = 0;
                StreamReader ctenar = new StreamReader("textak.txt");
                while (!ctenar.EndOfStream)
                {
                    string[] pole = ctenar.ReadLine().Split(';');
                    int max = 0;
                    foreach (string slovo in pole)
                    {
                        int pocetpismen = 0;
                        foreach (char znak in slovo)
                        {
                            pocetpismen++;
                            if (pocetpismen > max)
                            {
                                max = pocetpismen;
                            }
                        }
                    }
                    double desetinne = ((double)max) / 10;
                    poledesetinnych[pocitadlo] = desetinne;
                    pocitadlo++;
                }
                
                BinaryWriter zapisovak = new BinaryWriter(datovytok);
                foreach (double cislo in poledesetinnych)
                {
                    zapisovak.Write(cislo);
                }
                ctenar.Close();
                datovytok.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            listBox1.Items.Clear();
            FileStream datovytok = new FileStream("Cisla.dat", FileMode.OpenOrCreate, FileAccess.Read);
            BinaryReader ctenar = new BinaryReader(datovytok);
            ctenar.BaseStream.Position = 0;
            while (ctenar.BaseStream.Position < ctenar.BaseStream.Length)
            {
                listBox1.Items.Add(ctenar.ReadDouble());
            }
            datovytok.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            FileStream datovytok = new FileStream("Cisla.dat", FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter zapisovak = new BinaryWriter(datovytok);
            zapisovak.BaseStream.Position = 0;
            foreach (double cislo in listBox1.Items)
            {
                if (cislo < 1)
                {
                    zapisovak.Write(cislo * 10);
                }
                else
                {
                    zapisovak.Write(cislo);
                }
            }
            datovytok.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileStream datovytok = new FileStream("Cisla.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);           
            BinaryReader ctenar = new BinaryReader(datovytok);
            ctenar.BaseStream.Position = 0;
            double soucet = 0;
            int pocet = 0;
            while (ctenar.BaseStream.Position < ctenar.BaseStream.Length)
            {
                double cislo = ctenar.ReadDouble();
                if (cislo > 2)
                {
                    soucet += cislo;
                    pocet++;
                }
            }
            BinaryWriter zapisovak = new BinaryWriter(datovytok);
            zapisovak.BaseStream.Position = zapisovak.BaseStream.Length;            
            zapisovak.Write(soucet / (double)pocet);
            datovytok.Close();
        }
    }
}