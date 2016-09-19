/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

using System;
using System.Windows.Forms;

namespace MSDES.Chap10.ThreeStageTandemLine
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            lvTrajectory.Items.Clear();
            txtAQL.Text = string.Empty;

            //run the simulation
            Simulator simulator = new Simulator();
            simulator.Run(1000);

            //Print Average Queue Lengths
            for (int i = 1; i <= 3; i++)
            {
                txtAQL.AppendText("AQL[" + i + "]= " + simulator.AQL[i] + "\r\n");
            }

            //Print System Trajectory
            string[] logs = simulator.Logs.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < logs.Length; i++)
            {
                string[] elements = logs[i].Split('\t');
                ListViewItem item = new ListViewItem(elements[0]);

                for (int j = 1; j < elements.Length; j++)
                {
                    item.SubItems.Add(elements[j]);
                }
                lvTrajectory.Items.Add(item);
            }

            lvTrajectory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}
