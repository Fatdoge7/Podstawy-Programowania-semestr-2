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

namespace Projekt_PP_Piotr_Dzik
{
    public partial class Form1 : Form
    {
        OpenFileDialog otworz;
        SaveFileDialog zapis;
        double srednia = 0, wariancja = 0, min = 0 , max = 0;
        List<double> pomiary_double;

        public Form1()
        {
            InitializeComponent();
            otworz = new OpenFileDialog();
            zapis = new SaveFileDialog();
        }

        private void clean()
        {
            srednia = 0;
            wariancja = 0;
            min = 0;
            max = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            label14.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            otworz.Filter = "txt files (*.txt)|*.txt";
            DialogResult result = otworz.ShowDialog();
            if (result == DialogResult.OK)
            {
                clean();
                string path = otworz.FileName;
                string data = File.ReadAllText(path);
                textBox1.Text = data;
                string[] pomiary = data.Split(';');
                pomiary_double = new List<double>();
                for (int i = 0; i < pomiary.Length; i++)
                {
                    if (double.TryParse(pomiary[i], out _))
                        pomiary_double.Add(Convert.ToDouble(pomiary[i]));
                }
                srednia = (pomiary_double.Sum() / pomiary_double.Count);
                textBox2.Text = Math.Round(srednia, 7).ToString();
                min = pomiary_double.AsQueryable().Min();
                textBox3.Text = min.ToString();
                max = pomiary_double.AsQueryable().Max();
                textBox5.Text = max.ToString();
                double suma_kwadrat = 0;
                for (int i = 0; i < pomiary_double.Count; i++)
                    suma_kwadrat += Math.Pow((pomiary_double[i] - srednia), 2);
                wariancja = suma_kwadrat / pomiary_double.Count;
                textBox4.Text = Math.Round(wariancja, 7).ToString();
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                MessageBox.Show("Błędne wczytanie pomiarów", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Hand); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 wykres = new Form2(pomiary_double);
            wykres.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clean();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            zapis.Filter = "txt files (*.txt)|*.txt";
            DialogResult result = zapis.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = zapis.FileName;
                string kontent = "Wartość średnia: " + Math.Round(srednia, 7) + "\nWariancja: " + Math.Round(wariancja, 7) +
                    "\nWartość minimalna: " + min + "\nWartość maksymalna: " + max;
                File.WriteAllText(path, kontent);
                MessageBox.Show("Pomyślne zapisanie wyników", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label14.Visible = true;
            }
            else
            {
                MessageBox.Show("Niepoprawne zapisanie wyników", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult potwierdzenie = MessageBox.Show("Czy chcesz zamknąć program?", "Uwaga!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (potwierdzenie == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
