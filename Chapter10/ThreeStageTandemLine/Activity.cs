/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

namespace MSDES.Chap10.ThreeStageTandemLine
{
    /// <summary>
    /// Class for an Activity
    /// </summary>
    public class Activity
    {
        #region Member Variables
        private string _Name;
        private int _K = 0;
        #endregion

        #region Properties
        /// <summary>
        /// Activity Name
        /// </summary>
        public string Name
        {
            get { return _Name; }
        }

        /// <summary>
        /// Parameter (K)
        /// </summary>
        public int K
        {
            get { return _K; }
        }
        #endregion

        #region Constructors
        public Activity(string name)
        {
            _Name = name;
            _K = int.MinValue;
        }

        public Activity(string name, int workstationid)
        {
            _Name = name;
            _K = workstationid;
        }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            bool rslt = false;
            if (obj != null && obj is Activity)
            {
                Activity target = (Activity)obj;
                if (target != null && target.Name == _Name && target.K == _K)
                    rslt = true;
            }
            return rslt;
        }

        public override string ToString()
        {
            if (_K == int.MinValue)
                return _Name;
            else
                return _Name + "(" + _K + ")";                
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        #endregion
    }
}
