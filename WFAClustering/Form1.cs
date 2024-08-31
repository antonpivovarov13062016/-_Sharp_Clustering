using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using ClassLibraryClustering;
using elem = ClassLibraryClustering.ClassElements.elemArr;
using elemCoef = ClassLibraryClustering.ClassElements.elemCoef;
using CsvHelper;
using System.IO;
using System.Security.Cryptography;


namespace WFASeasonalCoefficients
{
    public partial class Form1 : Form
    {
        AboutBox1 aboutBox = new AboutBox1();
        public static readonly String nameApp = "Программное обеспечение для решения задач кластерного анализа";
        public static readonly String constAdditive = "Аддитивная";
        public static readonly String constMultiply = "Мультипликативная";
        public const String the_name = "Дата наблюдения";
        public const String the_r = "Исходный ряд";
        public const String the_t = "Порядковый номер наблюдения";
        public const String dirResult = @"Work\Result";
        public const String dirData = @"Work\Data";
        public const String dirCentre = @"Work\Centre";
        public const String dirSettings = @"Work\Setting";

        public List<String> FileCentreTitle = new List<string>(); //Заголовок 
        public List<String> FileDataTitle = new List<string>(); //Заголовок 

        public List<String> FileDataNumber = new List<string>(); // #
        public List<String> FileDataName = new List<string>(); // Имя объекта
        
        public List<List<Double>> FileCentre = new List<List<double>>();
        public List<List<Double>> EndCenterCluster = new List<List<double>>();

        public List<List<Double>> SelectEndCenterCluster = new List<List<double>>(); // Для вывода графика средних
        public List<int> SelectEndCenterClusterIndex = new List<int>(); // Для вывода графика средних

        public List<List<Double>> CentreObjectSelect = new List<List<double>>();
        public List<int> CentreObjectSelectIndex = new List<int>();

        //        public List<List<Double>> DataObjectSelect = new List<List<double>>();
        //        public List<int> DataObjectSelectIndex = new List<int>();

        public List<List<Double>> FileData = new List<List<double>>();

        public List<List<Double>> FileCentreInverse = new List<List<double>>();
        public List<List<Double>> FileDataInverse = new List<List<double>>();

        public const String the_nameLat = "NameSeries";
        public const String the_rLat = "Series";
        public const String the_tLat = "T";
        
        public int posIndexStr;
        public int posDate;
        public int posT;
        public int posData;

        public int countClusters;
        public List<long> indexCluster = new List<long>(); // 


