using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using elemCoef = ClassLibraryClustering.ClassElements.elemCoef;
using CsvHelper;
using System.Windows.Forms.DataVisualization.Charting;

namespace WFASeasonalCoefficients
{
    public partial class FormClustering : Form
    {
        public List<Double> listCoef;

        public const String nameOtn = "Отношение исходного ряда к ряду скользящих средних";
        public const String nameRazn = "Разница между исходным рядом и рядом скользящих средних";
        public const String nameOtnRazn = "Разница/отношение между исходным рядом и рядом скользящих средних";

        private readonly String nameType;

        public FormClustering(String _nameType)
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonV_Click(object sender, EventArgs e)
        {
            FormGraph fG = new FormGraph();
            fG.Show();
            fG.Text = this.Text;
            fG.labelTitle.Text = this.Text;

            fG.chart1.Series[0].Name = this.Text;
            int x = 0;
            Double y = 0;
            Double minY = 9999999, maxY = -9999999;

            for (int i = 0; i < this.listCoef.Count; i++)
            {
                y = listCoef[i];
                if (y > maxY)
                {
                    maxY = y;
                }

                if (y < minY)
                {
                    minY = y;
                }
                
                fG.chart1.Series[0].Points.AddXY(i + 1, y);
            }

            fG.chart1.ChartAreas[0].AxisX.Minimum = 1;
            fG.chart1.ChartAreas[0].AxisX.Maximum = this.listCoef.Count;


            fG.chart1.ChartAreas[0].AxisY.Minimum = Math.Round(minY, 4);
            fG.chart1.ChartAreas[0].AxisY.Maximum = Math.Round(maxY, 4);
        }

        private void FormSeasonCoefGraph_Load(object sender, EventArgs e)
        {
            if (Program.f1.FileData[0].Count != 2 && Program.f1.FileData[0].Count != 3) this.button1.Enabled = false;


        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.DefaultExt = "";
            saveFileDialog1.Filter = "CSV (*.csv*)|*.csv*";
            saveFileDialog1.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, Form1.dirResult);
            saveFileDialog1.Title = "Выберите документ для сохранения данных";

            saveFileDialog1.AddExtension = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName.Replace(".csv", "") + ".csv";

            if (filename != "")
            {
                List<List<String>> array = new List<List<String>>();

                List<String> l = new List<string>();

                for (int i = 0; i < this.dataGridViewClusters.Columns.Count; i++)
                {
                    String s = "";
                    switch (this.dataGridViewClusters.Columns[i].HeaderText)
                    {
                        case Form1.the_name:
                            s = Form1.the_nameLat;
                            break;
                        case Form1.the_r:
                            s = Form1.the_rLat;
                            break;
                        case Form1.the_t:
                            s = Form1.the_tLat;
                            break;
                        default:
                            s = this.dataGridViewClusters.Columns[i].HeaderText;
                            break;
                    }
                    l.Add(s);
                }
                array.Add(l);

                for (int i = 0; i < this.dataGridViewClusters.Rows.Count; i++)
                {
                    l = new List<string>();
                    for (int j = 0; j < this.dataGridViewClusters.Columns.Count; j++)
                    {
                        if (this.dataGridViewClusters[j, i].Value == null) break;
                        l.Add(this.dataGridViewClusters[j, i].Value.ToString());
                    }
                    array.Add(l);
                }

                Form1.SaveStockCSV(filename, array);


                MessageBox.Show("Файл сохранен");
            }
        }

        private void FormSeasonCoefGraph_Shown(object sender, EventArgs e)
        {
            
        }

