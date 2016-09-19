/*
 * Copyright (c) Donghun Kang and Byoung K. Choi.  
 * This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
 */

using System;
using System.Collections.Generic;

namespace MSDES.Chap05.SimpleJobShop
{
    public class Simulator
    {
        #region Member Variables for State Variables
        /// <summary>
        /// Number of Available Machines at each station
        /// </summary>
        private int[] M;
        /// <summary>
        /// Queue (List of job(j,p)) at each station
        /// </summary>
        private Queue<int[]>[] Q;
        /// <summary>
        /// Current Job Type at each station
        /// </summary>
        private int[] JT;
        /// <summary>
        /// Routes of each job type
        /// </summary>
        private int[,] route;
        /// <summary>
        /// Procesing Time for a job having job type k and processing-step p
        /// </summary>
        private double[,] t;
        /// <summary>
        /// Transport Delay from Station s to Station ns
        /// </summary>
        private int[,] delay;
        #endregion

        #region Member Variables for Simulator Objects
        /// <summary>
        /// Simulation Clock
        /// </summary>
        private double CLK;
        /// <summary>
        /// Future Event List
        /// </summary>
        private EventList FEL;
        #endregion

        #region Member Variables for Statistics
        /// <summary>
        /// Lastly Collected Times 
        /// </summary>
        private double[] Before;
        /// <summary>
        /// Accumulated Values of Queue Length Changes over Time
        /// </summary>
        private double[] SumQ; 
        /// <summary>
        /// Average Queue Lengths 
        /// </summary>
        private double[] AQL;
        #endregion

        /// <summary>
        /// Pseudo Random Value Generator 
        /// </summary>
        private Random R;

        #region Properties
        /// <summary>
        /// Current Simulation Clock
        /// </summary>
        public double Clock
        {
            get { return this.CLK; }
        }

        /// <summary>
        /// Average queue length at each stage
        /// </summary>
        /// <returns>the average queue length</returns>
        public double[] AverageQueueLengths
        {
            get { return AQL; }
        }
        #endregion

        #region Constructors
        public Simulator()
        {
        }
        #endregion

        #region Methods for Main Program
        /// <summary>
        /// Run the simulation using next-event scheduling algorithm (Main Program)
        /// </summary>
        public void Run(double eosTime)
        {
            //1. Initialization phase
            FEL = new EventList();
            R = new Random();
            Event nextEvent = new Event();
            CLK = 0.0;
            
            Execute_Initialize_routine(CLK);

            while (CLK < eosTime) {
                //2. Time-flow mechanism phase
                nextEvent = Retrieve_Event();
                CLK = nextEvent.Time;

                //3. Event-routine execution phase
                switch (nextEvent.Name) {   
                    case "Arrive": {
                        Execute_Arrive_event_routine(nextEvent.J, nextEvent.P, nextEvent.S, CLK); break;
                    }
                    case "Move": {
                        Execute_Move_event_routine(nextEvent.J, nextEvent.P, nextEvent.S, CLK); break; }
                    case "Enter": {
                        Execute_Enter_event_routine(nextEvent.J, nextEvent.P,nextEvent.S, CLK); break;}
                    case "Load":{
                        Execute_Load_event_routine(nextEvent.J, nextEvent.P, nextEvent.S, CLK); break;
                    }
                    case "Unload":{
                        Execute_Unload_event_routine(nextEvent.J, nextEvent.P, nextEvent.S, CLK);break;}
                    case "Depart": {
                        Execute_Depart_event_routine(nextEvent.J, nextEvent.P, nextEvent.S, CLK); break; }
                    case "Exit":{
                        Execute_Exit_event_routine(nextEvent.J, nextEvent.P, nextEvent.S, CLK); break;}
                }
                
                //Print out the event trajectory and the graph of Queues
                MainFrm.App.AddTrajectory(Math.Round(nextEvent.Time, 2), nextEvent.Name, nextEvent.J, nextEvent.P, nextEvent.S, Q, M, FEL.ToString());
                MainFrm.App.AddChart(CLK, Q[0].Count, Q[1].Count, Q[2].Count, Q[3].Count);
            }
            //4. Statistics calculation phase
            Execute_Statistics_routine(CLK); 
        }
        #endregion

