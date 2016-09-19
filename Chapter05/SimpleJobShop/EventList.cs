/*
 * Copyright (c) Donghun Kang and Byoung K. Choi.  
 * This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
 */

using System;
using System.Collections.Generic;

namespace MSDES.Chap05.SimpleJobShop
{
    public class EventList
    {
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
        /// <param name="eventName">Name of an event to be scheduled</param>
        /// <param name="eventTime">Time of an event to be scheduled</param>
        public void AddEvent(string eventName, double eventTime)
        {
            Event nextEvent = new Event();
            nextEvent.Name = eventName;
            nextEvent.Time = eventTime;

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
        /// Schedule an event into the future event list (FEL)        
        /// </summary>
        /// <param name="name">the name of event</param>
        /// <param name="j">the job type of event</param>
        /// <param name="p">the processing step of event</param>
        /// <param name="s">the station of event</param>
        /// <param name="time">the time of event</param>
        public void AddEvent(string name, int j, int p, int s, double time)
        {
            Event nextEvent = new Event();
            nextEvent.Name = name;
            nextEvent.J = j;
            nextEvent.P = p;
            nextEvent.S = s;
            nextEvent.Time = time;
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
        /// </summary>
        public void RemoveEvent(string name, int j, int p, int s)
        {
            Event CancelEvent = null;
            for (int i = 0; i < _Events.Count; i++)
            {
                Event e = _Events[i];
                if (e.Name == name && e.J == j && e.P == p && e.S == s)
                {
                    CancelEvent = e; break;
                }
            }
            if (CancelEvent != null)
                _Events.Remove(CancelEvent);
        }

        /// <summary>
        /// Convert the information(name and time) of 1st and 2nd FEL to string type
        /// </summary>
        /// <returns>the name and time of 1st and 2nd FEL in string</returns>
        public override string ToString()
        {
            string fel = "";

            //If FEL has one member, then we have to convert the info of only 1st FEL to string type.
            int num;
            if (_Events.Count < 2)
                num = _Events.Count;
            else
                num = 2;

            for (int i = 0; i < num; i++)
            {
                double fel_time = Math.Round(_Events[i].Time, 2);
                if (i != 0)
                    fel += ", ";
                if (_Events[i].Name == "Arrive")
                    fel += "<" + _Events[i].Name + ", " + fel_time + ">";
                else if (_Events[i].Name == "Exit")
                    fel += "<" + _Events[i].Name + ", " + _Events[i].J + ", " + fel_time + ">";
                else if (_Events[i].Name == "Load")
                    fel += "<" + _Events[i].Name + ", " + _Events[i].S + " , " + fel_time + ">";
                else
                    fel += "<" + _Events[i].Name + ", " + _Events[i].J.ToString() + ", " + _Events[i].P.ToString() + fel_time + ">";
            }
            return fel;
        }

        #endregion
    }
}
