/*
 * Copyright (c) Donghun Kang and Byoung K. Choi.  
 * This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
 */

using System;
using System.Collections.Generic;

namespace MSDES.Chap04.SingleServerSystem
{
    public class EventList
    {
        #region Member Variables
        private List<Event> _Events; // future event list
        #endregion

        #region Constructors
        public EventList()
        {
            _Events = new List<Event>();
        }
        #endregion

        #region Methods
        public void Initialize()
        {
            _Events.Clear();
        }

        /// <summary>
        /// Schedule an event into the future event list (FEL)
        /// </summary>
        /// <param name="eventName">Event Name</param>
        /// <param name="eventTime">Event Time</param>
        public void AddEvent(String eventName, double eventTime)
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

        /// Return an event record that located at the first element in the future event list(FEL).
        /// When there is no more event record in the FEL, this method returns null.
        /// </summary>
        /// <returns>An event record</returns>
        public Event NextEvent()
        {
            Event temp_event = null;
            if (_Events.Count > 0)
            {
                temp_event = _Events[0];
                _Events.RemoveAt(0);
            }
            return temp_event;
        }

        /// <summary>
        /// Cancel an event which has the same name of a given name from the FEL.
        /// If there are several events that resides in the FEL,
        /// the one with the lowest time will be removed from the FEL.
        /// </summary>
        /// <param name="eventName">Name of an event to be canceled</param>
        public void RemoveEvent(String eventName)
        {
            Event CancelEvent = null;
            for (int i = 0; i < _Events.Count; i++)
            {
                Event e = _Events[i];
                if (e.Name == eventName)
                {
                    CancelEvent = e; break;
                }
            }
            if (CancelEvent != null)
                _Events.Remove(CancelEvent);
        }

        /// <summary>
        /// Make a string that contains the information of all event records of the FEL
        /// </summary>
        public override string ToString()
        {
            string fel = "";
            for (int i = 0; i < _Events.Count; i++)
            {
                if (i != 0)
                    fel += ", ";
                fel += "<" + _Events[i].Name + ", " + Math.Round(_Events[i].Time, 2) + ">";
            }

            return fel;
        }
        #endregion
    }
}