        private void buttonSeasonFactor_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.radioButtonAllCluster.Checked)
            {
                FormGraph fG = new FormGraph();
                fG.Show();
                fG.Text = this.Text;

                Double minY = Double.MaxValue, maxY = Double.MinValue;

                fG.labelTitle.Text = "Средние значения кластеров";

                for (int colIndex = 1; colIndex < dataGridViewEndCenter.Columns.Count; colIndex++)
                {
                    string seriesName = dataGridViewEndCenter.Columns[colIndex].Name;
                    fG.chart1.Series.Add(seriesName);
                    fG.chart1.Series[seriesName].ChartType = SeriesChartType.Line;
                    fG.chart1.Series[seriesName].BorderWidth = 2;

                    for (int rowIndex = 0; rowIndex < dataGridViewEndCenter.RowCount; rowIndex++)
                    {
                        Double y = Convert.ToDouble(dataGridViewEndCenter[colIndex, rowIndex].Value);
                        if (y > maxY) maxY = y;
                        if (y < minY) minY = y;
                        fG.chart1.Series[seriesName].Points.AddXY(rowIndex + 1, y);
                    }
                }

                fG.chart1.ChartAreas[0].AxisY.Minimum = minY;
                fG.chart1.ChartAreas[0].AxisY.Maximum = maxY;

                fG.chart1.ChartAreas[0].AxisX.Minimum = 1;
                fG.chart1.ChartAreas[0].AxisX.Maximum = dataGridViewEndCenter.RowCount;
                fG.chart1.ChartAreas[0].AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated90;
            }
            else if (this.radioButtonSelCluster.Checked)
            {
                FormSelectCenterCluster fSCC = new FormSelectCenterCluster();
                fSCC.Show();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.f1.FileData[0].Count == 2)
            {
                FormGraph fG = new FormGraph();
                fG.Show();
                fG.Text = this.Text;

                Double minY = Double.MaxValue, maxY = Double.MinValue;
                Double minX = Double.MaxValue, maxX = Double.MinValue;
                Double x = 0, y = 0;

                fG.labelTitle.Text = "Кластеры в 2-х мерном пространстве";

                //cluster centers
                string seriesNameC = "Cluster centers".ToString();
                fG.chart1.Series.Add(seriesNameC);
                fG.chart1.Series[seriesNameC].ChartType = SeriesChartType.Point;
                fG.chart1.Series[seriesNameC].MarkerSize = 7;

                for (int clusterIndex = 0; clusterIndex < Program.f1.EndCenterCluster.Count; clusterIndex++)
                {
                    x = Program.f1.EndCenterCluster[clusterIndex][0];
                    y = Program.f1.EndCenterCluster[clusterIndex][1];

                    if (x > maxX) maxX = x;
                    if (x < minX) minX = x;

                    if (y > maxY) maxY = y;
                    if (y < minY) minY = y;

                    fG.chart1.Series[seriesNameC].Points.AddXY(x, y);

                }


                for (int clusterIndex = 0; clusterIndex < Program.f1.countClusters; clusterIndex++)
                {
                    string seriesName = (clusterIndex + 1).ToString();
                    fG.chart1.Series.Add(seriesName);
                    fG.chart1.Series[seriesName].ChartType = SeriesChartType.Point;
                    fG.chart1.Series[seriesName].MarkerSize = 7;

                    for (int i = 0; i < this.dataGridViewClusters.RowCount; i++)
                    {
                        if (Convert.ToInt32(dataGridViewClusters[2, i].Value) == clusterIndex + 1)
                        {
                            x = Program.f1.FileData[i][0];
                            y = Program.f1.FileData[i][1];

                            if (x > maxX) maxX = x;
                            if (x < minX) minX = x;

                            if (y > maxY) maxY = y;
                            if (y < minY) minY = y;

                            fG.chart1.Series[seriesName].Points.AddXY(x, y);
                        }
                    }

                    
                }

                


                fG.chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                fG.chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

                fG.chart1.ChartAreas[0].BackColor = Color.White;
                fG.chart1.ChartAreas[0].BackSecondaryColor = Color.White;
                fG.chart1.ChartAreas[0].BorderColor = Color.White;

                fG.chart1.ChartAreas[0].AxisY.Minimum = minY;
                fG.chart1.ChartAreas[0].AxisY.Maximum = maxY;
                fG.chart1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;

                fG.chart1.ChartAreas[0].AxisX.Minimum = minX;
                fG.chart1.ChartAreas[0].AxisX.Maximum = maxX;
                fG.chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                fG.chart1.ChartAreas[0].AxisX.Interval = maxX % minX;
                fG.chart1.ChartAreas[0].AxisX.InterlacedColor = Color.White;
                fG.chart1.ChartAreas[0].AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated90;


            }

            //http://helpcentral.componentone.com/NetHelp/SpreadNet7/WF/spwin-make-xyzplot.html
            if (Program.f1.FileData[0].Count == 3)
            {
                FormGraph3d fG3d = new FormGraph3d();
                fG3d.Show();
            }

        }
    }
}
