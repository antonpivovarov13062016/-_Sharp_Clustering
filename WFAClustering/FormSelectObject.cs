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

namespace WFASeasonalCoefficients
{
    public partial class FormSelectObject : Form
    {
        public FormSelectObject()
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

        private bool LoadBaseData()
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
                
                for (int i = 0; i < Program.f1.FileDataTitle.Count(); i++)
                {
                    this.dataGridView1.Columns.Add(Program.f1.FileDataTitle[i], Program.f1.FileDataTitle[i]);
                }

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                for (int i = 0; i < Program.f1.FileData.Count(); i++)
                {
                    int count = (from a in Program.f1.CentreObjectSelectIndex where a == i select a).Count();
                    var list1 = new List<String> { (count > 0) ? "true" : "false" };
                    var l11 = new List<String> { Program.f1.FileDataNumber[i] };
                    var l12 = new List<String> { Program.f1.FileDataName[i] };
                    var list2 = (from s in Program.f1.FileData[i] where true select s.ToString()).ToList();
                    var list3 = list1.Concat(l11.Concat(l12.Concat(list2)));
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
            Program.f1.CentreObjectSelectIndex.Clear();
            Program.f1.CentreObjectSelect.Clear();

            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                if (Convert.ToBoolean(this.dataGridView1[0, i].Value) == true)
                {
                    List<Double> l = new List<Double>();
                    Program.f1.CentreObjectSelectIndex.Add(i);
                    for (int k = 3; k < this.dataGridView1.ColumnCount; k++)
                    {
                        l.Add(Convert.ToDouble(this.dataGridView1[k, i].Value));
                    }
                    Program.f1.CentreObjectSelect.Add(l);
                }
            }
            Program.f1.labelSelectedObject.Text = "Выбрано " + Program.f1.CentreObjectSelectIndex.Count + " объекта(ов)";
            if (Program.f1.CentreObjectSelectIndex.Count == 1) Program.f1.labelSelectedObject.Text = "Выбран 1 объект";

            this.Close();
        }

        private void FormSelectObject_Load(object sender, EventArgs e)
        {
            LoadBaseData();

            foreach (DataGridViewColumn column in this.dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                } 
            
        }
    }
}
