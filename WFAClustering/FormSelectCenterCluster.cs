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
using CsvHelper;
using System.Windows.Forms.DataVisualization.Charting;

namespace WFASeasonalCoefficients
{
    public partial class FormSelectCenterCluster : Form
    {
        public FormSelectCenterCluster()
        {
            InitializeComponent();
        }

        private static String Replace(String str)
        {
            str = str.Replace(".", ",");
            str = str.Replace(" ", "");
            if (str.IndexOf(',') > 0)
            {
                do
                    str = str.TrimEnd('0');
                while (str[str.Length - 1] == '0');
                str = str.TrimEnd('.');
                str = str.TrimEnd(',');
            }
            return str;
        }

        private static List<List<String>> LoadStockCSV(String adressFile, string delimiter = ";", int len = 0)
        {

            List<List<String>> arr = new List<List<String>>();
            using (var reader = new CsvReader(new StreamReader(adressFile, Encoding.Default)))
            {
                reader.Configuration.Delimiter = delimiter;
                object field;
                int length = 0;
                if (len != 0) length = len;
                reader.Read();
                List<String> rowHead = new List<String>();
                for (int i = 0; i < (len != 0 ? len : 100); i++)
                {
                    try
                    {
                        rowHead.Add(Replace(reader.GetField(i)));
                    }
                    catch
                    {
                        length = (len != 0 ? len : i);
                        break;
                    }
                }
                arr.Add(rowHead);
                while (reader.Read())
                {
                    List<String> row = new List<String>();
                    for (int i = 0; i < length; i++)
                    {
                        try
                        {
                            row.Add(Replace(reader.GetField(i)));
                        }
                        catch
                        { }
                    }
                    arr.Add(row);
                }
            }
            return arr;
        }



        private bool LoadBaseDataCentre()
        {
            try
            {
                this.dataGridView1.Columns.Clear();

                DataGridViewColumn dGVC = new DataGridViewColumn();

                DataGridViewCheckBoxColumn column1 = new DataGridViewCheckBoxColumn();
                column1 = new DataGridViewCheckBoxColumn(); // выделяем память для объекта
                column1.HeaderText = "Выбор";
                column1.Name = "Select";
                this.dataGridView1.Columns.Add(column1);

                for (int i = 0; i < Program.f1.FileCentreTitle.Count(); i++)
                {
                    this.dataGridView1.Columns.Add(Program.f1.FileCentreTitle[i], Program.f1.FileCentreTitle[i]);
                }

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                for (int i = 0; i < Program.f1.EndCenterCluster.Count(); i++)
                {
                    int count = (from a in Program.f1.SelectEndCenterClusterIndex where a == i select a).Count();
                    var list1 = new List<String> { (count > 0) ? "true" : "false" };
                    var l11   = new List<String> { (i+1).ToString() };
                    var list2 = (from s in Program.f1.EndCenterCluster[i] where true select s.ToString()).ToList();
                    var list3 = list1.Concat(l11.Concat(list2));
                    dataGridView1.Rows.Add(list3.ToArray());
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private void chart1_Click(object sender, EventArgs e)
        {

        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            Program.f1.SelectEndCenterClusterIndex.Clear();
            Program.f1.SelectEndCenterCluster.Clear();

            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                if (Convert.ToBoolean(this.dataGridView1[0, i].Value) == true)
                {
                    List<Double> l = new List<Double>();
                    Program.f1.SelectEndCenterClusterIndex.Add(i);
                    for (int k = 2; k < this.dataGridView1.ColumnCount; k++)
                    {
                        l.Add(Convert.ToDouble(this.dataGridView1[k, i].Value));
                    }
                    Program.f1.SelectEndCenterCluster.Add(l);
                }
            }


            FormGraph fG = new FormGraph();
            fG.Show();
            fG.labelTitle.Text = "Средние значения кластеров";

            fG.Text = fG.labelTitle.Text;

            Double minY = Double.MaxValue, maxY = Double.MinValue;

            if (Program.f1.SelectEndCenterCluster.Count > 0)
            {

                for (int colIndex = 1; colIndex < Program.f1.FileCentreTitle.Count; colIndex++)
                {
                    string seriesName = Program.f1.FileCentreTitle[colIndex];
                    fG.chart1.Series.Add(seriesName);
                    fG.chart1.Series[seriesName].ChartType = Program.f1.SelectEndCenterCluster.Count == 1 ? SeriesChartType.Point : SeriesChartType.Line ;
                    fG.chart1.Series[seriesName].BorderWidth = 2;

                    for (int rowIndex = 0; rowIndex < Program.f1.SelectEndCenterCluster.Count; rowIndex++)
                    {
                        Double y = Program.f1.SelectEndCenterCluster[rowIndex][colIndex - 1];
                        if (y > maxY) maxY = y;
                        if (y < minY) minY = y;
                        fG.chart1.Series[seriesName].Points.AddXY(Program.f1.SelectEndCenterClusterIndex[rowIndex]+1, y);
                    }
                }

                fG.chart1.ChartAreas[0].AxisY.Minimum = minY;
                fG.chart1.ChartAreas[0].AxisY.Maximum = maxY;

                fG.chart1.ChartAreas[0].AxisX.Minimum = Program.f1.SelectEndCenterClusterIndex.Min() + 1;
                fG.chart1.ChartAreas[0].AxisX.Maximum = Program.f1.SelectEndCenterClusterIndex.Max() + 1;
                fG.chart1.ChartAreas[0].AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated90; 
            }
            
        }

        private void FormSelectObject_Load(object sender, EventArgs e)
        {
            this.Text = "Выбрать кластеры";
            LoadBaseDataCentre();

            foreach (DataGridViewColumn column in this.dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                } 
            
        }
    }
}
