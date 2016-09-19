/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

using System;

namespace MSDES.Chap10.ThreeStageTandemLine
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
        /// Markings for the parameterized queue B
        /// </summary>
        public int[] B;
        /// <summary>
        /// Markings for the parameterized queue M
        /// </summary>
        public int[] M;

        /// <summary>
        /// Candidate Actvity List
        /// </summary>
        private ActivityList CAL;
        /// <summary>
        /// Future Event List
        /// </summary>
        private EventList FEL;
        #endregion

        #region Member Variables for Collecting Statistics
        public double[] SumQ;
        public double[] Before;
        public double[] AQL;
        #endregion

        #region Member Variables for Random Variate Generation
        /// <summary>
        /// Pseudo Random Variate Generator for uniform(0,1)
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
            CAL = new ActivityList(); //initialize the candidate activity list 
            FEL = new EventList(); //initialize the future event list
            Logs = string.Empty;
            R = new Random();
            Event nextEvent = null;

            Clock = 0;
            Execute_Initialize_routine(Clock);

            //Simulation            
            do
            {
                //2. Scanning Phase
                while (!CAL.IsEmpty()) {
                    Activity activity = Get_Activity();
                    switch (activity.Name) {
                        case "CREATE":  {
                                Execute_CREATE_activity_routine(Clock);
                                break;
                            }
                        case "SERVE": {
                                Execute_SERVE_activity_routine(Clock, activity.K);
                                break;
                            }                        
                    }
                    Log(1, Math.Round(Clock, 2), activity.ToString(), "", C, B[1], B[2], B[3], M[1], M[2], M[3], CAL.ToString(), FEL.ToString());
                }//end of while

                //3. Timing Phase
                //get the first event from FEL
                nextEvent = Retrieve_Event();
                //advance simulation clock
                Clock = nextEvent.Time;
                Log(2, Math.Round(Clock, 2), "", "", C, B[1], B[2], B[3], M[1], M[2], M[3], CAL.ToString(), FEL.ToString());

                //4. Executing Phase
                switch (nextEvent.Name) {
                    case "CREATED": {
                        Execute_CREATED_event_routine();
                        break;
                    }
                    case "SERVED": {
                        Execute_SERVED_event_routine(nextEvent.K);
                        break;
                    }
                } // end of switch-case
                Log(3, Math.Round(Clock, 2), "", nextEvent.ToString(), C, B[1], B[2], B[3], M[1], M[2], M[3], CAL.ToString(), FEL.ToString());
            } while (Clock < eosTime);

            //5. Statistics Collection Phase
            Execute_Statistics_routine(Clock);            
        }

        /// <summary>
        /// Log the current system state
        /// </summary>
        private void Log(int phase, double clock, string curActivity, string curEvent, double c, double b1, double b2, double b3, int m1, int m2, int m3, string cal, string fel)
        {
            Logs += string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\r\n", phase, Math.Round(clock, 2), curActivity, curEvent, c, b1, b2, b3, m1,m2, m3, cal, fel);
        }

        /// <summary>
        /// Initialize the simulation
        /// </summary>
        private void Execute_Initialize_routine(double clock)
        {
            //Initialize state variables and statistics variables
            C = 1;
            B = new int[4]; M = new int[4];
            Before = new double[4]; SumQ = new double[4];

            for (int k = 1; k <= 3; k++) {
                B[k] = 0; M[k] = 1;
                Before[k] = 0; SumQ[k] = 0;
            }

            //Store the initially enabled activity into CAL
            Store_Activity("CREATE"); 
        }

        private void Execute_Statistics_routine(double clock)
        {
            AQL = new double[4];
            for (int k = 1; k <= 3; k++)
            {
                SumQ[k] += B[k] * (clock - Before[k]);
                AQL[k] = SumQ[k] / clock;
            }
        }
        #endregion

        #region Activity List Handling Methods
        private void Store_Activity(string name)
        {
            CAL.AddActivity(name);
        }

        private void Store_Activity(string name, int k)
        {
            CAL.AddActivity(name, k);
        }

        private Activity Get_Activity()
        {
            return CAL.NextActivity();
        }
        #endregion

        #region Event List Handling Methods
        private void Schedule_Event(string name, double time, int k)
        {
            FEL.AddEvent(name, time, k);
        }

        private void Schedule_Event(string name, double time)
        {
            FEL.AddEvent(name, time);
        }

        private Event Retrieve_Event()
        {
            return FEL.NextEvent();
        }
        #endregion

        #region activity routine methods
        private void Execute_CREATE_activity_routine(double clock)
        {
            if (C>0){ //check the at-begin condition
                C--; //at-begin action

                double ta = Exp(10);
                Schedule_Event("CREATED", clock + ta); //Schedule the BTO-event
            }
        }

        private void Execute_SERVE_activity_routine(double clock, int k)
        {
            if ((B[k] > 0) && (M[k] > 0)) //check the at-begin condition
            {
                SumQ[k] += B[k] * (Clock - Before[k]); Before[k] = Clock; //Collect statistics

                B[k]--; M[k]--; // at-begin action

                double ts = (k == 1 ? 1 : 0) * Uni(10, 15) + (k == 2 ? 1 : 0) * Uni(13, 18) + (k == 3 ? 1 : 0) * Uni(8, 13);
                Schedule_Event("SERVED", clock + ts, k); //Schedule the BTO-event
            }
        }
        #endregion

        #region event routine methods
        private void Execute_CREATED_event_routine()
        {
            if (true) 
            {
                C++;//at-end action
                Store_Activity("CREATE"); //store influenced activity
            }

            if (true)
            {
                SumQ[1] += B[1] * (Clock - Before[1]); Before[1] = Clock; //Collect statistics
                
                B[1]++; //at-end action
                Store_Activity("SERVE", 1); //store influenced activity
            }
        }

        private void Execute_SERVED_event_routine(int k)
        {
            if (true)
            {
                M[k]++; //at-end action
                Store_Activity("SERVE", k); //store influenced activity
            }
            if (k < 3)
            {
                SumQ[k + 1] += B[k + 1] * (Clock - Before[k + 1]); Before[k + 1] = Clock; //Collect statistics

                B[k + 1]++; //at-end action
                Store_Activity("SERVE", k + 1); //store influenced activity
            }
        }
        #endregion

        #region Random Variate Generation Methods
        private double Exp(double a)
        {
            if (a <= 0) throw new Exception("Negative value is not allowed");
            double u = R.NextDouble();
            return (-a * Math.Log(u));
        }

        private double Uni(double a, double b)
        {
            if (a >= b) throw new Exception("The range is not valid.");
            double u = R.NextDouble();
            return (a + (b - a) * u);
        }

        #endregion
    }
}
