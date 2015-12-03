namespace TSP
{
    partial class mainform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainform));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tbProblemSize = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cboMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tbCostOfTour = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tbTimeToSolve = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.tbPrunedStates = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.tbStoredStates = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.AlgorithmMenu2 = new System.Windows.Forms.ToolStripSplitButton();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yourTSPToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.randomToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greedyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProblem = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbSeed = new System.Windows.Forms.ToolStripTextBox();
            this.randomProblem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.Solutions = new System.Windows.Forms.ToolStripTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.tbBSSFUpdates = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel9 = new System.Windows.Forms.ToolStripLabel();
            this.tbStatesCreated = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel10 = new System.Windows.Forms.ToolStripLabel();
            this.tbOptimal = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tbProblemSize,
            this.toolStripSeparator5,
            this.cboMode,
            this.toolStripSeparator6,
            this.toolStripLabel3,
            this.tbCostOfTour,
            this.toolStripSeparator4,
            this.toolStripLabel4,
            this.tbTimeToSolve,
            this.toolStripLabel6,
            this.tbPrunedStates,
            this.toolStripLabel7,
            this.tbStoredStates,
            this.toolStripLabel8,
            this.tbBSSFUpdates,
            this.toolStripLabel9,
            this.tbStatesCreated});
            this.toolStrip1.Location = new System.Drawing.Point(0, 570);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(963, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(75, 22);
            this.toolStripLabel2.Text = "Problem Size";
            // 
            // tbProblemSize
            // 
            this.tbProblemSize.Name = "tbProblemSize";
            this.tbProblemSize.Size = new System.Drawing.Size(50, 25);
            this.tbProblemSize.Text = "20";
            this.tbProblemSize.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbProblemSize.Leave += new System.EventHandler(this.tbProblemSize_Leave);
            this.tbProblemSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbProblemSize_KeyDown);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // cboMode
            // 
            this.cboMode.Items.AddRange(new object[] {
            "Easy",
            "Normal",
            "Hard"});
            this.cboMode.Name = "cboMode";
            this.cboMode.Size = new System.Drawing.Size(75, 25);
            this.cboMode.Text = "Hard";
            this.cboMode.SelectedIndexChanged += new System.EventHandler(this.cboMode_SelectedIndexChanged);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(70, 22);
            this.toolStripLabel3.Text = "Cost of tour";
            // 
            // tbCostOfTour
            // 
            this.tbCostOfTour.Enabled = false;
            this.tbCostOfTour.Name = "tbCostOfTour";
            this.tbCostOfTour.Size = new System.Drawing.Size(100, 25);
            this.tbCostOfTour.Text = "--";
            this.tbCostOfTour.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel4.Text = "Solved in ";
            // 
            // tbTimeToSolve
            // 
            this.tbTimeToSolve.Enabled = false;
            this.tbTimeToSolve.Name = "tbTimeToSolve";
            this.tbTimeToSolve.Size = new System.Drawing.Size(100, 25);
            this.tbTimeToSolve.Text = "--";
            this.tbTimeToSolve.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(79, 22);
            this.toolStripLabel6.Text = "Pruned States";
            // 
            // tbPrunedStates
            // 
            this.tbPrunedStates.Enabled = false;
            this.tbPrunedStates.Name = "tbPrunedStates";
            this.tbPrunedStates.Size = new System.Drawing.Size(100, 25);
            this.tbPrunedStates.Text = "--";
            this.tbPrunedStates.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(94, 22);
            this.toolStripLabel7.Text = "MaxStoredStates";
            // 
            // tbStoredStates
            // 
            this.tbStoredStates.Enabled = false;
            this.tbStoredStates.Name = "tbStoredStates";
            this.tbStoredStates.Size = new System.Drawing.Size(100, 25);
            this.tbStoredStates.Text = "--";
            this.tbStoredStates.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AlgorithmMenu2,
            this.newProblem,
            this.toolStripLabel1,
            this.tbSeed,
            this.randomProblem,
            this.toolStripSeparator1,
            this.toolStripLabel5,
            this.Solutions,
            this.toolStripLabel10,
            this.tbOptimal});
            this.toolStrip2.Location = new System.Drawing.Point(0, 545);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(963, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // AlgorithmMenu2
            // 
            this.AlgorithmMenu2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AlgorithmMenu2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dToolStripMenuItem,
            this.yourTSPToolStripMenuItem1,
            this.randomToolStripMenuItem1,
            this.bBToolStripMenuItem,
            this.greedyToolStripMenuItem});
            this.AlgorithmMenu2.Image = ((System.Drawing.Image)(resources.GetObject("AlgorithmMenu2.Image")));
            this.AlgorithmMenu2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AlgorithmMenu2.Name = "AlgorithmMenu2";
            this.AlgorithmMenu2.Size = new System.Drawing.Size(77, 22);
            this.AlgorithmMenu2.Text = "Algorithm";
            this.AlgorithmMenu2.ButtonClick += new System.EventHandler(this.AlgorithmMenu2_ButtonClick_1);
            this.AlgorithmMenu2.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.AlgorithmMenu2_DropDownItemClicked);
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.dToolStripMenuItem.Text = "Default";
            this.dToolStripMenuItem.Click += new System.EventHandler(this.dToolStripMenuItem_Click);
            // 
            // yourTSPToolStripMenuItem1
            // 
            this.yourTSPToolStripMenuItem1.Name = "yourTSPToolStripMenuItem1";
            this.yourTSPToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.yourTSPToolStripMenuItem1.Text = "Your TSP";
            this.yourTSPToolStripMenuItem1.Click += new System.EventHandler(this.yourTSPToolStripMenuItem1_Click);
            // 
            // randomToolStripMenuItem1
            // 
            this.randomToolStripMenuItem1.Name = "randomToolStripMenuItem1";
            this.randomToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.randomToolStripMenuItem1.Text = "Random";
            this.randomToolStripMenuItem1.Click += new System.EventHandler(this.randomToolStripMenuItem1_Click);
            // 
            // bBToolStripMenuItem
            // 
            this.bBToolStripMenuItem.Name = "bBToolStripMenuItem";
            this.bBToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.bBToolStripMenuItem.Text = "B and B";
            this.bBToolStripMenuItem.Click += new System.EventHandler(this.bBToolStripMenuItem_Click);
            // 
            // greedyToolStripMenuItem
            // 
            this.greedyToolStripMenuItem.Name = "greedyToolStripMenuItem";
            this.greedyToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.greedyToolStripMenuItem.Text = "Greedy";
            this.greedyToolStripMenuItem.Click += new System.EventHandler(this.greedyToolStripMenuItem_Click);
            // 
            // newProblem
            // 
            this.newProblem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newProblem.Image = ((System.Drawing.Image)(resources.GetObject("newProblem.Image")));
            this.newProblem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newProblem.Name = "newProblem";
            this.newProblem.Size = new System.Drawing.Size(83, 22);
            this.newProblem.Text = "New Problem";
            this.newProblem.Click += new System.EventHandler(this.newProblem_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel1.Text = "Seed";
            // 
            // tbSeed
            // 
            this.tbSeed.Name = "tbSeed";
            this.tbSeed.Size = new System.Drawing.Size(100, 25);
            // 
            // randomProblem
            // 
            this.randomProblem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.randomProblem.Image = ((System.Drawing.Image)(resources.GetObject("randomProblem.Image")));
            this.randomProblem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.randomProblem.Name = "randomProblem";
            this.randomProblem.Size = new System.Drawing.Size(104, 22);
            this.randomProblem.Text = "Random Problem";
            this.randomProblem.Click += new System.EventHandler(this.randomProblem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(61, 22);
            this.toolStripLabel5.Text = "Solution #";
            // 
            // Solutions
            // 
            this.Solutions.Enabled = false;
            this.Solutions.Name = "Solutions";
            this.Solutions.Size = new System.Drawing.Size(15, 25);
            this.Solutions.Text = "--";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(638, 618);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(78, 15);
            this.toolStripLabel8.Text = "BSSF Updates";
            // 
            // tbBSSFUpdates
            // 
            this.tbBSSFUpdates.Name = "tbBSSFUpdates";
            this.tbBSSFUpdates.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripLabel9
            // 
            this.toolStripLabel9.Name = "toolStripLabel9";
            this.toolStripLabel9.Size = new System.Drawing.Size(82, 15);
            this.toolStripLabel9.Text = "States Created";
            // 
            // tbStatesCreated
            // 
            this.tbStatesCreated.Name = "tbStatesCreated";
            this.tbStatesCreated.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripLabel10
            // 
            this.toolStripLabel10.Name = "toolStripLabel10";
            this.toolStripLabel10.Size = new System.Drawing.Size(102, 22);
            this.toolStripLabel10.Text = "Optimal Solution?";
            // 
            // tbOptimal
            // 
            this.tbOptimal.Name = "tbOptimal";
            this.tbOptimal.Size = new System.Drawing.Size(100, 25);
            // 
            // mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 595);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "mainform";
            this.Text = "Traveling Sales Person";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tbProblemSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        public System.Windows.Forms.ToolStripTextBox tbCostOfTour;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        public System.Windows.Forms.ToolStripTextBox tbStoredStates;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripComboBox cboMode;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton newProblem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tbSeed;
        private System.Windows.Forms.ToolStripButton randomProblem;
        private System.Windows.Forms.ToolStripSplitButton AlgorithmMenu2;
        private System.Windows.Forms.ToolStripMenuItem yourTSPToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem randomToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greedyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        public System.Windows.Forms.ToolStripTextBox Solutions;
        public System.Windows.Forms.ToolStripTextBox tbTimeToSolve;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ToolStripTextBox tbPrunedStates;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripLabel toolStripLabel8;
        private System.Windows.Forms.ToolStripLabel toolStripLabel9;
        public System.Windows.Forms.ToolStripTextBox tbBSSFUpdates;
        public System.Windows.Forms.ToolStripTextBox tbStatesCreated;
        private System.Windows.Forms.ToolStripLabel toolStripLabel10;
        public System.Windows.Forms.ToolStripTextBox tbOptimal;



    }
}