        #region Methods for Handling Events
        /// <summary>
        /// Schedule an event into the future event list (FEL)
        /// </summary>
        /// <param name="name">Event Name</param>
        /// <param name="time">Event Time</param>
        private void Schedule_Event(string name, double time)
        {
            FEL.AddEvent(name, time);
        }

        /// <summary>
        /// Schedule an event into the future event list (FEL)        
        /// </summary>
        /// <param name="name">the name of event</param>
        /// <param name="j">the job type of event</param>
        /// <param name="p">the processing step of event</param>
        /// <param name="s">the station of event</param>
        /// <param name="time">the time of event</param>
        private void Schedule_Event(string name, int j, int p, int s, double time)
        {
            FEL.AddEvent(name, j, p, s, time);
        }

        /// <summary>
        /// Return an event record that located at the first element in the future event list(FEL).
        /// When there is no more event record in the FEL, this method returns null.
        /// </summary>
        /// <returns>An event record</returns>
        private Event Retrieve_Event()
        {
            Event nextEvent = null;
            nextEvent = FEL.NextEvent();
            return nextEvent;
        }

        /// <summary>
        /// Cancel an event which has the same name of a given name from the FEL.
        /// If there are several events that resides in the FEL,
        /// the one with the lowest time will be removed from the FEL.
        /// </summary>
        /// <param name="eventName">Name of an event to be canceled</param>
        /// <param name="eventJobType">Job type of an event to be canceled</param>
        /// <param name="eventProcessingStep">ProcessingStep of an event to be canceled</param>
        /// <param name="eventStation">Station of an event to be canceled</param>
        private void Cancel_event(string eventName, int eventJobType, int eventProcessingStep, int eventStation)
        {
            FEL.RemoveEvent(eventName, eventJobType, eventProcessingStep, eventStation);
        }
        #endregion

        #region Event Routines
        /// <summary>
        /// Execute initialize routine
        /// </summary>
        /// <param name="Now">Time of an initialization (=start time of simulation)</param>
        private void Execute_Initialize_routine(double Now)
        {
            //Initialize the Queue Length, the # of Machines, and the variables for collecting statistics
            Q = new Queue<int[]>[4]; M = new int[4]; JT = new int[4];
            Before = new double[4]; SumQ = new double[4]; 
            for (int s = 0; s < 4; s++)
            {
                Q[s] = new Queue<int[]>();
                M[s] = 1; 
                JT[s] = 0;
                Before[s] = 0.0; SumQ[s] = 0.0; 
            }

            //Initialize the routes
            route = new int[3, 6];
            route[0, 0] = 0; route[0, 1] = 1; route[0, 2] = 2; route[0, 3] = 3; route[0, 4] = 4;
            route[1, 0] = 0; route[1, 1] = 1; route[1, 2] = 3; route[1, 3] = 1; route[1, 4] = 2; route[1, 5] = 4;
            route[2, 0] = 1; route[2, 1] = 0; route[2, 2] = 2; route[2, 3] = 4;

            //Initialize the processing times
            t = new double[3, 6];
            t[0, 0] = 6; t[0, 1] = 5; t[0, 2] = 15; t[0, 3] = 8;
            t[1, 0] = 11; t[1, 1] = 4; t[1, 2] = 15; t[1, 3] = 6; t[1, 4] = 27;
            t[2, 0] = 7; t[2, 1] = 7; t[2, 2] = 18; 

            //Initialize the transport delay data
            delay = new int[4, 5];
            delay[0, 0] = 0; delay[0, 1] = 2; delay[0, 2] = 4; delay[0, 3] = 6; delay[0, 4] = 2;
            delay[1, 0] = 6; delay[1, 1] = 0; delay[1, 2] = 2; delay[1, 3] = 4; delay[1, 4] = 2;
            delay[2, 0] = 4; delay[2, 1] = 6; delay[2, 2] = 0; delay[2, 3] = 2; delay[2, 4] = 2;
            delay[3, 0] = 2; delay[3, 1] = 4; delay[3, 2] = 6; delay[3, 3] = 0; delay[3, 4] = 2;

            //Schedule Arrive Event
            Schedule_Event("Arrive", Now);
        }

        /// <summary>
        /// Execute Arrive event routine
        /// </summary>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Arrive_event_routine(int j, int p, int s, double Now)
        {
            double U = Uni(0, 1);
            j = ((U > 0.26) ? 1 : 0) + ((U > 0.74) ? 1 : 0);
            s = route[j, 0];

            Schedule_Event("Arrive", 0, 0, 0, Now + 12);
            Schedule_Event("Move", j, 0, s, Now);
        }

