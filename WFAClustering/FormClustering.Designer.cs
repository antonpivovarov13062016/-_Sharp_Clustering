namespace WFASeasonalCoefficients
{
    partial class FormClustering
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClustering));
            this.dataGridViewClusters = new System.Windows.Forms.DataGridView();
            this.ColumnNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSKO = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewNumberOfObservations = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewHistoryIteration = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridViewBeginCenter = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridViewEndCenter = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.radioButtonSelCluster = new System.Windows.Forms.RadioButton();
            this.radioButtonAllCluster = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClusters)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNumberOfObservations)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistoryIteration)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBeginCenter)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndCenter)).BeginInit();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewClusters
            // 
            this.dataGridViewClusters.AllowDrop = true;
            this.dataGridViewClusters.AllowUserToAddRows = false;
            this.dataGridViewClusters.AllowUserToDeleteRows = false;
            this.dataGridViewClusters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewClusters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClusters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNR,
            this.ColumnR});
            this.dataGridViewClusters.Location = new System.Drawing.Point(13, 28);
            this.dataGridViewClusters.Name = "dataGridViewClusters";
            this.dataGridViewClusters.Size = new System.Drawing.Size(301, 291);
            this.dataGridViewClusters.TabIndex = 1;
            this.dataGridViewClusters.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ColumnNR
            // 
            this.ColumnNR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnNR.HeaderText = "1";
            this.ColumnNR.Name = "ColumnNR";
            this.ColumnNR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnR
            // 
            this.ColumnR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnR.HeaderText = "2";
            this.ColumnR.Name = "ColumnR";
            this.ColumnR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(755, 544);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(168, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(929, 544);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(168, 23);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonSeasonFactor_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(12, 6);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(49, 13);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "labelTitle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Принадлежность к кластерам";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 326);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Суммарное квадратичное отклонение";
            // 
            // textBoxSKO
            // 
            this.textBoxSKO.Location = new System.Drawing.Point(214, 325);
            this.textBoxSKO.Name = "textBoxSKO";
            this.textBoxSKO.Size = new System.Drawing.Size(100, 20);
            this.textBoxSKO.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 349);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(300, 27);
            this.button1.TabIndex = 9;
            this.button1.Text = "Отобразить полученные кластеры на графике";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.dataGridViewClusters);
            this.panel1.Controls.Add(this.textBoxSKO);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(13, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 400);
            this.panel1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 377);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "(только для 2-мерного и 3-мерного пространства)";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.dataGridViewNumberOfObservations);
            this.panel2.Location = new System.Drawing.Point(737, 225);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(360, 197);
            this.panel2.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Число наблюдений в каждом кластере";
            // 
            // dataGridViewNumberOfObservations
            // 
            this.dataGridViewNumberOfObservations.AllowDrop = true;
            this.dataGridViewNumberOfObservations.AllowUserToAddRows = false;
            this.dataGridViewNumberOfObservations.AllowUserToDeleteRows = false;
            this.dataGridViewNumberOfObservations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewNumberOfObservations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNumberOfObservations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridViewNumberOfObservations.Location = new System.Drawing.Point(14, 32);
            this.dataGridViewNumberOfObservations.Name = "dataGridViewNumberOfObservations";
            this.dataGridViewNumberOfObservations.Size = new System.Drawing.Size(338, 149);
            this.dataGridViewNumberOfObservations.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "2";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.dataGridViewHistoryIteration);
            this.panel3.Location = new System.Drawing.Point(737, 22);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(360, 197);
            this.panel3.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(267, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Хронология итераций (в рамках последнего цикла)";
            // 
            // dataGridViewHistoryIteration
            // 
            this.dataGridViewHistoryIteration.AllowDrop = true;
            this.dataGridViewHistoryIteration.AllowUserToAddRows = false;
            this.dataGridViewHistoryIteration.AllowUserToDeleteRows = false;
            this.dataGridViewHistoryIteration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewHistoryIteration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistoryIteration.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dataGridViewHistoryIteration.Location = new System.Drawing.Point(14, 32);
            this.dataGridViewHistoryIteration.Name = "dataGridViewHistoryIteration";
            this.dataGridViewHistoryIteration.Size = new System.Drawing.Size(338, 156);
            this.dataGridViewHistoryIteration.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "1";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "2";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.dataGridViewBeginCenter);
            this.panel4.Location = new System.Drawing.Point(371, 22);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(360, 197);
            this.panel4.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Начальные центры кластеров";
            // 
            // dataGridViewBeginCenter
            // 
            this.dataGridViewBeginCenter.AllowDrop = true;
            this.dataGridViewBeginCenter.AllowUserToAddRows = false;
            this.dataGridViewBeginCenter.AllowUserToDeleteRows = false;
            this.dataGridViewBeginCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewBeginCenter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBeginCenter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dataGridViewBeginCenter.Location = new System.Drawing.Point(14, 32);
            this.dataGridViewBeginCenter.Name = "dataGridViewBeginCenter";
            this.dataGridViewBeginCenter.Size = new System.Drawing.Size(338, 156);
            this.dataGridViewBeginCenter.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.HeaderText = "1";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "2";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.dataGridViewEndCenter);
            this.panel5.Location = new System.Drawing.Point(371, 225);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(360, 197);
            this.panel5.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Конечные центры кластеров";
            // 
            // dataGridViewEndCenter
            // 
            this.dataGridViewEndCenter.AllowDrop = true;
            this.dataGridViewEndCenter.AllowUserToAddRows = false;
            this.dataGridViewEndCenter.AllowUserToDeleteRows = false;
            this.dataGridViewEndCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEndCenter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEndCenter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dataGridViewEndCenter.Location = new System.Drawing.Point(14, 32);
            this.dataGridViewEndCenter.Name = "dataGridViewEndCenter";
            this.dataGridViewEndCenter.Size = new System.Drawing.Size(338, 156);
            this.dataGridViewEndCenter.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.HeaderText = "1";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.HeaderText = "2";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.radioButtonSelCluster);
            this.panel6.Controls.Add(this.radioButtonAllCluster);
            this.panel6.Controls.Add(this.button2);
            this.panel6.Location = new System.Drawing.Point(15, 428);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(350, 100);
            this.panel6.TabIndex = 15;
            // 
            // radioButtonSelCluster
            // 
            this.radioButtonSelCluster.AutoSize = true;
            this.radioButtonSelCluster.Location = new System.Drawing.Point(15, 64);
            this.radioButtonSelCluster.Name = "radioButtonSelCluster";
            this.radioButtonSelCluster.Size = new System.Drawing.Size(120, 17);
            this.radioButtonSelCluster.TabIndex = 2;
            this.radioButtonSelCluster.Text = "выбрать кластеры";
            this.radioButtonSelCluster.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllCluster
            // 
            this.radioButtonAllCluster.AutoSize = true;
            this.radioButtonAllCluster.Checked = true;
            this.radioButtonAllCluster.Location = new System.Drawing.Point(15, 41);
            this.radioButtonAllCluster.Name = "radioButtonAllCluster";
            this.radioButtonAllCluster.Size = new System.Drawing.Size(125, 17);
            this.radioButtonAllCluster.TabIndex = 1;
            this.radioButtonAllCluster.TabStop = true;
            this.radioButtonAllCluster.Text = "для всех кластеров";
            this.radioButtonAllCluster.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(300, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Построить график средних значений";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormClustering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 579);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormClustering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormClustering";
            this.Load += new System.EventHandler(this.FormSeasonCoefGraph_Load);
            this.Shown += new System.EventHandler(this.FormSeasonCoefGraph_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClusters)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNumberOfObservations)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistoryIteration)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBeginCenter)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndCenter)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonSave;
        public System.Windows.Forms.DataGridView dataGridViewClusters;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonClose;
        public System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNR;
        public System.Windows.Forms.TextBox textBoxSKO;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.DataGridView dataGridViewNumberOfObservations;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.DataGridView dataGridViewHistoryIteration;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.DataGridView dataGridViewBeginCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.DataGridView dataGridViewEndCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton radioButtonSelCluster;
        private System.Windows.Forms.RadioButton radioButtonAllCluster;
    }
}