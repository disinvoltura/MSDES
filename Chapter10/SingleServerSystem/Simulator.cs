/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

using System;

namespace MSDES.Chap10.SingleServerSystem
{
    public class Simulator
    {
        #region Member Variables
        /// <summary>
        /// Simulation Clock
        /// </summary>
        public double Clock;
        /// <summary>
        /// Marking for the queue C
        /// </summary>
        public int C;
        /// <summary>
        /// Markings for the queue Q
        /// </summary>
        public int Q;
        /// <summary>
        /// Markings for the queue M
        /// </summary>
        public int M;

        /// <summary>
        /// Candidate Activity List
        /// </summary>
        private ActivityList CAL;
        /// <summary>
        /// Future Event List
        /// </summary>
        private EventList FEL;
        #endregion

        #region Member Variables for Collecting Statistics
        private double SumQ;
        private double Before;
        private double AQL;

        public double AverageQueueLength {
            get { return this.AQL; }
        }
        #endregion

        #region Member Variables for Random Variate Generation
        /// <summary>
        /// Pseudo Random Variate Generator for uniform distribution
        /// </summary>
        private Random R;
        #endregion

        #region Member Variables for Logging
        public string Logs;
        #endregion

        #region Constructors
        public Simulator()
        {
            
        }
        #endregion

        #region run method
        public void Run(double eosTime)
        {
            //1. Initialization Phase 
            CAL = new ActivityList();
            FEL = new EventList();
            Logs = string.Empty;
            R = new Random();
            
            Clock = 0;
            Execute_Initialize_routine(Clock);
            
            //Simulation
            Event nextEvent = null;
            do {
                //2. Scanning phase
                while (!CAL.IsEmpty()) {
                    string ACTIVITY = Get_Activity();
                    switch (ACTIVITY) {
                        case "Create": {
                                Execute_Create_activity_routine(Clock);
                                break;
                            }
                        case "Process": {
                                Execute_Process_activity_routine(Clock);
                                break;
                            }                        
                    }

                    Log(1, Math.Round(Clock, 2), ACTIVITY, "", C, Q, M, CAL.ToString(), FEL.ToString());
                }//end of while

                //3. Timing phase
                //get the first event from FEL
                nextEvent = Retrieve_Event();
                //advance simulation clock
                Clock = nextEvent.Time;
                Log(2, Math.Round(Clock, 2), "", "", C, Q, M, CAL.ToString(), FEL.ToString());

                //4. Executing phase
                switch (nextEvent.Name) {
                    case "Created": {
                        Execute_Created_event_routine();
                        break;
                    }
                    case "Processed": {
                        Execute_Processed_event_routine();
                        break;
                    }
                } // end of switch-case
                Log(3, Math.Round(Clock, 2), "", nextEvent.Name, C, Q, M, CAL.ToString(), FEL.ToString());

            } while (Clock < eosTime);

            //5. Statistics collection phase
            Execute_Statistics_routine(Clock);
        }
                
        /// <summary>
        /// Log the current system state
        /// </summary>
        private void Log(int phase, double clock, string curActivity, string curEvent, double c, double q, int m, string cal, string fel)
        {
            Logs += string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\r\n", phase, Math.Round(clock, 2), curActivity, curEvent, c, q, m, cal, fel);
        }

        #endregion

        #region Activity List Handling Methods
        private void Store_Activity(string name) {
            CAL.AddActivity(name);
        }

        private string Get_Activity() {
            Activity act = CAL.NextActivity();
            return act.Name;
        }

        #endregion

        #region Event List Handling Methods
        private void Schedule_Event(string name, double time) {
            FEL.AddEvent(name, time);
        }

        private Event Retrieve_Event() {
            Event nextEvent = null;
            nextEvent = FEL.NextEvent();
            return nextEvent;
        }
        #endregion

        #region activity routine methods
        private void Execute_Create_activity_routine(double clock) {
            if (C > 0) { //check the at-begin condition
                C--; //at-begin action

                double ta = Exp(5);
                Schedule_Event("Created", clock + ta); //Schedule the BTO-event
            }
        }

        private void Execute_Process_activity_routine(double clock) {
            if ((M > 0) && (Q > 0)) { //check the at-begin condition
                SumQ += Q * (Clock - Before); Before = Clock; //Collect statistics

                M--; Q--;// at-begin action
                double ts = Uni(4, 6);
                Schedule_Event("Processed", clock + ts); //Schedule the BTO-event
            }
        }
        #endregion

        #region event routine methods
        private void Execute_Initialize_routine(double clock)
        {
            //Initialize state variables (markings for queues)
            C = 1; M = 1; Q = 0;

            //Initialize statistics variables
            Before = 0; SumQ = 0;

            //Store the initially enabled activity into CAL
            Store_Activity("Create");
        }

        private void Execute_Statistics_routine(double clock)
        {
            SumQ += Q * (clock - Before);
            AQL = SumQ / clock;
        }

        private void Execute_Created_event_routine() {
            if (true) {
                C++;//at-end action
                Store_Activity("Create"); //store influenced activity
            }

            if (true) {
                SumQ += Q * (Clock - Before); Before = Clock;//Collect statistics
                
                Q++; //at-end action
                Store_Activity("Process"); //store influenced activity
            }
        }

        private void Execute_Processed_event_routine() {
            if (true) {
                M++; //at-end action
                Store_Activity("Process"); //store influenced activity
            }
        }
        #endregion

        #region Random Variate Generation Methods
        /// <summary>	
        /// Returns a random value that follows the exponential 
        /// distribution with a given mean of a
        /// </summary>
        /// <param name="a">A mean value</param>
        /// <returns>Exponential random value </returns>
        private double Exp(double a) {
            if (a <= 0) 
                throw new Exception("Negative value is not allowed");
            double u = R.NextDouble();
            return (-a * Math.Log(u));
        }

        /// <summary>
        /// Returns a random value that follows the uniform distribution 
        /// with a given range of a and b
        /// </summary>	
        /// <param name="a">Start range</param>
        /// <param name="b">End range</param>
        /// <returns>Uniform random value</returns>
        private double Uni(double a, double b) {
            if (a >= b) 
                throw new Exception("The range is not valid.");
            double u = R.NextDouble();
            return (a + (b - a) * u);
        }
        #endregion
    }
}
