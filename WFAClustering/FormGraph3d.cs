using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System;
using System.Collections;
using HostingWpfUserControlInWf1;

using System.Windows.Forms.Integration;

using System.Threading;
using System.Threading.Tasks;

using ClassLibraryClustering;

namespace WFASeasonalCoefficients
{
    public partial class FormGraph3d : Form
    {
        // transform class object for rotate the 3d model
        public WPFChart3D.TransformMatrix m_transformMatrix = new WPFChart3D.TransformMatrix();

        // ***************************** 3d chart ***************************
        // https://www.codeproject.com/Articles/42174/High-performance-WPF-3D-Chart

        private WPFChart3D.Chart3D m_3dChart;       // data for 3d chart
        public int m_nChartModelIndex = -1;         // model index in the Viewport3d
        public int m_nSurfaceChartGridNo = 100;     // surface chart grid no. in each axis
        public int m_nScatterPlotDataNo = 5000;     // total data number of the scatter plot

        // ***************************** selection rect ***************************
        WPFChart3D.ViewportRect m_selectRect = new WPFChart3D.ViewportRect();

        public int m_nRectModelIndex = -1;

        public volatile bool Run = true;


        // Create the WPF UserControl.
        HostingWpfUserControlInWf1.UserControl1 vP =
            new HostingWpfUserControlInWf1.UserControl1();



        public FormGraph3d()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void GettingData()
        {
            //Запускает функцию получения стакана в отдельном потоке, чтобы приложение откликалось на действия пользователя
            //Чтобы остановить выполнение данного потока, нужно переменной "Run" присвоить значение "false"
            //Сделать это можно, либо в функции по событию закрытия приложения, либо при нажатии на кнопке и т.д.
            new Thread(() =>
            {
                //Постоянный цикл в отдельном потоке
                while (Run)
                {
                    

                    Thread.Sleep(1);
                }
            }).Start();
        }

        private void FormGraph_Load(object sender, EventArgs e)
        {

            // Create the ElementHost control for hosting the
            // WPF UserControl.
            ElementHost host = new ElementHost();
            host.Dock = DockStyle.Fill;



            // Assign the WPF UserControl to the ElementHost control's
            // Child property.
            host.Child = vP;

            // Add the ElementHost control to the form's
            // collection of child controls.
            this.Controls.Add(host);


            //this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown); 

            vP.myViewport1.KeyDown += new System.Windows.Input.KeyEventHandler(this.OnKeyDown);
           // OnViewportMouseMove
            // selection rect
            m_selectRect.SetRect(new Point(-0.5, -0.5), new Point(-0.5, -0.5));

            WPFChart3D.Model3D model3d = new WPFChart3D.Model3D();
            ArrayList meshs = m_selectRect.GetMeshes();
            m_nRectModelIndex = model3d.UpdateModel(meshs, null, m_nRectModelIndex, vP.myViewport1);
            
            // display surface chart
            TestScatterPlot(1000);
            TransformChart();

            GettingData();
        }

        // function for testing surface chart
        public void TestSurfacePlot(int nGridNo)
        {
            int nXNo = nGridNo;
            int nYNo = nGridNo;
            // 1. set the surface grid
            m_3dChart = new WPFChart3D.UniformSurfaceChart3D();
            ((WPFChart3D.UniformSurfaceChart3D)m_3dChart).SetGrid(nXNo, nYNo, -100, 100, -100, 100);

            // 2. set surface chart z value
            double xC = m_3dChart.XCenter();
            double yC = m_3dChart.YCenter();
            int nVertNo = m_3dChart.GetDataNo();
            double zV;
            for (int i = 0; i < nVertNo; i++)
            {
                WPFChart3D.Vertex3D vert = m_3dChart[i];

                double r = 0.15 * Math.Sqrt((vert.x - xC) * (vert.x - xC) + (vert.y - yC) * (vert.y - yC));
                if (r < 1e-10) zV = 1;
                else zV = Math.Sin(r) / r;

                m_3dChart[i].z = (float)zV;
            }
            m_3dChart.GetDataRange();

            // 3. set the surface chart color according to z vaule
            double zMin = m_3dChart.ZMin();
            double zMax = m_3dChart.ZMax();
            for (int i = 0; i < nVertNo; i++)
            {
                WPFChart3D.Vertex3D vert = m_3dChart[i];
                double h = (vert.z - zMin) / (zMax - zMin);

                Color color = WPFChart3D.TextureMapping.PseudoColor(h);
                m_3dChart[i].color = color;
            }

            // 4. Get the Mesh3D array from surface chart
            ArrayList meshs = ((WPFChart3D.UniformSurfaceChart3D)m_3dChart).GetMeshes();

            // 5. display vertex no and triangle no of this surface chart
            UpdateModelSizeInfo(meshs);

            // 6. Set the model display of surface chart
            WPFChart3D.Model3D model3d = new WPFChart3D.Model3D();
            Material backMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Gray));
            m_nChartModelIndex = model3d.UpdateModel(meshs, backMaterial, m_nChartModelIndex, vP.myViewport1);

