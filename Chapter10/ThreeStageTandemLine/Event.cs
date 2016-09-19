/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

using System;

namespace MSDES.Chap10.ThreeStageTandemLine
{
    /// <summary>
    /// Class for an Event Record
    /// </summary>
    public class Event
    {
        #region Member Variables
        private string _Name;
        private double _Time; 
        private int _K;
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
        /// Event Parameter (K)
        /// </summary>
        public int K
        {
            get { return _K; }
        }
        /// <summary>
        /// Scheduled Event Time
        /// </summary>
        public double Time
        {
            get { return _Time; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for Event class
        /// </summary>
        /// <param name="name">The Name of an Event</param>
        /// <param name="time">The Time of an Event</param>
        public Event(string name, double time)
        {
            _Name = name;
            _Time = time;
            _K = int.MinValue;
        }

        /// <summary>
        /// Constructor for Event class
        /// </summary>
        /// <param name="name">The Name of an Event</param>
        /// <param name="time">The Time of an Event</param>
        /// <param name="k">Event Parameter (k)</param>
        public Event(string name, double time, int k)
        {
            _Name = name;
            _Time = time;
            _K = k;
        }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            bool rslt = false;
            Event target = (Event)obj;
            if (target != null && target.Name == _Name &&
                target.Time == _Time &&
                target.K == _K)
                rslt = true;

            return rslt;
        }

        public override string ToString()
        {
            if (_K == int.MinValue)
                return _Name + "@" + Math.Round(_Time, 2);
            else
                return _Name + "(" + _K + ")@" + Math.Round(_Time, 2);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion
    }
}
