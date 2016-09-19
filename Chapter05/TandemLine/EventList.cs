/*
 * Copyright (c) Donghun Kang and Byoung K. Choi.  
 * This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
 */

using System;
using System.Collections.Generic;

namespace MSDES.Chap05.TandemLine
{
    public class EventList     {
        #region Member Variables
        private List<Event> _Events; // future event list
        #endregion

        #region Properties

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
        public void AddEvent(string eventName, int eventParameter, double eventTime) {
            Event nextEvent = new Event(eventName, eventParameter, eventTime);

            if (_Events.Count == 0) {
                _Events.Add(nextEvent);
            } else {
                bool isAdded = false;
                for (int i = 0; i < _Events.Count; i++) {
                    Event e = _Events[i];
                    if (nextEvent.Time <= e.Time) {
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
        /// </summary>
        /// <returns>An event record</returns>
        public Event NextEvent()
        {
            Event temp_event = null;
            if (_Events.Count > 0) {
                temp_event = _Events[0];
                _Events.RemoveAt(0);
            }
            return temp_event;
        }

        /// <summary>
        /// Remove an event record that has the same name of a given name with the same parameter.
        /// </summary>
        /// <param name="eventName">Event Name to be Canceled</param>
        public void RemoveEvent(string eventName, int eventParameter) {
            Event CancelEvent = null;
            for (int i = 0; i < _Events.Count; i++) {
                Event e = _Events[i];
                if (e.Name == eventName && e.K == eventParameter) {
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
            for (int i = 0; i < _Events.Count; i++) {
                if (i != 0)
                    fel += ", ";
                fel += "<" + _Events[i].Name + _Events[i].K.ToString() + ", " + Math.Round(_Events[i].Time, 2) + ">";
            }

            return fel;
        }

        #endregion
    }
}
