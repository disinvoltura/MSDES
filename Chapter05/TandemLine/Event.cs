/*
 * Copyright (c) Donghun Kang and Byoung K. Choi.  
 * This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
 */

namespace MSDES.Chap05.TandemLine
{
    /// <summary>
    /// Class for an Event Record
    /// </summary>
    public class Event
    {
        #region Member Variables
        private string _Name;
        private int _K;
        private double _Time;
        #endregion

        #region Properties
        public string Name { get { return _Name; } }
        public int K { get { return _K; } }
        public double Time { get { return _Time; } }
        #endregion

        /// <summary>
        /// Parameterless default constructor
        /// </summary>
        public Event() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">the name of event</param>
        /// <param name="parameter">the parameter of event</param>
        /// <param name="time">the time of event</param>
        public Event(string name, int parameter, double time) {
            _Name = name;
            _K = parameter;
            _Time = time;
        }

        /// <summary>
        /// Check if the parameter is equal to the event
        /// </summary>
        /// <param name="obj">Object to compare with this event</param>
        public override bool Equals(object obj) {
            if (obj is Event) {
                Event target = obj as Event;
                if (target.Name == this.Name && target.K == this.K && target.Time == this.Time)
                    return true;
                else
                    return false;
            } else
                return false;
        }
    }
}
