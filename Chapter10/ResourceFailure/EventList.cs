/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

using System;
using System.Collections.Generic;

namespace MSDES.Chap10.ResourceFailure
{
    /// <summary>
    /// Container for managing events in the time-order
    /// </summary>
    public class EventList
    {
        #region Member Variables
        private List<Event> _Events;
        #endregion

        #region Properties
        public int Count
        {
            get { return _Events.Count; }
        }
        #endregion

        #region Constructors
        public EventList()
        {
            _Events = new List<Event>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get the next event (remove the first event in the list)
        /// </summary>
        public Event NextEvent()
        {
            Event next_event = null;
            if (_Events.Count > 0)
            {
                next_event = _Events[0];
                _Events.RemoveAt(0);
            }
            return next_event;
        }

        /// <summary>
        /// Schedule an event into the future event list (FEL)
        /// </summary>
        /// <param name="eventName">Event Name</param>
        /// <param name="eventTime">Event Time</param>
        public void AddEvent(string eventName, double eventTime)
        {
            Event nextEvent = new Event(eventName, eventTime);

            if (_Events.Count == 0)
            {
                _Events.Add(nextEvent);
            }
            else
            {
                bool isAdded = false;
                for (int i = 0; i < _Events.Count; i++)
                {
                    Event e = _Events[i];
                    if (nextEvent.Time <= e.Time)
                    {
                        _Events.Insert(i, nextEvent);
                        isAdded = true;
                        break;
                    }
                }
                if (!isAdded)
                    _Events.Add(nextEvent);
            }            
        }

        /// <summary>
        /// Delete an event from the list whose name is same as the specified name <paramref name="eventName"/> with the least time value.
        /// </summary>
        /// <param name="eventName">Event Name</param>
        public void DeleteEvent(string eventName)
        {
            for (int i = 0; i < _Events.Count; i++)
            {
                if (_Events[i].Name == eventName)
                {
                    _Events.RemoveAt(i);
                    break;
                }
            }
        }

        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < _Events.Count; i++)
            {
                Event evt = (Event)_Events[i];

                str += evt.Name.ToString() +
                           "(" + Math.Round(evt.Time, 2).ToString() + ")";

                if (i < _Events.Count - 1)
                    str += ",";
            }

            return str;
        }
        #endregion
    }

}
