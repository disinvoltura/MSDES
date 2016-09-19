/*
 * Copyright (c) Donghun Kang and Byoung K. Choi.  
 * This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
 */


namespace MSDES.Chap04.SingleServerSystem
{
    /// <summary>
    /// Class for an Event Record
    /// </summary>
    public class Event
    {
        #region Member Variables
        private string _Name;
        private double _Time;
        #endregion

        #region Properties
        /// <summary>
        /// Event Name
        /// </summary>
        public string Name
        {
            get { return _Name; }
        }

        /// <summary>
        /// Event Time
        /// </summary>
        public double Time
        {
            get { return _Time; }
        }
        #endregion

        #region Constructors
        public Event()
        {
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="name">the name of an event</param>
        /// <param name="time">the time of an event</param>
        public Event(string name, double time)
        {
            _Name = name;
            _Time = time;
        }
        #endregion

        #region Methods        
        public override bool Equals(object obj)
        {
            bool rslt = false;
            Event target = (Event)obj;
            if (target != null && target.Name == _Name &&
                target.Time == _Time)
                rslt = true;

            return rslt;
        }

        public override string ToString()
        {
            return _Name + "@" + _Time;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion
    }
}
