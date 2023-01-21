using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Projekt_PP_Piotr_Dzik
{
    public partial class Form2 : Form
    {
        List<double> pomiary_double;
        public Form2(List<double> pomiary_double)
        {
            InitializeComponent();
            this.pomiary_double = pomiary_double;

            chart1.ChartAreas[0].AxisX.Title = "Numer Pomiaru";
            chart1.ChartAreas[0].AxisY.Title = "Wynik Pomiaru";

            chart1.Series[0] = new Series("Próbki");
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].Color = Color.Red;
            for (int i = 0; i < pomiary_double.Count(); i++)
                chart1.Series[0].Points.AddXY(i, pomiary_double[i]);

            chart1.Series[1] = new Series("Różnice");
            chart1.Series[1].ChartType = SeriesChartType.Line;
            chart1.Series[1].Color = Color.Blue;
            for (int i = 1; i < pomiary_double.Count(); i++)
                chart1.Series[1].Points.AddXY(i, pomiary_double[i] - pomiary_double[i - 1]);

            comboBox1.Items.Add("Czerwony");
            comboBox1.Items.Add("Niebieski");
            comboBox1.Items.Add("Zielony");
            comboBox1.Items.Add("Żółty");

            comboBox2.Items.Add("Czerwony");
            comboBox2.Items.Add("Niebieski");
            comboBox2.Items.Add("Zielony");
            comboBox2.Items.Add("Żółty");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult potwierdzenie = MessageBox.Show("Czy chcesz zamknąć wykres?", "Uwaga!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (potwierdzenie == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    chart1.Series[0].Color = Color.Red;
                    break;

                case 1:
                    chart1.Series[0].Color = Color.Blue;
                    break;

                case 2:
                    chart1.Series[0].Color = Color.Green;
                    break;

                case 3:
                    chart1.Series[0].Color = Color.Yellow;
                    break;

                default:
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    chart1.Series[1].Color = Color.Red;
                    break;

                case 1:
                    chart1.Series[1].Color = Color.Blue;
                    break;

                case 2:
                    chart1.Series[1].Color = Color.Green;
                    break;

                case 3:
                    chart1.Series[1].Color = Color.Yellow;
                    break;

                default:
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                chart1.Series[0].IsVisibleInLegend = true;
                chart1.Series[0].Enabled = true;
            }
            else
            {
                chart1.Series[0].IsVisibleInLegend = false;
                chart1.Series[0].Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                chart1.Series[1].IsVisibleInLegend = true;
                chart1.Series[1].Enabled = true;
            }
            else
            {
                chart1.Series[1].IsVisibleInLegend = false;
                chart1.Series[1].Enabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                chart1.Legends[0].Enabled = true;
            else
                chart1.Legends[0].Enabled = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            chart1.Series[0].BorderWidth = trackBar1.Value;
            chart1.Series[1].BorderWidth = trackBar1.Value;
        }

    }
}
