/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

using System;
using System.Windows.Forms;
using VMS.StateGraph.Simulation;

namespace MSDES.Chap09.TableTennisGame
{
    public partial class MainFrm : Form
    {
        #region Variables
        private CoupledSimulator _simulator;
        private PlayerA _playerA;
        private PlayerB _playerB;
        private Friend _friend;
        #endregion

        #region Constructors
        public MainFrm()
        {
            InitializeComponent();

            Form.CheckForIllegalCrossThreadCalls = false;
        }
        #endregion

        #region UI Event Handling Methods
        private void btnStart_Click(object sender, EventArgs e)
        {
            initialize();
            run();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the simulation
        /// </summary>
        private void initialize()
        {
            lblPlayerAScore.Text = "0";
            lblPlayerBScore.Text = "0";

            txtSimOut.Text = string.Empty;
            txtSimTime.Text = "0.0";
        }

        /// <summary>
        /// Run the simulation
        /// </summary>
        private void run()
        {
            //Get the players' configurations from the UI
            //- Player A's Configuration
            string paName = "Player A";
            double pa = double.Parse(txtPA.Text);
            double wa = double.Parse(txtWA.Text);
            double aa = double.Parse(txtAA.Text);
            //- Player B's Configuration
            string pbName = "Player B";
            double pb = double.Parse(txtPB.Text);
            double wb = double.Parse(txtWB.Text);
            double ab = double.Parse(txtAB.Text);

            //- Friend's Configuration
            int qt = int.Parse(txtQuitTD.Text);
            
            //Construct Atomic Simulators
            _simulator = new CoupledSimulator();
            _playerA = new PlayerA(paName);
            _playerA.PA = pa;
            _playerA.wa = wa;
            _playerA.aa = aa;

            _playerB = new PlayerB(pbName);
            _playerB.PB = pb;
            _playerB.wb = wb;
            _playerB.ab = ab;

            _friend = new Friend("Friend");
            _friend.qt = qt;

            //Couple the message relations between atomic simulators
            _simulator.AddCoupling("Friend", "Quit", pbName, "Quit");
            _simulator.AddCoupling("Friend", "Quit", pbName, "Quit");
            _simulator.AddCoupling("Friend", "Quit", paName, "Quit");
            _simulator.AddCoupling(paName, "Over", "Friend", "Over");
            _simulator.AddCoupling(pbName, "Over", "Friend", "Over");
            _simulator.AddCoupling(paName, "BallA", pbName, "BallA");
            _simulator.AddCoupling(paName, "OutA", pbName, "OutA");
            _simulator.AddCoupling(pbName, "BallB", paName, "BallB");
            _simulator.AddCoupling(pbName, "OutB", paName, "OutB");

            _simulator.AddModel(_playerA);
            _simulator.AddModel(_playerB);
            _simulator.AddModel(_friend);

            //Add Log Handler and SimulationEnd Handler
            _simulator.Logged += new LogHandler(OnLogged);
            _simulator.SimulationEnded += new SimulationEndEventHandler(OnSimulationEnded);

            //Start the simulation
            _simulator.Start();
        }

        /// <summary>
        /// Print the upated scores
        /// </summary>
        private void updateScores()
        {
            lblPlayerAScore.Text = _playerA.MyScr.ToString();
            lblPlayerBScore.Text = _playerB.MyScr.ToString();
        }

        /// <summary>
        /// Do the stuff when the simulation is ended
        /// </summary>
        private void OnSimulationEnded()
        {
            //MessageBox.Show(this, "Simulation ended.", "Ping Pong");
            printLog("Simulation ended.");
            updateScores();
        }

        /// <summary>
        /// When a log record is received, print the appropriate message to the screen.
        /// </summary>
        /// <param name="sender">Name of an atomic simulator which sends a message</param>
        /// <param name="msg">Content of a message sent</param>
        /// <param name="time">Time when a message is sent</param>
        private void OnLogged(string sender, string msg, double time)
        {
            txtSimTime.Text = time.ToString();
            updateScores();

            if (cbLog.Checked)
            {
                if (msg.StartsWith("MSR(BallA)") || msg.StartsWith("MSR(BallB)"))
                    printLog(string.Format("[{0}] {1} attacks successfully.", time, sender));
                else if (msg.StartsWith("MSR(OutA)"))
                {
                    printLog(string.Format("[{0}] {1} fails to attack.", time, sender));
                    printLog(string.Format("[{0}] Score changes to {1} : {2}", time, _playerA.MyScr, _playerA.UrScr));
                }
                else if (msg.StartsWith("MSR(OutB)"))
                {
                    printLog(string.Format("[{0}] {1} fails to attack.", time, sender));
                    printLog(string.Format("[{0}] Score changes to {1} : {2}", time, _playerB.UrScr , _playerB.MyScr));
                }

                txtSimOut.Focus();
                txtSimOut.ScrollToCaret();
            }
        }

        private void printLog(string log)
        {
            txtSimOut.Text += log + "\r\n";
        }
        #endregion
    }
}
