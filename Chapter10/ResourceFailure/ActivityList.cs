/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

using System;
using System.Collections.Generic;

namespace MSDES.Chap10.ResourceFailure
{
    /// <summary>
    /// Container for Candidate Activity List
    /// </summary>
    public class ActivityList
    {
        #region Member Variables
        private List<Activity> mList;
        #endregion

        #region Properties
        public int Count
        {
            get { return mList.Count; }
        }
        #endregion

        #region Constructors
        public ActivityList()
        {
            mList = new List<Activity>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a candidate activity to the end of the list
        /// </summary>
        /// <param name="act"></param>
        public void AddActivity(string name)
        {
            Activity act = new Activity(name);
            mList.Add(act);
        }
        
        /// <summary>
        /// Check that the list is empty or not.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (mList.Count == 0)
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
            if (mList.Count == 0)
                throw new Exception("The list is empty. No available activities...");

            Activity act = (Activity)mList[0];
            mList.RemoveAt(0);
            return act;
        }

        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < mList.Count; i++)
            {
                Activity activity = (Activity)mList[i];
                str += activity.Name.ToString();

                if (i < mList.Count - 1)
                    str += ",";
            }

            return str;
        }
        #endregion
    }
}
