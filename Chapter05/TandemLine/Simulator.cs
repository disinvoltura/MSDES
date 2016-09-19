/*
 * Copyright (c) Donghun Kang and Byoung K. Choi.  
 * This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
 */

using System;

namespace MSDES.Chap05.TandemLine
{
    public class Simulator {
        #region Member Variables for State Variables
        /// <summary>
        /// Machine Status of each stage 
        /// </summary>
        private int[] M; 
        /// <summary>
        /// Number of jobs waiting at each stage
        /// </summary>
        private int[] Q;
        #endregion

        #region Member Variables of Simulation Objects
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
        private double[] Before;
        private double[] SumQ; //accumulated values of queue length * time
        private double[] AQL; //average queue length
        #endregion

        #region Member Variables for Arrival Time and Service Times
        /// <summary>
        /// Arrival Time 
        /// </summary>
        private double ta;
        #endregion

        /// <summary>
        /// Pseudo Random Value Generator
        /// </summary>
        private Random R;

        #region Properties
        /// <summary>
        /// Current Simulation Clock
        /// </summary>
        public double Clock {
            get { return this.CLK; }
        }

        /// <summary>
        /// Average queue length at each stage
        /// </summary>
        /// <returns>the average queue length</returns>
        public double[] AverageQueueLengths {
            get { return AQL; }
        }
        #endregion

        #region Constructors
        public Simulator() { }
        #endregion

        #region Methods for Main Program
        /// <summary>
        /// Run the simulation using next-event scheduling algorithm
        /// </summary>
        public void Run(double eosTime) {
            //1. Initialization phase
            CLK = 0.0;
            //Initialize the FEL 
            FEL = new EventList();
            //Initialize Random variate R
            R = new Random();
            //Initialize next event
            Event nextEvent = new Event();

            Execute_Initialize_routine(CLK);
                        
            while (CLK < eosTime) {
                //2. Time-flow mechanism phase
                nextEvent = Retrieve_Event(); 
                CLK = nextEvent.Time; 

                //3. Event-routine execution phase
                switch (nextEvent.Name) {
                    case "Enter": { Execute_Enter_event_routine(nextEvent.K, CLK); break; }
                    case "Load": { Execute_Load_event_routine(nextEvent.K, CLK); break; }
                    case "Unload": { Execute_Unload_event_routine(nextEvent.K, CLK); break; }
                }
                //Print out the event trajectory "Time, Name, Parmeter, Q, M, FEL" and the graph of Queues
                MainFrm.App.AddTrajectory(Math.Round(nextEvent.Time,2), nextEvent.Name, nextEvent.K, Q, M, FEL.ToString());
                MainFrm.App.AddChart(CLK, Q);
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
        /// <param name="k">Event Parameter</param>
        /// <param name="time">Event Time</param>
        private void Schedule_Event(string name, int k, double time) {
            FEL.AddEvent(name, k, time);
        }

        /// <summary>
        /// Return an event record that located at the first element in the FEL.
        /// </summary>
        /// <returns>An event record</returns>
        private Event Retrieve_Event() {
            Event nextEvent = null;
            nextEvent = FEL.NextEvent();
            return nextEvent;
        }

        /// <summary>
        /// Cancel an event which has the same name and parameter of given name and k from the FEL.
        /// </summary>
        /// <param name="name">Event Name</param>
        /// <param name="k">Event Parameter</param>
        private void Cancel_Event(String name, int k) {
            FEL.RemoveEvent(name, k);
        }
        #endregion

        #region Event Routines
        /// <summary>
        /// Execute initialize routine
        /// </summary>
        /// <param name="Now"> Time </param>
        private void Execute_Initialize_routine(double Now) {
            //Initialize the state variables
            Q = new int[4]; M = new int[4];
            Before = new double[4]; SumQ = new double[4];
            for (int k = 1; k <= 3; k++) {
                Q[k] = 0; M[k] = 1;
                Before[k] = 0; SumQ[k] = 0;
            }           

            //Schedule Enter event
            Schedule_Event("Enter", 1, Now);
        }

        /// <summary>
        /// Execute Enter event routine
        /// </summary>
        /// <param name="j">Parameter of an event</param>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Enter_event_routine(int k, double Now) {
            SumQ[k] += Q[k] * (Now - Before[k]); Before[k] = Now;
            Q[k]++;
            
            if (k == 1) {
                ta = Now + Exp(10);
                Schedule_Event("Enter", k, ta);
            }

            if (M[k] > 0)
                Schedule_Event("Load", k, Now);
        }

        /// <summary>
        /// Execute Load event routine
        /// </summary>
        /// <param name="j">Parameter of an event</param>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Load_event_routine(int k, double Now) {
            SumQ[k] += Q[k] * (Now - Before[k]); Before[k] = Now;
            Q[k]--; M[k]--;
            
            double ts = (k == 1 ? 1 : 0) * Uni(10, 15) + (k == 2 ? 1 : 0) * Uni(13, 18) + (k == 3 ? 1 : 0) * Uni(8, 13);
            Schedule_Event("Unload", k, Now + ts);
        }

        /// <summary>
        /// Execute Unload event routine
        /// </summary>
        /// <param name="k">Parameter of an event</param>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Unload_event_routine(int k, double Now) {
            M[k]++;
            if (Q[k] > 0) 
                Schedule_Event("Load", k, Now);
            if (k < 3) 
                Schedule_Event("Enter", k + 1, Now);
        }

        /// <summary>
        /// Calculate the average queue length 
        /// </summary>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Statistics_routine(double Now) {
            AQL = new double[4];
            for (int k = 1; k <= 3; k++)
            {
                SumQ[k] += Q[k] * (Now - Before[k]);
                AQL[k] = SumQ[k] / Now;
            }
        }
        #endregion

        #region Random-variate Generation Methods
        /// <summary>
        /// Returns a random value that follows the exponential distribution with a given mean of a
        /// </summary>
        /// <param name="a">A mean value</param>
        /// <returns>Exponential random value </returns>
        private double Exp(double a) {
            if (a <= 0) throw new Exception("Negative value is not allowed");
            double u = R.NextDouble();
            return (-a * Math.Log(u));
        }

        /// <summary>
        /// Returns a random value that follows the uniform distribution with a given range of a and b
        /// </summary>
        /// <param name="a">Start range</param>
        /// <param name="b">End range</param>
        /// <returns>Uniform random value</returns>
        private double Uni(double a, double b) {
            if (a >= b) throw new Exception("The range is not valid.");
            double u = R.NextDouble();
            return (a + (b - a) * u);
        }
        #endregion
    }
}
