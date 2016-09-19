/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

using System;
using System.Collections.Generic;

namespace MSDES.Chap10.ThreeStageTandemLine
{
    /// <summary>
    /// Container for managing events in the time-order
    /// </summary>
    public class EventList
    {
        #region Member Variables
        private List<Event> Events;
        #endregion

        #region Properties
        public int Count
        {
            get { return Events.Count; }
        }
        #endregion

        #region Constructors
        public EventList()
        {
            Events = new List<Event>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get the next event (remove the first event in the list)
        /// </summary>
        public Event NextEvent()
        {
            if (Events.Count == 0)
            {
                throw new Exception("No more event-time pair in this list");
            }

            Event nextEvent = Events[0] as Event;

            if (nextEvent == null)
            {
                throw new Exception("Invalid arguments, Can't find any next event.");
            }

            Events.RemoveAt(0);

            return nextEvent;
        }

        /// <summary>
        /// Schedule an event into the future event list (FEL)
        /// </summary>
        /// <param name="name">Event Name</param>
        /// <param name="time">Event Time</param>
        public void AddEvent(string name, double time)
        {
            Event evt = new Event(name, time);

            if (Events.Count == 0)
            {
                Events.Add(evt);
                return;
            }

            for (int i = 0; i < Events.Count; i++)
            {
                Event item = Events[i] as Event;
                if (item != null)
                {
                    if (evt.Time < item.Time)
                    {
                        Events.Insert(i, evt);
                        return;
                    }
                }
            }

            Events.Add(evt);
        }

        /// <summary>
        /// Schedule an event into the future event list (FEL)
        /// </summary>
        /// <param name="name">Event Name</param>
        /// <param name="k">Event Parameter (k)</param>
        /// <param name="time">Event Time</param>
        public void AddEvent(string name, double time, int k)
        {
            Event evt = new Event(name, time, k);

            if (Events.Count == 0)
            {
                Events.Add(evt);
                return;
            }

            for (int i = 0; i < Events.Count; i++)
            {
                Event item = Events[i] as Event;
                if (item != null)
                {
                    if (evt.Time < item.Time)
                    {
                        Events.Insert(i, evt);
                        return;
                    }
                }
            }

            Events.Add(evt);
        }

        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < Events.Count; i++)
            {
                Event evt = (Event)Events[i];

                if (evt.K == int.MinValue)
                {
                    str += evt.Name.ToString() +
                           "(" + Math.Round(evt.Time, 2).ToString() + ")";
                }
                else
                {
                    str += evt.Name.ToString() +
                           "(" + evt.K + "," + Math.Round(evt.Time, 2).ToString() + ")";
                }
                if (i < Events.Count - 1)
                    str += ",";
            }

            return str;
        }
        #endregion
    }

}