            // 7. set projection matrix, so the data is in the display region
            float xMin = m_3dChart.XMin();
            float xMax = m_3dChart.XMax();
            m_transformMatrix.CalculateProjectionMatrix(xMin, xMax, xMin, xMax, zMin, zMax, 0.5);
            TransformChart();
        }

        // function for testing 3d scatter plot
        public void TestScatterPlot(int nDotNo)
        {
            // 1. set scatter chart data no.
            m_3dChart = new WPFChart3D.ScatterChart3D();
            m_3dChart.SetDataNo(Program.f1.FileData.Count);

            // 2.
            Double maxX = Double.MinValue, maxY = Double.MinValue, maxZ = Double.MinValue;
            Double minX = Double.MaxValue, minY = Double.MaxValue, minZ = Double.MaxValue;
            Random randomObject = new Random();
            int nDataRange = Program.f1.FileData.Count;
            List<List<Double>> matrMinMax = ClassLibraryClustering.ClassMath.MinMax(Program.f1.FileData);

            for (int i = 0; i < Program.f1.countClusters; i++)
            {




                Byte nR = (Byte)randomObject.Next(256);
                Byte nG = (Byte)randomObject.Next(256);
                Byte nB = (Byte)randomObject.Next(256);

                for (int k = 0; k < Program.f1.FileData.Count; k++)
                {
                    if (Program.f1.indexCluster[k] == i)
                    {
                        Double x = 0, y = 0, z = 0;

                        x = ClassLibraryClustering.ClassMath.NormalizedData(Program.f1.FileData[k][0], matrMinMax[0][0], matrMinMax[0][1], 200, 0);
                        y = ClassLibraryClustering.ClassMath.NormalizedData(Program.f1.FileData[k][1], matrMinMax[1][0], matrMinMax[1][1], 200, 0);
                        z = ClassLibraryClustering.ClassMath.NormalizedData(Program.f1.FileData[k][2], matrMinMax[2][0], matrMinMax[2][1], 200, 0);

                        if (x > maxX) maxX = x;
                        if (x < minX) minX = x;

                        if (y > maxY) maxY = y;
                        if (y < minY) minY = y;

                        if (z > maxZ) maxZ = z;
                        if (z < minZ) minZ = z;


                        WPFChart3D.ScatterPlotItem plotItem = new WPFChart3D.ScatterPlotItem();

                        plotItem.x = (float) x;
                        plotItem.y = (float) y;
                        plotItem.z = (float) z;

                        plotItem.w = 4;
                        plotItem.h = 6;
                        

                        plotItem.shape = randomObject.Next(4);

                        plotItem.color = Color.FromRgb(nR, nG, nB);
                        ((WPFChart3D.ScatterChart3D)m_3dChart).SetVertex(k, plotItem);
                    }
                }

                
            }

            // 3. set the axes
            m_3dChart.GetDataRange();
            m_3dChart.SetAxes();

            // 4. get Mesh3D array from the scatter plot
            ArrayList meshs = ((WPFChart3D.ScatterChart3D)m_3dChart).GetMeshes();

            // 5. display model vertex no and triangle no
            UpdateModelSizeInfo(meshs);

            // 6. display scatter plot in Viewport3D
            WPFChart3D.Model3D model3d = new WPFChart3D.Model3D();
            m_nChartModelIndex = model3d.UpdateModel(meshs, null, m_nChartModelIndex, vP.myViewport1 );

            // 7. set projection matrix
            Double rangeX = maxX - minX;
            Double rangeY = maxY - minY;
            Double rangeZ = maxZ - minZ;
            Double maxRange = (rangeX > rangeY ? (rangeX > rangeZ ? rangeX : rangeZ) : (rangeY > rangeZ ? rangeY : rangeZ));
            Double min = (minX < minY ? (minX < minZ ? minX : minZ) : (minY < minZ ? minY : minZ));
            Double max = (maxX > maxY ? (maxX > maxZ ? maxX : maxZ) : (maxY > maxZ ? maxY : maxZ));

            int count1 = Program.f1.FileData.Count;
            int viewRange = 200;
            m_transformMatrix.CalculateProjectionMatrix(0, viewRange, 0, viewRange, 0, viewRange, 0.5);

            // m_transformMatrix.CalculateProjectionMatrix(minX, maxX, minY, maxY, minZ, maxZ, 0.5);// ClassLibraryClustering.ClassMath.NormalizedData(maxRange * count1, min * count1, max * count1, 1, 0.8 ));
            TransformChart();
        }

        // function for set a scatter plot, every dot is just a simple pyramid.
        public void TestSimpleScatterPlot(int nDotNo)
        {
            // 1. set the scatter plot size
            m_3dChart = new WPFChart3D.ScatterChart3D();
            m_3dChart.SetDataNo(nDotNo);

            // 2. set the properties of each dot
            Random randomObject = new Random();
            int nDataRange = 200;
            for (int i = 0; i < nDotNo; i++)
            {
                WPFChart3D.ScatterPlotItem plotItem = new WPFChart3D.ScatterPlotItem();

                plotItem.w = 2;
                plotItem.h = 2;

                plotItem.x = (float)randomObject.Next(nDataRange);
                plotItem.y = (float)randomObject.Next(nDataRange);
                plotItem.z = (float)randomObject.Next(nDataRange);

                plotItem.shape = (int)WPFChart3D.Chart3D.SHAPE.PYRAMID;

                Byte nR = (Byte)randomObject.Next(256);
                Byte nG = (Byte)randomObject.Next(256);
                Byte nB = (Byte)randomObject.Next(256);

                plotItem.color = Color.FromRgb(nR, nG, nB);
                ((WPFChart3D.ScatterChart3D)m_3dChart).SetVertex(i, plotItem);
            }
            // 3. set axes
            m_3dChart.GetDataRange();
            m_3dChart.SetAxes();

            // 4. Get Mesh3D array from scatter plot
            ArrayList meshs = ((WPFChart3D.ScatterChart3D)m_3dChart).GetMeshes();

            // 5. display vertex no and triangle no.
            UpdateModelSizeInfo(meshs);

            // 6. show 3D scatter plot in Viewport3d
            WPFChart3D.Model3D model3d = new WPFChart3D.Model3D();
            m_nChartModelIndex = model3d.UpdateModel(meshs, null, m_nChartModelIndex, vP.myViewport1);

            // 7. set projection matrix
            float viewRange = (float)nDataRange;
            m_transformMatrix.CalculateProjectionMatrix(0, viewRange, 0, viewRange, 0, viewRange, 0.5);
            TransformChart();
        }


        public void OnViewportMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs args)
        {
            Point pt = args.GetPosition(vP.myViewport1);
            if (args.ChangedButton == MouseButton.Left)         // rotate or drag 3d model
            {
                m_transformMatrix.OnLBtnDown(pt);
            }
            else if (args.ChangedButton == MouseButton.Right)   // select rect
            {
                m_selectRect.OnMouseDown(pt, vP.myViewport1, m_nRectModelIndex);
            }
        }

        public void OnViewportMouseMove(object sender, System.Windows.Input.MouseEventArgs args)
        {
            Point pt = args.GetPosition(vP.myViewport1);

            if (args.LeftButton == MouseButtonState.Pressed)                // rotate or drag 3d model
            {
                m_transformMatrix.OnMouseMove(pt, vP.myViewport1);

                TransformChart();
            }
            else if (args.RightButton == MouseButtonState.Pressed)          // select rect
            {
                m_selectRect.OnMouseMove(pt, vP.myViewport1, m_nRectModelIndex);
            }
            else
            {
                /*
                String s1;
                Point pt2 = m_transformMatrix.VertexToScreenPt(new Point3D(0.5, 0.5, 0.3), vP.myViewport1);
                s1 = string.Format("Screen:({0:d},{1:d}), Predicated: ({2:d}, H:{3:d})", 
                    (int)pt.X, (int)pt.Y, (int)pt2.X, (int)pt2.Y);
                this.statusPane.Text = s1;
                */
            }
        }

        public void OnViewportMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs args)
        {
            Point pt = args.GetPosition(vP.myViewport1);
            if (args.ChangedButton == MouseButton.Left)
            {
                m_transformMatrix.OnLBtnUp();
            }
            else if (args.ChangedButton == MouseButton.Right)
            {
                if (m_nChartModelIndex == -1) return;
                // 1. get the mesh structure related to the selection rect
                MeshGeometry3D meshGeometry = WPFChart3D.Model3D.GetGeometry(vP.myViewport1, m_nChartModelIndex);
                if (meshGeometry == null) return;

                // 2. set selection in 3d chart
                m_3dChart.Select(m_selectRect, m_transformMatrix, vP.myViewport1);

                // 3. update selection display
                m_3dChart.HighlightSelection(meshGeometry, Color.FromRgb(200, 200, 200));
            }
        }

        // zoom in 3d display
        public void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs args)
        {
            m_transformMatrix.OnKeyDown(args);
            TransformChart();
        }
        
        private void UpdateModelSizeInfo(ArrayList meshs)
        {
            int nMeshNo = meshs.Count;
            int nChartVertNo = 0;
            int nChartTriangelNo = 0;
            for (int i = 0; i < nMeshNo; i++)
            {
                nChartVertNo += ((WPFChart3D.Mesh3D)meshs[i]).GetVertexNo();
                nChartTriangelNo += ((WPFChart3D.Mesh3D)meshs[i]).GetTriangleNo();
            }
        }

        // this function is used to rotate, drag and zoom the 3d chart
        private void TransformChart()
        {
            if (m_nChartModelIndex == -1) return;
            ModelVisual3D visual3d = (ModelVisual3D)(vP.myViewport1.Children[m_nChartModelIndex]);
            if (visual3d.Content == null) return;
            Transform3DGroup group1 = visual3d.Content.Transform as Transform3DGroup;
            group1.Children.Clear();
            group1.Children.Add(new MatrixTransform3D(m_transformMatrix.m_totalMatrix));
        }

        private void FormGraph3d_FormClosing(object sender, FormClosingEventArgs e)
        {
            Run = false;
        }
    }
}
