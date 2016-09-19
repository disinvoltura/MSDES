/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

namespace MSDES.Chap09.TableTennisGame
{
    partial class MainFrm
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
            this.txtSimOut = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblPlayerA = new System.Windows.Forms.Label();
            this.lblPlayerAScore = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSimTime = new System.Windows.Forms.TextBox();
            this.lblPlayerBScore = new System.Windows.Forms.Label();
            this.lblPlayerB = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtWB = new System.Windows.Forms.TextBox();
            this.txtAB = new System.Windows.Forms.TextBox();
            this.txtAA = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtWA = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPA = new System.Windows.Forms.TextBox();
            this.txtPB = new System.Windows.Forms.TextBox();
            this.txtQuitTD = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbLog = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSimOut
            // 
            this.txtSimOut.Location = new System.Drawing.Point(3, 42);
            this.txtSimOut.Multiline = true;
            this.txtSimOut.Name = "txtSimOut";
            this.txtSimOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSimOut.Size = new System.Drawing.Size(413, 114);
            this.txtSimOut.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblPlayerA);
            this.groupBox5.Controls.Add(this.lblPlayerAScore);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.txtSimTime);
            this.groupBox5.Controls.Add(this.lblPlayerBScore);
            this.groupBox5.Controls.Add(this.lblPlayerB);
            this.groupBox5.Location = new System.Drawing.Point(413, 199);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(426, 65);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Simulation Results";
            // 
            // lblPlayerA
            // 
            this.lblPlayerA.Location = new System.Drawing.Point(167, 19);
            this.lblPlayerA.Name = "lblPlayerA";
            this.lblPlayerA.Size = new System.Drawing.Size(55, 31);
            this.lblPlayerA.TabIndex = 1;
            this.lblPlayerA.Text = "PlayerA";
            this.lblPlayerA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPlayerAScore
            // 
            this.lblPlayerAScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPlayerAScore.Location = new System.Drawing.Point(229, 19);
            this.lblPlayerAScore.Name = "lblPlayerAScore";
            this.lblPlayerAScore.Size = new System.Drawing.Size(29, 31);
            this.lblPlayerAScore.TabIndex = 0;
            this.lblPlayerAScore.Text = "0";
            this.lblPlayerAScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(13, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 21);
            this.label11.TabIndex = 9;
            this.label11.Text = "Clock:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(281, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(9, 31);
            this.label6.TabIndex = 0;
            this.label6.Text = ":";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSimTime
            // 
            this.txtSimTime.BackColor = System.Drawing.SystemColors.Info;
            this.txtSimTime.Location = new System.Drawing.Point(67, 22);
            this.txtSimTime.Name = "txtSimTime";
            this.txtSimTime.ReadOnly = true;
            this.txtSimTime.Size = new System.Drawing.Size(80, 23);
            this.txtSimTime.TabIndex = 11;
            // 
            // lblPlayerBScore
            // 
            this.lblPlayerBScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPlayerBScore.Location = new System.Drawing.Point(316, 19);
            this.lblPlayerBScore.Name = "lblPlayerBScore";
            this.lblPlayerBScore.Size = new System.Drawing.Size(29, 31);
            this.lblPlayerBScore.TabIndex = 0;
            this.lblPlayerBScore.Text = "0";
            this.lblPlayerBScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPlayerB
            // 
            this.lblPlayerB.Location = new System.Drawing.Point(351, 19);
            this.lblPlayerB.Name = "lblPlayerB";
            this.lblPlayerB.Size = new System.Drawing.Size(54, 31);
            this.lblPlayerB.TabIndex = 1;
            this.lblPlayerB.Text = "PlayerB";
            this.lblPlayerB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.txtQuitTD);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(395, 215);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Player Configuration";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.3274F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.6726F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtWB, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtAB, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtAA, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtWA, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label17, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPA, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtPB, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(369, 143);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(5, 75);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(180, 30);
            this.label15.TabIndex = 15;
            this.label15.Text = "Attack Time Delay:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(193, 2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 33);
            this.label13.TabIndex = 13;
            this.label13.Text = "Player A";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(5, 37);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(180, 36);
            this.label14.TabIndex = 1;
            this.label14.Text = "Probability of an attack success:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtWB
            // 
            this.txtWB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWB.Location = new System.Drawing.Point(281, 110);
            this.txtWB.Name = "txtWB";
            this.txtWB.Size = new System.Drawing.Size(83, 23);
            this.txtWB.TabIndex = 2;
            this.txtWB.Text = "5.0";
            // 
            // txtAB
            // 
            this.txtAB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAB.Location = new System.Drawing.Point(281, 78);
            this.txtAB.Name = "txtAB";
            this.txtAB.Size = new System.Drawing.Size(83, 23);
            this.txtAB.TabIndex = 2;
            this.txtAB.Text = "0.8";
            // 
            // txtAA
            // 
            this.txtAA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAA.Location = new System.Drawing.Point(193, 78);
            this.txtAA.Name = "txtAA";
            this.txtAA.Size = new System.Drawing.Size(80, 23);
            this.txtAA.TabIndex = 2;
            this.txtAA.Text = "0.8";
            // 
            // label16
            // 
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(5, 107);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(180, 34);
            this.label16.TabIndex = 16;
            this.label16.Text = "Waiting Time Delay:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtWA
            // 
            this.txtWA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWA.Location = new System.Drawing.Point(193, 110);
            this.txtWA.Name = "txtWA";
            this.txtWA.Size = new System.Drawing.Size(80, 23);
            this.txtWA.TabIndex = 2;
            this.txtWA.Text = "5.0";
            // 
            // label17
            // 
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(281, 2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 33);
            this.label17.TabIndex = 17;
            this.label17.Text = "Player B";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPA
            // 
            this.txtPA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPA.Location = new System.Drawing.Point(193, 40);
            this.txtPA.Name = "txtPA";
            this.txtPA.Size = new System.Drawing.Size(80, 23);
            this.txtPA.TabIndex = 2;
            this.txtPA.Text = "0.9";
            // 
            // txtPB
            // 
            this.txtPB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPB.Location = new System.Drawing.Point(281, 40);
            this.txtPB.Name = "txtPB";
            this.txtPB.Size = new System.Drawing.Size(83, 23);
            this.txtPB.TabIndex = 2;
            this.txtPB.Text = "0.9";
            // 
            // txtQuitTD
            // 
            this.txtQuitTD.Location = new System.Drawing.Point(177, 176);
            this.txtQuitTD.Name = "txtQuitTD";
            this.txtQuitTD.Size = new System.Drawing.Size(70, 23);
            this.txtQuitTD.TabIndex = 7;
            this.txtQuitTD.Text = "800";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 180);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(136, 15);
            this.label12.TabIndex = 6;
            this.label12.Text = "Friend\'s quit time-delay";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbLog);
            this.groupBox4.Controls.Add(this.txtSimOut);
            this.groupBox4.Location = new System.Drawing.Point(411, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(428, 169);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Simulation Log";
            // 
            // cbLog
            // 
            this.cbLog.AutoSize = true;
            this.cbLog.Location = new System.Drawing.Point(340, 17);
            this.cbLog.Name = "cbLog";
            this.cbLog.Size = new System.Drawing.Size(76, 19);
            this.cbLog.TabIndex = 1;
            this.cbLog.Text = "Show Log";
            this.cbLog.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(5, 226);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(395, 40);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 280);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFrm";
            this.Text = "Table Tennis Game State Graph Simulator";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSimOut;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblPlayerA;
        private System.Windows.Forms.Label lblPlayerAScore;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPlayerBScore;
        private System.Windows.Forms.Label lblPlayerB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtQuitTD;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPA;
        private System.Windows.Forms.TextBox txtAA;
        private System.Windows.Forms.TextBox txtWA;
        private System.Windows.Forms.TextBox txtPB;
        private System.Windows.Forms.TextBox txtAB;
        private System.Windows.Forms.TextBox txtWB;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSimTime;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox cbLog;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
    }
}