        CultureInfo provider = CultureInfo.InvariantCulture;
        //Аддитивная
        //Мультипликативная
        //
        public Form1()
        {
            InitializeComponent();
            this.Text = nameApp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            aboutBox.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox = new AboutBox1();
            aboutBox.Show();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            aboutBox.Activate();
            LoadSetting(dirSettings + @"\default.txt");
            LoadBasicFile(this.textBoxFileOpen.Text);
            LoadFileCenter(this.textBoxFileCenter.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void тестовоеЗаполнениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ClearTable()
        {
        }

        private void ClearTab(int toPosClear = 1)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            ClearTab();
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearTable();
        }

        private void buttonBuildGraph_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void button1_Click_1(object sender, EventArgs e)
        {

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

        public static bool SaveStockCSV(String adressFile, List<List<String>> arr, String delimiter = ";")
        {
            try
            {
                using (TextWriter writer = new StreamWriter(adressFile, false, Encoding.Default))
                {
                    using (var csv = new CsvWriter(writer))
                    {
                        csv.Configuration.Delimiter = delimiter;
                        csv.Configuration.HasHeaderRecord = true;
                        for (int i = 0; i < arr.Count(); i++)
                        {
                            for (int j = 0; j < arr[i].Count(); j++)
                            {
                                csv.WriteField(arr[i][j]);
                            }
                            csv.NextRecord();
                        }
                        csv.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private Double Rand(Double max)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                // Buffer storage.
                byte[] data = new byte[8];
                    // Fill buffer.
                rng.GetBytes(data);
                    // Convert to int 32.
                double value = BitConverter.ToDouble(data, 0) % max;
                return value;
            }
        }

        public static Double GetRandomDoubleBetween(Double minValue, Double maxValue)
        {
            if (minValue > maxValue) throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;
            
            var rng = new RNGCryptoServiceProvider();
            var uint32Buffer = new byte[8];
            Double diff = maxValue - minValue;

            while (true)
            {
                rng.GetBytes(uint32Buffer);
                int rand = BitConverter.ToInt32(uint32Buffer, 0);
                int randFractions = BitConverter.ToInt32(uint32Buffer, 0);
                Double randFractionsDouble = Convert.ToDouble(randFractions);
                while ((int) randFractionsDouble != 0)
                {
                    randFractionsDouble *= 0.1;
                }

                Double randDouble = rand + randFractionsDouble;
                const Double max = Double.MaxValue;
                Double remainder = max % diff;

                Double result = (Double)(minValue + (randDouble % diff));
                if (randDouble < max - remainder)
                {
                    if (result >= minValue && result <= maxValue)
                    {
                        return result;
                    }
                    
                }
            }
        }

        public static int GetRandomIntBetween(int minValue, int maxValue)
        {
            if (minValue > maxValue) throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;

            var rng = new RNGCryptoServiceProvider();
            var uint32Buffer = new byte[4];
            long diff = maxValue - minValue;
            while (true)
            {
                rng.GetBytes(uint32Buffer);
                uint rand = BitConverter.ToUInt32(uint32Buffer, 0);
                const int max = int.MaxValue;

                long remainder = max % diff;
                int result = (int)(minValue + (rand % diff));
                if (rand < max - remainder)
                {
                    if (result >= minValue && result <= maxValue)
                    {
                        return result;
                    }
                }
            }
        }

        private List<List<Double>> RandomCentre(int rowCount, int colCount, Double min, Double max)
        {
            List<List<Double>> rC = new List<List<Double>>();
            for (int i = 0; i < rowCount; i++)
            {
                List<Double> colRC = new List<double>();
                for (int j = 0; j < colCount; j++)
                {

                    //colRC.Add()
                }
            }
            return null;
        }

        private bool LoadBasicFile(String fileName)
        {
            try
            {
                this.textBoxFileOpen.Text = fileName; 
                this.dataGridView1.Columns.Clear();
                
                List<List<String>> array = LoadStockCSV(fileName);

                FileDataTitle.Clear();
                FileDataTitle = new List<string>();

                FileData.Clear();
                FileData = new List<List<double>>();

                FileDataInverse.Clear();
                FileDataInverse = new List<List<double>>();

                FileDataNumber.Clear();
                FileDataName.Clear();
                FileDataNumber = new List<string>();
                FileDataName = new List<string>();


                List<String> lS = array[0];

                for (int i = 0; i < lS.Count(); i++)
                {
                    this.dataGridView1.Columns.Add(array[0][i], array[0][i]);
                    FileDataTitle.Add(array[0][i]);
                }

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                List<String> _l = new List<String>();
                
                for (int i = 1; i < array.Count(); i++)
                {
                    _l = array[i];
                    dataGridView1.Rows.Add(_l.ToArray());
                }

                for (int j = 2; j < array[0].Count(); j++)
                {
                    List<Double> l = new List<double>();

                    for (int i = 1; i < array.Count; i++)
                    {
                        try
                        {
                            FileDataNumber.Add(array[i][0]);
                            FileDataName.Add(array[i][1]);

                            l.Add(Convert.ToDouble(array[i][j]));
                        }
                        catch (Exception)
                        {
                            l.Add(0);
                        }
                    }
                    FileDataInverse.Add(l);
                }

                for (int i = 1; i < array.Count(); i++)
                {
                    List<Double> l = new List<double>();

                    for (int j = 2; j < array[i].Count; j++)
                    {
                        try
                        {
                            l.Add(Convert.ToDouble(array[i][j]));
                        }
                        catch (Exception)
                        {
                            l.Add(0);
                        }
                    }
                    FileData.Add(l);
                }

                if (CentreObjectSelectIndex.Count > 0)
                {
                    foreach (var item in CentreObjectSelectIndex)
                    {
                        CentreObjectSelect.Add(new List<double>(FileData[item]));
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool NormalizeData(List<List<Double>> normalizeFileDataInverse)
        {
            try
            {
              
                FileData.Clear();
                FileData = new List<List<double>>();

                FileDataInverse.Clear();
                FileDataInverse = new List<List<double>>(normalizeFileDataInverse);

                for (int i = 0; i < normalizeFileDataInverse[0].Count; i++)
                {
                    List<double> l = new List<double>();
                    for (int j = 0; j < normalizeFileDataInverse.Count; j++)
                    {
                        l.Add(normalizeFileDataInverse[j][i]);
                    }
                    FileData.Add(l);
                }

                for (int i = 2; i < this.dataGridView1.ColumnCount; i++)
                {
                    for (int j = 0; j < this.dataGridView1.RowCount; j++)
                    {
                        this.dataGridView1[i, j].Value = FileData[j][i-2].ToString();
                    } 
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private bool LoadRandomData()
        {
            try
            {
                this.dataGridView1.Columns.Clear();
                
                FileData.Clear();
                FileData = new List<List<double>>();

                FileDataInverse.Clear();
                FileDataInverse = new List<List<double>>();

                int countSings = 0;
                if (!int.TryParse(this.textBoxCountSings.Text, out countSings)) throw new AppException("Введите корректные данные", this.textBoxCountSings);

                int countObj = 0;
                if (!int.TryParse(this.textBoxCountObj.Text, out countObj)) throw new AppException("Введите корректные данные", this.textBoxCountObj);

                Double rangeTo = 0;
                if (!Double.TryParse(this.textBoxRandomRangeTo.Text, out rangeTo)) throw new AppException("Введите корректные данные", this.textBoxRandomRangeTo);

                Double rangeDo = 0;
                if (!Double.TryParse(this.textBoxRandomRangeDo.Text, out rangeDo)) throw new AppException("Введите корректные данные", this.textBoxRandomRangeDo);


                FileCentreTitle.Clear();
                FileCentreTitle = new List<string>();

                FileDataTitle.Clear();
                FileDataTitle = new List<string>();

                this.dataGridView1.Columns.Add("Порядковый номер", "Порядковый номер");
                this.dataGridView1.Columns.Add("Имя объекта", "Имя объекта");
                FileCentreTitle.Add("№");

                FileDataTitle.Add("Порядковый номер");
                FileDataTitle.Add("Имя объекта");


                for (int i = 0; i < countSings; i++)
                {
                    this.dataGridView1.Columns.Add((i + 1).ToString(), (i + 1).ToString());
                    FileCentreTitle.Add((i + 1).ToString());

                    FileDataTitle.Add((i + 1).ToString());
                }

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                for (int j = 0; j < countSings; j++)
                {
                    List<Double> l = new List<double>();

                    for (int i = 0; i < countObj; i++)
                    {
                        try
                        {
                            int numbRandomInt = 0;
                            Double numbRandomDouble = 0;
                            if (this.radioButtonRandomIntNumber.Checked)
                            {
                                if (this.radioButtonRandomRangeMax.Checked)
                                {
                                    numbRandomInt = (int) GetRandomDoubleBetween(int.MinValue, int.MaxValue);
                                }
                                else numbRandomInt = GetRandomIntBetween(Convert.ToInt32(rangeTo), Convert.ToInt32(rangeDo));
                                l.Add(numbRandomInt);
                            }
                            else if (this.radioButtonRandomRealNumber.Checked)
                            {
                                if (this.radioButtonRandomRangeMax.Checked)
                                {
                                    numbRandomDouble = GetRandomDoubleBetween(int.MinValue, int.MaxValue);
                                }
                                else numbRandomDouble = GetRandomDoubleBetween(rangeTo, rangeDo);
                                l.Add(numbRandomDouble);
                            }
                            
                        }
                        catch (Exception)
                        {
                            l.Add(0);
                        }
                    }
                    FileDataInverse.Add(l);
                }

                for (int i = 0; i < FileDataInverse[0].Count; i++)
                {
                    List<Double> l = new List<double>();

                    for (int j = 0; j < FileDataInverse.Count; j++)
                    {
                        try
                        {
                            l.Add(FileDataInverse[j][i]);
                        }
                        catch (Exception)
                        {
                            l.Add(0);
                        }
                    }
                    FileData.Add(l);
                }

                FileDataNumber.Clear();
                FileDataName.Clear();

                for (int i = 0; i < countObj; i++)
                {
                    var list1 = new List<String> { (i+1).ToString(), (i+1).ToString() };
                    FileDataNumber.Add((i + 1).ToString());
                    FileDataName.Add((i + 1).ToString());
                    
                    var list2 = (from p in FileData[i] where true select p.ToString() ).ToList();
                    var list3 = list1.Concat(list2); 

                    dataGridView1.Rows.Add(list3.ToArray());
                }

 
                return true;
            }
            catch (AppException ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.ObjTextBox != null)
                {
                    ex.ObjTextBox.Focus();
                }
                else if (ex.ObjComboBox != null)
                {
                    ex.ObjComboBox.Focus();
                }
                return false;
            }
        }


        private void LoadData()
        {
            this.labelSelectedObject.Text = "(не выбрано)";

            openFileDialog1.FileName = "";
            openFileDialog1.DefaultExt = "*.csv;";
            openFileDialog1.Filter = "CSV (*.csv*)|*.csv*";
            openFileDialog1.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, dirData);

            openFileDialog1.Title = "Выберите документ для загрузки данных";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Вы не выбрали файл для открытия", "Загрузка данных...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (!LoadBasicFile(this.openFileDialog1.FileName))
                {
                    MessageBox.Show("Ошибка загрузки данных", "Загрузка данных...", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            LoadData();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.DefaultExt = "";
            saveFileDialog1.Filter = "CSV (*.csv*)|*.csv*";
            saveFileDialog1.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, dirResult);
            saveFileDialog1.Title = "Выберите документ для сохранения данных";

            saveFileDialog1.AddExtension = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName.Replace(".csv", "") + ".csv";

            if (filename != "")
            {
                List<List<String>> array = new List<List<String>>();

                List<String> l = new List<string>();

                for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                {
                    String s = "";
                    switch (this.dataGridView1.Columns[i].HeaderText)
                    {
                        case the_name:
                            s = the_nameLat;
                            break;
                        case the_r:
                            s = the_rLat;
                            break;
                        case the_t:
                            s = the_tLat;
                            break;
                        default:
                            s = this.dataGridView1.Columns[i].HeaderText;
                            break;
                    }
                    l.Add(s);
                }
                array.Add(l);

                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    l = new List<string>();
                    for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
                    {
                        if (this.dataGridView1[j, i].Value == null) break;
                        l.Add(this.dataGridView1[j, i].Value.ToString());
                    }
                    array.Add(l);
                }

                SaveStockCSV(filename, array);

                MessageBox.Show("Файл сохранен");

            }
        }

        private void comboBoxPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxPeriod_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxTo_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBoxDo_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void radioButtonW_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonWravn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonMCassicalMetod_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBoxMClassicalMetodD_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxDistanceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private bool LoadFileCenter(String filename)
        {
            try
            {
                List<List<String>> array = LoadStockCSV(filename);


                FileCentre.Clear();
                FileCentre = new List<List<double>>();

                FileCentreInverse.Clear();
                FileCentreInverse = new List<List<double>>();

                FileCentreTitle.Clear();
                FileCentreTitle = new List<string>();

                for (int t = 0; t < array[0].Count; t++)
                {
                    FileCentreTitle.Add(array[0][t]);
                }

                for (int j = 1; j < array[0].Count; j++)
                {
                    List<double> fCCol = new List<double>();
                    for (int i = 1; i < array.Count(); i++)
                    {
                        try
                        {
                            fCCol.Add(Convert.ToDouble(array[i][j]));
                        }
                        catch (Exception)
                        {
                            fCCol.Add(0);
                        }
                    }
                    FileCentreInverse.Add(fCCol);
                }

                for (int i = 1; i < array.Count; i++)
                {
                    List<double> fCCol = new List<double>();
                    for (int j = 1; j < array[0].Count(); j++)
                    {
                        try
                        {
                            fCCol.Add(Convert.ToDouble(array[i][j]));
                        }
                        catch (Exception)
                        {
                            fCCol.Add(0);
                        }
                    }
                    FileCentre.Add(fCCol);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        private void button1_Click_2(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.DefaultExt = "*.csv;";
            openFileDialog1.Filter = "CSV (*.csv*)|*.csv*";
            openFileDialog1.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, dirData);

            openFileDialog1.Title = "Выберите документ для загрузки данных";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Вы не выбрали файл для открытия", "Загрузка данных...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (LoadFileCenter(openFileDialog1.FileName)) this.textBoxFileCenter.Text = openFileDialog1.FileName;
                else {
                    MessageBox.Show("Ошибка чтения файла!");
                    this.textBoxFileCenter.Focus();
                }
            }
        }

        private bool SaveSetting(string filename)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filename);
                sw.WriteLine("radioButtonWithoutProcessing$" + this.radioButtonWithoutProcessing.Checked + "$");
                sw.WriteLine("radioButtonTballs$" + this.radioButtonTballs.Checked + "$");
                sw.WriteLine("radioButtonZrating$" + this.radioButtonZrating.Checked + "$");
                sw.WriteLine("radioButtonRangeUser$" + this.radioButtonRangeUser.Checked + "$");
                sw.WriteLine("radioButtonRange1Minus1$" + this.radioButtonRange1Minus1.Checked + "$");
                sw.WriteLine("radioButtonMiniMax$" + this.radioButtonMiniMax.Checked + "$");
                sw.WriteLine("textBoxRangeUserDo$" + this.textBoxRangeUserDo.Text + "$");
                sw.WriteLine("textBoxRangeUserTo$" + this.textBoxRangeUserTo.Text + "$");
                sw.WriteLine("textBoxCountSings$" + this.textBoxCountSings.Text + "$");
                sw.WriteLine("textBoxCountObj$" + this.textBoxCountObj.Text + "$");
                sw.WriteLine("textBoxMaxIterCenter$" + this.textBoxMaxIterCenter.Text + "$");
                sw.WriteLine("textBoxCountCluster$" + this.textBoxCountCluster.Text + "$");
                sw.WriteLine("comboBoxDistanceType$" + this.comboBoxDistanceType.Text + "$");
                sw.WriteLine("textBoxMaxIterSKO$" + this.textBoxMaxIterSKO.Text + "$");
                sw.WriteLine("radioButtonObjectSeek$" + this.radioButtonObjectSeek.Checked + "$");
                sw.WriteLine("radioButtonCenterRandom$" + this.radioButtonCenterRandom.Checked + "$");
                sw.WriteLine("radioButtonCenterFile$" + this.radioButtonCenterFile.Checked + "$");
                sw.WriteLine("textBoxFileCenter$" + this.textBoxFileCenter.Text + "$");
                sw.WriteLine("textBoxCountClusterDo$" + this.textBoxCountClusterDo.Text + "$");
                sw.WriteLine("textBoxCountClusterTo$" + this.textBoxCountClusterTo.Text + "$");
                sw.WriteLine("textBoxFileOpen$" + this.textBoxFileOpen.Text + "$");


                sw.WriteLine("radioButtonRandomRangeMax$" + this.radioButtonRandomRangeMax.Checked + "$");
                sw.WriteLine("radioButtonRandomRangeToDo$" + this.radioButtonRandomRangeToDo.Checked + "$");
                sw.WriteLine("textBoxRandomRangeTo$" + this.textBoxRandomRangeTo.Text + "$");
                sw.WriteLine("textBoxRandomRangeDo$" + this.textBoxRandomRangeDo.Text + "$");
                sw.WriteLine("radioButtonRandomIntNumber$" + this.radioButtonRandomIntNumber.Checked + "$");
                sw.WriteLine("radioButtonRandomRealNumber$" + this.radioButtonRandomRealNumber.Checked + "$");
                
                sw.WriteLine("CentreObjectSelectIndex$" + String.Join(";", CentreObjectSelectIndex) + "$");

                sw.WriteLine("radioButtonCenterRandomPP$" + this.radioButtonCenterRandomPP.Checked + "$");

                sw.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool LoadSetting(string filename)
        {
            try
            {
                StreamReader sr = new StreamReader(filename);
                this.radioButtonWithoutProcessing.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);
                this.radioButtonTballs.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);
                this.radioButtonZrating.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);
                this.radioButtonRangeUser.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);
                this.radioButtonRange1Minus1.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);
                this.radioButtonMiniMax.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);
                this.textBoxRangeUserDo.Text = sr.ReadLine().Split('$')[1];
                this.textBoxRangeUserTo.Text = sr.ReadLine().Split('$')[1];
                this.textBoxCountSings.Text = sr.ReadLine().Split('$')[1];
                this.textBoxCountObj.Text = sr.ReadLine().Split('$')[1];
                this.textBoxMaxIterCenter.Text = sr.ReadLine().Split('$')[1];
                this.textBoxCountCluster.Text = sr.ReadLine().Split('$')[1];
                this.comboBoxDistanceType.Text = sr.ReadLine().Split('$')[1];
                this.textBoxMaxIterSKO.Text = sr.ReadLine().Split('$')[1];
                this.radioButtonObjectSeek.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);
                this.radioButtonCenterRandom.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);
                this.radioButtonCenterFile.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);
                this.textBoxFileCenter.Text = sr.ReadLine().Split('$')[1];
                this.textBoxCountClusterDo.Text = sr.ReadLine().Split('$')[1];
                this.textBoxCountClusterTo.Text = sr.ReadLine().Split('$')[1];
                this.textBoxFileOpen.Text = sr.ReadLine().Split('$')[1];

                this.radioButtonRandomRangeMax.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);
                this.radioButtonRandomRangeToDo.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);
                this.textBoxRandomRangeTo.Text = sr.ReadLine().Split('$')[1];
                this.textBoxRandomRangeDo.Text = sr.ReadLine().Split('$')[1];
                this.radioButtonRandomIntNumber.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);
                this.radioButtonRandomRealNumber.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);

                String[] strCentreObjectSelectIndex = (sr.ReadLine().Split('$')[1]).Split(';');
                CentreObjectSelectIndex = (from a in strCentreObjectSelectIndex where true select Convert.ToInt32(a)).ToList();

                if (CentreObjectSelectIndex.Count>0) 
                    Program.f1.labelSelectedObject.Text = "Выбрано " + Program.f1.CentreObjectSelectIndex.Count + " объекта(ов)";
                else this.labelSelectedObject.Text = "(не выбрано)";
                
                this.radioButtonCenterRandomPP.Checked = Convert.ToBoolean(sr.ReadLine().Split('$')[1]);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    


        private void SaveSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.DefaultExt = "";
            saveFileDialog1.Filter = "TXT (*.txt*)|*.txt*";
            saveFileDialog1.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, dirSettings);
            saveFileDialog1.Title = "Выберите документ для сохранения настроек";

            saveFileDialog1.AddExtension = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName.Replace(".txt", "") + ".txt";

            if (filename != "")
            {
                SaveSetting(filename);
            }

        }

        private void LoadSettingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.DefaultExt = "*.txt;";
            openFileDialog1.Filter = "TXT (*.txt*)|*.txt*";
            openFileDialog1.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, dirSettings);

            openFileDialog1.Title = "Выберите файл настройки";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Вы не выбрали файл для открытия", "Загрузка данных...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string filename = openFileDialog1.FileName.Replace(".txt", "") + ".txt";
                string str;
                if (filename != "")
                {
                    if (LoadSetting(filename))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Ошибка загрузки.");
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSetting(dirSettings + @"\default.txt");
        }

        private void panelBegin_Paint(object sender, PaintEventArgs e)
        {

        }

        private List<List<Double>> RandomCenter(int i)
        {
            if (i > 0)
            {
                //Формирование рандомных точек 
                List<List<Double>> RandomCenterMatr = new List<List<double>>();
                for (int j = 0; j < i; j++)
                {
                    List<Double> lRC = new List<double>();

                    for (int k = 0; k < FileDataInverse.Count; k++)
                    {
                        Double min = FileDataInverse[k].Min();
                        Double max = FileDataInverse[k].Max();

                        lRC.Add(GetRandomDoubleBetween(min, max));
                    }
                    RandomCenterMatr.Add(lRC);
                }
                return RandomCenterMatr;
            }
            else return null;

        }

        private void buttonEstimate_Click(object sender, EventArgs e)
        {
            bool fError = false;
            this.radioButtonCenterRandom.Checked = true;

            int periodTo = 0;
            int periodDo = 0;
            int.TryParse(this.textBoxCountClusterDo.Text, out periodDo);

            if (int.TryParse(this.textBoxCountClusterTo.Text, out periodTo))
            {
                if (periodTo <= 1 || periodDo < periodTo)
                {
                    MessageBox.Show("Введите целое положительное значение периода больше 1 и меньше периода ''до''!");
                    fError = true;
                    this.textBoxCountClusterTo.Text = "2";
                    this.textBoxCountClusterTo.Focus();
                }
            }
            else
            {
                MessageBox.Show("Введите целое положительное значение периода больше 1!");
                fError = true;
                this.textBoxCountClusterTo.Text = "2";
                this.textBoxCountClusterTo.Focus();
            }

            int countRows = dataGridView1.Rows.Count;

            if (int.TryParse(this.textBoxCountClusterDo.Text, out periodDo))
            {
                if (periodDo < periodTo || periodDo > countRows)
                {
                    MessageBox.Show("Введите целое положительное значение периода больше 1 и больше значения ''от''!");
                    fError = true;
                    this.textBoxCountClusterDo.Text = countRows.ToString();
                    this.textBoxCountClusterDo.Focus();
                }
            }
            else
            {
                MessageBox.Show("Введите целое положительное значение периода больше 1!");
                fError = true;
                this.textBoxCountClusterDo.Text = countRows.ToString();
                this.textBoxCountClusterDo.Focus();
            }

            int a = periodTo;
            int b = periodDo;

            if (a <= 1 || b <= 1 || b <= a || b > countRows)
            {
                if (b > a)
                {
                    a = 2;
                    b = countRows;
                }
                else
                {
                    b = countRows;
                }
            }
            this.textBoxCountClusterTo.Text = a.ToString();
            this.textBoxCountClusterDo.Text = b.ToString();

            if (this.dataGridView1.ColumnCount > 1 && this.dataGridView1.RowCount > 1 && FileData.Count > 1 && !fError)
            {
                List<Double> sko = new List<double>();
                for (int i = a; i <= b; i++)
                {

                    //Считаем СКО
                    ClassClustering cC = new ClassClustering(FileData, RandomCenter(i));
                    List<List<Double>> lCenter = cC.FindCenterCluster(1000);
                    sko.Add(cC.CheckSKO());
                }

                //Строим график SKO
                FormGraph fG = new FormGraph();
                fG.Show();
                fG.labelTitle.Text = "";

                fG.chart1.Series[0].Points.Clear();
                fG.chart1.Series[0].Name = "Ряд 1";
                int x = 0;
                Double y = 0;

                Double minY = Double.MaxValue, maxY = Double.MinValue;

                for (int i = 0; i < sko.Count; i++)
                {
                    // int.TryParse(a, out x);
                    x = i;
                    y = sko[i];

                   // if (x != 0 && y != 0)
                    {
                        if (y > maxY)
                        {
                            maxY = y;
                        }

                        if (y < minY)
                        {
                            minY = y;
                        }

                        fG.chart1.Series[0].Points.AddXY(x+a, y);
                    }
                }

                fG.chart1.ChartAreas[0].AxisY.Minimum = Math.Round(minY, 7);
                fG.chart1.ChartAreas[0].AxisY.Maximum = Math.Round(maxY, 7);

                fG.chart1.ChartAreas[0].AxisX.Minimum = a;
                fG.chart1.ChartAreas[0].AxisX.Maximum = b;
                fG.chart1.ChartAreas[0].AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated90;

                fG.Text = Form1.nameApp;

            }


        }
        

        private void textBoxCountClusterTo_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!Int32.TryParse(this.textBoxCountClusterTo.Text, out result))
            {
                this.textBoxCountClusterTo.Text = "";
            }
        }

        private void textBoxCountClusterDo_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!Int32.TryParse(this.textBoxCountClusterDo.Text, out result))
            {
                this.textBoxCountClusterDo.Text = "";
            }
        }

        private void textBoxCountCluster_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!Int32.TryParse(this.textBoxCountCluster.Text, out result))
            {
                this.textBoxCountCluster.Text = "";
            }
        }

        private void textBoxMaxIterCenter_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!Int32.TryParse(this.textBoxMaxIterCenter.Text, out result))
            {
                this.textBoxMaxIterCenter.Text = "";
            }
        }

        private void textBoxMaxIterSKO_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!Int32.TryParse(this.textBoxMaxIterSKO.Text, out result))
            {
                this.textBoxMaxIterSKO.Text = "";
            }
        }

        private void comboBoxDistanceType_TextUpdate(object sender, EventArgs e)
        {
            switch (comboBoxDistanceType.Text)
            {
                case "Эвклидово расстояние": break;
                case "Норма L1 (манхэттенское расстояние)": break;
                case "Расстояние Чебышёва (расстояние шахматной доски)": break;
                default:
                    comboBoxDistanceType.Text = "Эвклидово расстояние";
                    break;
            }
        }

        private void textBoxRangeUserTo_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!Int32.TryParse(this.textBoxRangeUserTo.Text, out result))
            {
                this.textBoxRangeUserTo.Text = "";
            }
        }

        private void textBoxRangeUserDo_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!Int32.TryParse(this.textBoxRangeUserDo.Text, out result))
            {
                this.textBoxRangeUserDo.Text = "";
            }
        }

        private void textBoxCountObj_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!Int32.TryParse(this.textBoxCountObj.Text, out result))
            {
                this.textBoxCountObj.Text = "";
            }
        }

        private void textBoxCountSings_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!Int32.TryParse(this.textBoxCountSings.Text, out result))
            {
                this.textBoxCountSings.Text = "";
            }
        }

        public class AppException : Exception
        {
            public TextBox ObjTextBox { get; }
            public ComboBox ObjComboBox { get; }

            public AppException(String message) : base(message)
            {
                ObjTextBox = null;
                ObjComboBox = null;
            }

            public AppException(String message, TextBox b) : base(message)
            {
                ObjTextBox = b;
                ObjComboBox = null;
            }

            public AppException(String message, ComboBox b) : base(message)
            {
                ObjTextBox = null;
                ObjComboBox = b;
            }

            public AppException(String message, Exception inner, TextBox b) : base(message, inner) { ObjTextBox = b; }
        }

        private void buttonClustering_Click(object sender, EventArgs e)
        {
            try
            {
                int a = 0, b = 0;

                //Exception noData;
                //Проверка нормализованных данных
                if (this.dataGridView1.RowCount <= 1) throw new AppException("Загрузите данные");
                
                int countObj, countSings;
                if (!int.TryParse(this.textBoxCountObj.Text, out countObj))
                {
                    throw new AppException("Укажите количество объектов", this.textBoxCountObj);
                }

                if (!int.TryParse(this.textBoxCountSings.Text, out countSings))
                {
                    throw new AppException("Укажите количество признаков", this.textBoxCountSings);
                }

                //Проверка данных Кластеризации
                int countCluster = 0;
                if (!Int32.TryParse(this.textBoxCountCluster.Text, out countCluster))
                {
                    throw new AppException("Укажите число кластеров", this.textBoxCountCluster);
                }

                int maxIterCenter = 0;
                if (!Int32.TryParse(this.textBoxMaxIterCenter.Text, out maxIterCenter))
                {
                    throw new AppException("Укажите число итераций (изменений центров кластеров)", this.textBoxMaxIterCenter);
                }

                int maxIterSKO = 0;
                if (!Int32.TryParse(this.textBoxMaxIterSKO.Text, out maxIterSKO))
                {
                    throw new AppException("Укажите число итераций (по минимизации СКО)", this.textBoxMaxIterSKO);
                }

                switch (comboBoxDistanceType.Text)
                {
                    case "Эвклидово расстояние": break;
                    case "Норма L1 (манхэттенское расстояние)": break;
                    case "Расстояние Чебышёва (расстояние шахматной доски)": break;
                    default:
                        throw new AppException("Укажите меру расстояния", this.comboBoxDistanceType);
                }



                //Clustering
                ClassClustering cC = null;
                List<List<Double>> center = null;

                if (this.radioButtonCenterFile.Checked)
                {
                    center = FileCentre;
                }
                else if (this.radioButtonCenterRandom.Checked)
                {
                    center = RandomCenter(countCluster);
                }
                else if (this.radioButtonObjectSeek.Checked && this.CentreObjectSelectIndex.Count > 0)
                {
                    center = this.CentreObjectSelect;
                }

                if (center != null)
                {
                    cC = new ClassClustering(FileData, center);
                }


                if (cC != null)
                {
                    List<List<Double>> lCenter = cC.FindCenterCluster(maxIterCenter, Convert.ToByte(this.comboBoxDistanceType.SelectedIndex));
                    Double sko = cC.CheckSKO();

                    FormClustering fC = new FormClustering(nameApp);
                    fC.Show();

                    int checkCountCluster = (this.radioButtonCenterRandom.Checked ? countCluster : (this.radioButtonCenterFile.Checked ? FileCentre.Count : (this.radioButtonObjectSeek.Checked ? CentreObjectSelectIndex.Count : countCluster)));

                    fC.labelTitle.Text = "Число кластеров: " + checkCountCluster + ". Максимум итераций: " + maxIterCenter + ". Мера расстояния: " + this.comboBoxDistanceType.Text + ".";

                    countClusters = checkCountCluster;

                    // Принадлежность к кластерам
                    fC.dataGridViewClusters.Columns.Clear();
                    fC.dataGridViewClusters.Columns.Add(this.dataGridView1.Columns[0].Name, this.dataGridView1.Columns[0].HeaderText);
                    fC.dataGridViewClusters.Columns.Add(this.dataGridView1.Columns[1].Name, this.dataGridView1.Columns[1].HeaderText);
                    fC.dataGridViewClusters.Columns.Add("NumberCluster", "Номер кластера");
                    indexCluster = cC.indexCluster;

                    for (int i = 0; i < this.dataGridView1.RowCount; i++)
                    {
                        fC.dataGridViewClusters.Rows.Add(this.dataGridView1[0, i].Value, this.dataGridView1[1, i].Value, cC.indexCluster[i] + 1);
                    }

                    //SKO
                    fC.textBoxSKO.Text = sko.ToString();

                    // Начальные центры кластеров
                    fC.dataGridViewBeginCenter.Columns.Clear();

                    foreach (var item in FileCentreTitle)
                    {
                        fC.dataGridViewBeginCenter.Columns.Add(item, item);
                    }

                    for (int i = 0; i < center.Count; i++)
                    {
                        var list1 = new List<String>() { (i + 1).ToString() };
                        var list2 = (from p in center[i] where true select p.ToString()).ToList(); 
                        var list3 = list1.Concat(list2);
                        fC.dataGridViewBeginCenter.Rows.Add(list3.ToArray());   
                    }

                    // Конечные центры кластеров
                    fC.dataGridViewEndCenter.Columns.Clear();

                    foreach (var item in FileCentreTitle)
                    {
                        fC.dataGridViewEndCenter.Columns.Add(item, item);
                    }
                    
                    EndCenterCluster = cC.newCenterCluster;
                    for (int i = 0; i < cC.newCenterCluster.Count; i++)
                    {
                        var list1 = new List<String>() { (i + 1).ToString() };
                        var list2 = (from p in cC.newCenterCluster[i] where true select p.ToString()).ToList();
                        var list3 = list1.Concat(list2);
                        fC.dataGridViewEndCenter.Rows.Add(list3.ToArray());
                    }

                    // Хронология итераций
                    fC.dataGridViewHistoryIteration.Columns.Clear();

                    fC.dataGridViewHistoryIteration.Columns.Add("Iteration", "Итерация");

                    for (int item = 0; item < cC.chronologyDistance[0].Count(); item++)
                    {
                        fC.dataGridViewHistoryIteration.Columns.Add((item+1).ToString(), (item+1).ToString()); 
                    }


                    for (int iter = 0; iter < cC.chronologyDistance.Count; iter++)
                    {
                        var list0 = new List<String>() { (iter + 1).ToString() }; //Итерация
                        var list1 = (from p in cC.chronologyDistance[iter] where true select p.ToString()).ToList();
                        var list2 = list0.Concat(list1);
                        fC.dataGridViewHistoryIteration.Rows.Add(list2.ToArray());
                    }

                    // Число наблюдений в каждом кластере
                    fC.dataGridViewNumberOfObservations.Columns.Clear();
                    fC.dataGridViewNumberOfObservations.Columns.Add("NumberCluster", "Номер кластера");
                    fC.dataGridViewNumberOfObservations.Columns.Add("CountObject", "Число объектов");
                    
                    for (int i = 0; i < center.Count; i++)
                    {
                        int count = cC.indexCluster.Count(d => d == i);
                        fC.dataGridViewNumberOfObservations.Rows.Add((i+1).ToString(),  count);
                    }

                    fC.Text = nameApp;

                }

            }
            catch (AppException ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.ObjTextBox != null)
                {
                    ex.ObjTextBox.Focus();
                }
                else if (ex.ObjComboBox != null)
                {
                    ex.ObjComboBox.Focus();
                }
            }
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            LoadRandomData();
        }

        private void buttonLoadFileData_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void radioButtonRandomRangeToDo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void buttonSelectObject_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.RowCount >0)
            {
                FormSelectObject fSO = new FormSelectObject();
                fSO.Show();
            }
        }


        private void SaveTable()
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.DefaultExt = "";
            saveFileDialog1.Filter = "CSV (*.csv*)|*.csv*";
            saveFileDialog1.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, dirResult);
            saveFileDialog1.Title = "Выберите документ для сохранения данных";

            saveFileDialog1.AddExtension = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName.Replace(".csv", "") + ".csv";

            if (filename != "")
            {
                List<List<String>> array = new List<List<String>>();

                List<String> l = new List<string>();

                for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                {
                    l.Add(this.dataGridView1.Columns[i].HeaderText);
                }
                array.Add(l);

                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    l = new List<string>();
                    for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
                    {
                        if (this.dataGridView1[j, i].Value == null) break;
                        l.Add(this.dataGridView1[j, i].Value.ToString());
                    }
                    array.Add(l);
                }

                SaveStockCSV(filename, array);

                MessageBox.Show("Файл сохранен");

            }
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveTable();
        }

        private void buttonNormalize_Click(object sender, EventArgs e)
        {
            try
            {
                int a = 0, b = 0;

                //Exception noData;
                //Проверка нормализованных данных
                if (this.dataGridView1.RowCount <= 1) throw new AppException("Загрузите данные");

                if (this.radioButtonRangeUser.Checked)
                {
                    if (!int.TryParse(this.textBoxRangeUserTo.Text, out a))
                    {
                        throw new AppException("Укажите диапазон нормализации", this.textBoxRangeUserTo);
                    }

                    if (!int.TryParse(this.textBoxRangeUserDo.Text, out b))
                    {
                        throw new AppException("Укажите диапазон нормализации", this.textBoxCountClusterDo);
                    }

                    if (!ClassMath.RangeAB(a, b))
                    {
                        throw new AppException("Укажите диапазон нормализации", this.textBoxRangeUserTo);
                    }
                }

                //Нормализуем данные
                List<List<Double>> normalizeFileDataInverse = new List<List<double>>();

                if (this.radioButtonMiniMax.Checked) // [0;1]
                {
                    foreach (var item in FileDataInverse)
                    {
                        List<Double> lND = new List<double>();
                        lND.AddRange(ClassMath.NormalizedData(item));
                        normalizeFileDataInverse.Add(lND);
                    }
                }

                if (this.radioButtonRange1Minus1.Checked)
                {
                    foreach (var item in FileDataInverse)
                    {
                        List<Double> lND = new List<double>();
                        lND.AddRange(ClassMath.NormalizedData(item, 1, -1));
                        normalizeFileDataInverse.Add(lND);
                    }
                }

                if (this.radioButtonRangeUser.Checked)
                {
                    foreach (var item in FileDataInverse)
                    {
                        List<Double> lND = new List<double>();
                        lND.AddRange(ClassMath.NormalizedData(item, Convert.ToDouble(b), Convert.ToDouble(a)));
                        normalizeFileDataInverse.Add(lND);
                    }
                }

                if (this.radioButtonZrating.Checked)
                {
                    foreach (var item in FileDataInverse)
                    {
                        List<Double> lND = new List<double>();
                        lND.AddRange(ClassMath.Zscaling(item));
                        normalizeFileDataInverse.Add(lND);
                    }
                }

                if (this.radioButtonTballs.Checked)
                {
                    foreach (var item in FileDataInverse)
                    {
                        List<Double> lND = new List<double>();
                        lND.AddRange(ClassMath.Tballs(item));
                        normalizeFileDataInverse.Add(lND);
                    }
                }

                if (!this.radioButtonWithoutProcessing.Checked)
                {
                    string message = "Нормализовать данные в таблице? Таблица будет перезаписана.";
                    string caption = "";

                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;

                    // Displays the MessageBox.
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        NormalizeData(normalizeFileDataInverse);
                    }
                } 

            }
            catch (AppException ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.ObjTextBox != null)
                {
                    ex.ObjTextBox.Focus();
                }
                else if (ex.ObjComboBox != null)
                {
                    ex.ObjComboBox.Focus();
                }
            }
        }

        private void button1_Click_4(object sender, EventArgs e) //buttonSaveTable
        {
            SaveTable();
        }
    }
}