        /// <summary>
        /// Execute Move event routine
        /// </summary>
        /// <param name="j">JobType</param>
        /// <param name="p">ProcessingStep</param>
        /// <param name="s">Station</param>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Move_event_routine(int j, int p, int s, double Now)
        {
            //Schedule Event
            Schedule_Event("Enter", j, p, s, Now);
        }

        /// <summary>
        /// Execute Enter event routine
        /// </summary>
        /// <param name="j">JobType</param>
        /// <param name="p">ProcessingStep</param>
        /// <param name="s">Station</param>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Enter_event_routine(int j, int p, int s, double Now)
        {
            //Collect Statistics
            SumQ[s] += Q[s].Count * (Now - Before[s]); Before[s] = Now;
            
            //State Change
            Q[s].Enqueue(new int[] { j, p });

            //Schedule Event
            if (M[s] > 0)
                Schedule_Event("Load", 0, 0, s, Now);
        }

        /// <summary>
        /// Execute Load event routine
        /// </summary>
        /// <param name="s">Station</param>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Load_event_routine(int j, int p, int s, double Now)
        {
            //Collect Statistics
            SumQ[s] += Q[s].Count * (Now - Before[s]); Before[s] = Now;
            
            //State change
            int[] job = Q[s].Dequeue();
            j = job[0];
            p = job[1];
            M[s]--;

            double tp = Exp(t[j, p]);
            if (j != JT[s]) tp += 30;
            //Schedule Event
            Schedule_Event("Unload", j, p, s, Now + tp);
        }

        /// <summary>
        /// Execute Unload event routine
        /// </summary>
        /// <param name="j">JobType</param>
        /// <param name="p">ProcessingStep</param>
        /// <param name="s">Station</param>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Unload_event_routine(int j, int p, int s, double Now)
        {
            //State change
            M[s]++;
            JT[s] = j;
            
            //Schedule Event
            if (true)
                Schedule_Event("Depart", j, p, s, Now);
            if (Q[s].Count > 0)
                Schedule_Event("Load", 0, 0, s, Now);
        }

        /// <summary>
        /// Execute Depart event routine
        /// </summary>
        /// <param name="j">JobType</param>
        /// <param name="p">ProcessingStep</param>
        /// <param name="s">Station</param>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Depart_event_routine(int j, int p, int s, double Now)
        {
            int ns = route[j, p + 1];
            int td = delay[s, ns];

            if (ns != 4)
                Schedule_Event("Move", j, p + 1, ns, Now + td);
            else if (ns == 4)
                Schedule_Event("Exit", j, 0, 0, Now);
        }
        /// <summary>
        /// Execute Exit event routine
        /// </summary>
        /// <param name="j">JobType</param>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Exit_event_routine(int j, int p, int s, double Now)
        {
            //Do Nothing
        }

        /// <summary>
        /// Execute Statistics routine after the simulation clock reaches the end of simulation time
        /// </summary>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Statistics_routine(double Now)
        {
            AQL = new double[4];
            for (int s = 0; s < 4; s++)
            {
                SumQ[s] += Q[s].Count * (Now - Before[s]);
                AQL[s] = SumQ[s] / Now;
            }
        }
        #endregion

        #region Random-variate Generation Methods
        /// <summary>
        /// Generate a random value which follows the exponential distribution with a given mean of a
        /// Returns a double-typed random value which follows the exponential distribution with a given mean of a.
        /// Throws Exception
        /// </summary>
        /// <param name="a">A mean value</param>
        /// <returns>Random value of the exponential distribution</returns>
        private double Exp(double a)
        {
            if (a <= 0) throw new Exception("Negative value is not allowed");
            double u = R.NextDouble();
            return (-a * Math.Log(u));
        }

        /// <summary>
        /// Generate a random value which follows the uniform distribution with a given range of a and b
        /// Returns a double-typed random value which follows the uniform distribution with a given range of a and b
        /// Throws Exception
        /// </summary>
        /// <param name="a">Start range</param>
        /// <param name="b">End range</param>
        /// <returns></returns>
        private double Uni(double a, double b)
        {
            if (a >= b) throw new Exception("The range is not valid.");
            double u = R.NextDouble();
            return (a + (b - a) * u);
        }
        #endregion
    }
}
