/*
 * Copyright (c) Donghun Kang and Byoung K. Choi.  
 * This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
 */
 
namespace MSDES.Chap05.SimpleJobShop
{
    /// <summary>
    /// Class for an Event Record
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Name of Event
        /// </summary>
        public string Name;
        /// <summary>
        /// Scheduled Time of Event
        /// </summary>
        public double Time; 
        /// <summary>
        /// Job Type (Event Parameter)
        /// </summary>
        public int J; 
        /// <summary>
        /// Processing Step (Event Parameter)
        /// </summary>
        public int P; 
        /// <summary>
        /// Station Number (Event Parameter)
        /// </summary>
        public int S; 
        
        /// <summary>
        /// Parameterless default constructor
        /// </summary>
        public Event()
        {
            this.J = 0;
            this.P = 0;
            this.S = 0;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="time"></param>
        public Event(string name, double time) : this()
        {
            this.Name = name;
            this.Time = time;
        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="name"></param>
        /// <param name="jobtype"></param>
        /// <param name="processingstep"></param>
        /// <param name="station"></param>
        /// <param name="time"></param>
        public Event(string name, int j, int p, int s, double time)
        {
            this.Name = name;
            this.P = p;
            this.J = j;
            this.S = s;
            this.Time = time;

        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// Check if this event is equal to the parameter
        /// </summary>
        /// <param name="obj">Object to compare with this event</param>
        /// <returns>true, if this event is equal to the parameter,
        ///          false, otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Event)
            {
                Event target = obj as Event;
                if (target.Name == this.Name && target.J == this.J && target.P == this.P && target.S == this.S && target.Time == this.Time)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

    }
}
