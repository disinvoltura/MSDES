/*
 * Copyright (c) Donghun Kang and Byoung K. Choi.  
 * This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
 */

using System;

namespace MSDES.Chap04.SingleServerSystem
{
    public class Simulator
    {
        #region Member variables for state variables
        /// <summary>
        /// Number of available machines
        /// </summary>
        private int M;    
        /// <summary>
        /// Number of jobs awaiting at the buffer
        /// </summary>
        private double Q;
        #endregion

        #region Member Variables for Simulator objects
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
        private double Before; // lastly collected time
        private double SumQ; // accumulated values of queue length * time
        private double AQL;  // average queue length
        #endregion

        /// <summary>
        /// Pseudo Random Value Generator 
        /// </summary>
        private Random R;

        #region Properties
        /// <summary>
        /// Average Queue Length at the Buffer
        /// </summary>
        public double AverageQueueLength
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
            CLK = 0.0;
            //Initialize the FEL
            FEL = new EventList();
            //Initialize Random variate R
            R = new Random();

            Execute_Initialize_routine(CLK);
        
            Event nextEvent = new Event();
            while (CLK < eosTime) 
            {                
                //2. Time-flow mechanism phase
                nextEvent = Retrieve_Event();
                CLK = nextEvent.Time;

                //3. Event-routine execution phase
                switch(nextEvent.Name) { 
                    case "Arrive": { Execute_Arrive_event_routine(CLK);break; }
                    case "Load":   { Execute_Load_event_routine(CLK);break; }
                    case "Unload": { Execute_Unload_event_routine(CLK);break; }
                }
            
                //Print out the event trajectory "Time, Name, Q, M, FEL"
                Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4}", Math.Round(CLK, 2), nextEvent.Name, Q, M, FEL.ToString());
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
        /// <param name="timeime">Event Time</param>
        private void Schedule_Event(string name, double time)
        {
            FEL.AddEvent(name, time);
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
        /// <param name="eventName">Event Name</param>
        private void Cancel_Event(string eventName)
        {
            FEL.RemoveEvent(eventName);
        }

        #endregion

        #region Event Routines
        /// <summary>
        /// Execute initialize routine
        /// </summary>
        /// <param name="Now"> Time </param>
        private void Execute_Initialize_routine(double Now) 
        {
            //Initialize Q, and M (state variables)
            Q = 0; 
            M = 1;

            //Initialize the state variables for collecting statistics
            Before = 0; SumQ = 0;
                        
            //Schedule Arrive event 
            Schedule_Event("Arrive", Now);
        }

        /// <summary>
        /// Execute Arrive event routine
        /// </summary>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Arrive_event_routine(double Now) 
        {        
            SumQ += Q * (Now - Before); Before = Now;        
            Q++;

            double ta = Exp(5);
            Schedule_Event("Arrive", Now + ta);
            
            if (M > 0)
                Schedule_Event("Load", Now);
        }
        
        /// <summary>
        /// Execute Load event routine
        /// </summary>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Load_event_routine(double Now) 
        {
            SumQ += Q * (Now - Before); Before = Now;
            M--;
            Q--;

            double ts = Uni(4, 6);
            Schedule_Event("Unload", Now + ts);        
        }
        
        /// <summary>
        /// Execute Unload event routine
        /// </summary>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Unload_event_routine(double Now) 
        {
            M++;

            if (Q >0) 
                Schedule_Event("Load", Now);
        }
       
        /// <summary>
        /// Execute Statistics routine after the simulation clock reaches the end of simulation time
        /// </summary>
        /// <param name="Now">Current Simulation Clock</param>
        private void Execute_Statistics_routine(double Now) 
        {
            SumQ += Q * (Now - Before); 
            AQL = SumQ / Now;        
        }

        #endregion

        #region Methods for Generating Random Variates
        /// <summary>
        /// Generate a random value which follows the exponential distribution with a given mean of a
        /// </summary>
        /// <param name="a">A mean value</param>
        /// <returns>Exponential Random value </returns>
        private double Exp(double a) 
        {
            if (a<=0) throw 
                new ArgumentException("Negative value is not allowed");
            double u = R.NextDouble(); 
            return (-a * Math.Log(u));
        }
        
        /// <summary>
        /// Generate a random value which follows the uniform distribution with a given range of a and b
        /// </summary>
        /// <param name="a">Start range</param>
        /// <param name="b">End range</param>
        /// <returns></returns>
        private double Uni(double a, double b) 
        {
            if (a>=b) throw new Exception("The range is not valid.");
            double u = R.NextDouble();
            return (a + (b - a) * u);
        }        
        #endregion
    }
}
