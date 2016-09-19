/*
 * Copyright (c) Donghun Kang and Byoung K. Choi.  
 * This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MSDES.Chap05.SimpleJobShop
{
    public partial class MainFrm : Form
    {        
        public static MainFrm App;

        public MainFrm()
        {
            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
            MainFrm.App = this;
        }

        //Start Simulation and Print out the output
        private void button1_Click(object sender, EventArgs e)
        {
            //Initilaize the output window
            Simulator sim = new Simulator();
            textBox1.Text = "";
            listView1.Items.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Name = "Q[0]";
            chart1.Series[0].Color = Color.Red;

            chart1.Series[1].Points.Clear();
            chart1.Series[1].Name = "Q[1]";
            chart1.Series[1].Color = Color.Yellow;

            chart1.Series[2].Points.Clear();
            chart1.Series[2].Name = "Q[2]";
            chart1.Series[2].Color = Color.Blue;

            chart1.Series[3].Points.Clear();
            chart1.Series[3].Name = "Q[3]";
            chart1.Series[3].Color = Color.Green;

            //Simulate and catch exception
            try
            {
                sim.Run(1000);
            }
            catch (Exception ex)
            {
                textBox1.Text += ex.ToString();
            }

            //Average Queue lengths of the stations
            double[] AQL = sim.AverageQueueLengths;
            
            //Print out the statistics
            textBox1.Text += "=========Statistics=========\r\n";
            for (int i = 0; i < AQL.Length; i++)
            {
                AQL[i] = (Math.Round(AQL[i] * 100)) / 100.0;

                textBox1.Text += "AQL of Queue " + (i) + " : " + AQL[i].ToString() + " \r\n";
            }

            //Set the grid of X-axis
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = sim.Clock;
            chart1.ChartAreas[0].AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
        }

        //Add Event Trajectory
        public void AddTrajectory(double time, string name, int jobtype, int processingstep, int station, Queue<int[]>[] Q1, int[] M, string fel)
        {
            ListViewItem item = new ListViewItem(time.ToString());
            item.SubItems.Add(name);
            if (jobtype == -1)
                item.SubItems.Add("-");
            else 
                item.SubItems.Add(jobtype.ToString());
            if (processingstep == -1)
                item.SubItems.Add("-");
            else 
                item.SubItems.Add(processingstep.ToString());
            if (station == -1)
                item.SubItems.Add("-");
            else 
               item.SubItems.Add(station.ToString());
            

            for (int i = 1; i < Q1.Length; i++)
            {
                item.SubItems.Add(Q1[i].Count.ToString());
            }
            for (int i = 1; i < M.Length; i++)
            {
                item.SubItems.Add(M[i].ToString());
            } 
            item.SubItems.Add(fel);

            listView1.Items.Add(item);

        }

        //Add a point in the Chart
        public void AddChart(double x, int y1, int y2, int y3, int y4)
        {
            chart1.Series[0].Points.AddXY(x, y1);
            chart1.Series[1].Points.AddXY(x, y2);
            chart1.Series[2].Points.AddXY(x, y3);
            chart1.Series[3].Points.AddXY(x, y4);
        }
    }
}
