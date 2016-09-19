/*
 * Copyright (c) Donghun Kang and Byoung K. Choi.  
 * This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
 */

using System;
using System.Windows.Forms;

namespace MSDES.Chap05.TandemLine
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

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        //Start Simulation and Print out the output
        private void button1_Click(object sender, EventArgs e)
        {
            //Initialize the output window
            Simulator sim = new Simulator();
            textBox1.Text = "";
            listView1.Items.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Name = "Q[1]";
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;
            chart1.ChartAreas[0].AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;

            chart1.Series[1].Points.Clear();
            chart1.Series[1].Name = "Q[2]";

            chart1.Series[2].Points.Clear();
            chart1.Series[2].Name = "Q[3]";

            //Simulate and catch exception
            try
            {
                sim.Run(500);
            }
            catch (Exception ex)
            {
                textBox1.Text += ex.ToString();
            }

            //Average Queue lengths of the stations
            double[] AQL = sim.AverageQueueLengths;

            //Print out the statistics
            textBox1.Text += "=========Statistics=========\r\n";
            for (int i = 1; i < AQL.Length; i++)
            {
                AQL[i] = (Math.Round(AQL[i] * 100)) / 100.0;
                textBox1.Text += "AQL of Queue " + (i) + " : " + AQL[i].ToString() + " \r\n";
            }

            //Set the grid of X-axis
            chart1.ChartAreas[0].AxisX.Maximum = sim.Clock;
            chart1.ChartAreas[0].AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
        }

        //Add Event Trajectory
        public void AddTrajectory(double time, string name, int parameter, int[] Q, int[] M, string fel)
        {
            ListViewItem item = new ListViewItem(time.ToString());
            item.SubItems.Add(name);
            item.SubItems.Add(parameter.ToString());
            

            for (int i = 1; i < Q.Length; i++)
            {
                item.SubItems.Add(Q[i].ToString());
            }
            for (int i = 1; i < M.Length; i++)
            {
                item.SubItems.Add(M[i].ToString());
            } 
            item.SubItems.Add(fel);

            listView1.Items.Add(item);

        }

        //Add a point in the Chart
        public void AddChart(double x, int[] ys)
        {
            chart1.Series[0].Points.AddXY(x, ys[1]);
            chart1.Series[1].Points.AddXY(x, ys[2]);
            chart1.Series[2].Points.AddXY(x, ys[3]);
            
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }


    }
}
