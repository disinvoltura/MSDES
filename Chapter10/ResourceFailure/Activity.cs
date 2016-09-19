/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

namespace MSDES.Chap10.ResourceFailure
{
    /// <summary>
    /// Class for an Activity
    /// </summary>
    public class Activity
    {
        #region Member Variables
        private string _Name;
        #endregion

        #region Properties
        public string Name
        {
            get { return _Name; }
        }
        #endregion

        #region Constructors
        public Activity(string name)
        {
            _Name = name;
        }        
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            bool rslt = false;
            if (obj != null && obj is Activity)
            {
                Activity target = (Activity)obj;
                if (target != null && target.Name == _Name)
                    rslt = true;
            }
            return rslt;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        #endregion
    }
}
