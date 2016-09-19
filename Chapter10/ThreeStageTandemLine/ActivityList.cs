/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

using System;
using System.Collections.Generic;

namespace MSDES.Chap10.ThreeStageTandemLine
{
    /// <summary>
    /// Container for Candidate Activity List
    /// </summary>
    public class ActivityList
    {
        #region Member Variables
        private List<Activity> _Activities;
        #endregion

        #region Properties
        public int Count
        {
            get { return _Activities.Count; }
        }
        #endregion

        #region Constructors
        public ActivityList()
        {
            _Activities = new List<Activity>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a candidate activity to the end of the list
        /// </summary>
        /// <param name="name">Activity Name</param>
        public void AddActivity(string name)
        {
            Activity act = new Activity(name);
            _Activities.Add(act);
        }

        /// <summary>
        /// Add a candidate activity to the end of the list
        /// </summary>
        /// <param name="name">Activity Name</param>
        /// <param name="k">Activity Parameter</param>
        public void AddActivity(string name, int k)
        {
            Activity act = new Activity(name, k);
            _Activities.Add(act);
        }
        
        /// <summary>
        /// Check that the list is empty or not.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (_Activities.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Retrieve next activity at the first of the list.
        /// </summary>
        /// <returns></returns>
        public Activity NextActivity()
        {
            if (_Activities.Count == 0)
                throw new Exception("The list is empty. No available activities...");

            Activity act = (Activity)_Activities[0];
            _Activities.RemoveAt(0);
            return act;
        }

        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < _Activities.Count; i++)
            {
                Activity activity = (Activity)_Activities[i];
                if (activity.K == int.MinValue)
                    str += activity.Name.ToString(); 
                else
                    str += activity.Name.ToString() + "(" + activity.K + ")";

                if (i < _Activities.Count - 1)
                    str += ",";
            }

            return str;
        }
        #endregion
    }
}
