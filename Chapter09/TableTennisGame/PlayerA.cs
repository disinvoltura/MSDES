/*
* Copyright (c) Donghun Kang and Byoung K. Choi.  
* This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
*/

/* 
 * This code is automatically generated by State Graph Simulator, Copyright (C) Donghun Kang, VMS Laboratory.
 * Date: 2013-09-16
 * Time: 16:41
 * Model Name: PlayerA
 *
 */
using System;
using VMS.StateGraph.Simulation;

namespace MSDES.Chap09.TableTennisGame
{
    /// <summary>
    /// Enumerator for Player A's State
    /// </summary>
    public enum PlayerAState { Attack, Defense, Gameover, OVER, Quit, STOP, Wait };

    /// <summary>
    /// Atomic Simulator for Player A
    /// </summary>
    public class PlayerA : AtomicSimulator
    {
        #region Variables 
        /// <summary>
        /// Current State of Player A
        /// </summary>
        public PlayerAState STATE;
        /// <summary>
        /// Attack Time Delay
        /// </summary>
        public double aa;
        /// <summary>
        /// Probability of an Attack Success
        /// </summary>
        public double PA;
        /// <summary>
        /// Waiting Time Delay
        /// </summary>
        public double wa;
        /// <summary>
        /// Player A's Score
        /// </summary>
        public int MyScr;
        /// /// <summary>
        /// Player B's Score
        /// </summary>
        public int UrScr;
        /// <summary>
        /// Number of Rallies
        /// </summary>
        public int Rally;
        /// <summary>
        /// Receive Count
        /// </summary>
        public int Rcv;
        /// <summary>
        /// Serve Count
        /// </summary>
        public int Srv;
        /// <summary>
        /// Random Value
        /// </summary>
        public double U;
        /// <summary>
        /// Uniform Random Variate Geneartor
        /// </summary>
        private Random r;
        #endregion 

        #region Properties
        public override string CurrentState
        {
            get { return this.STATE.ToString(); }
        }
        #endregion

        #region Constructors
        public PlayerA(string name)
            : base(name)
        {
            aa = 0.5;
            PA = 0.4;
            wa = 4;
            MyScr = 0;
            Rally = 0;
            Rcv = 2;
            Srv = 0;
            U = 0;
            UrScr = 0;

            r = new Random();
        }
        #endregion       

        #region Methods
        public override void Run()
        {
            State_Wait(0.0);
        }

        public override void ExternalInput(INPUT input)
        {
            switch (STATE)
            {
                case PlayerAState.Attack:
                    EXT_State_Attack(input);
                    break;
                case PlayerAState.Defense:
                    EXT_State_Defense(input);
                    break;
                case PlayerAState.Gameover:
                    EXT_State_Gameover(input);
                    break;
                case PlayerAState.Quit:
                    EXT_State_Quit(input);
                    break;
                case PlayerAState.Wait:
                    EXT_State_Wait(input);
                    break;
                default:
                    WriteError("No matching state.", "ExternalInput()");
                    break;
            }
        }

        public override object getStateVariable(string name)
        {
            if (name.Equals("MyScr"))
                return this.MyScr;
            if (name.Equals("Rally"))
                return this.Rally;
            if (name.Equals("Rcv"))
                return this.Rcv;
            if (name.Equals("Srv"))
                return this.Srv;
            if (name.Equals("U"))
                return this.U;
            if (name.Equals("UrScr"))
                return this.UrScr;
            return null;
        }
        #endregion

        #region StateX Methods
        private void State_Attack(double now)
        {
            STATE = PlayerAState.Attack;

            //Entry Action
            Rally++;
            U = r.NextDouble();
            _Clock = now;
            Send_TAR(_Clock + aa, "Quit", "OVER");
        }

        private void EXT_State_Attack(INPUT input)
        {
            if (input.Type == InputType.TAG)
            {
                TAG tag = input.Record as TAG;
                if (U <= PA) //Condition
                {
                    //Transition Action
                    Send_MSR("BallA");
                    //Next State
                    State_Defense(tag.Now); //Next State
                }
                else if (U > PA) //Condition
                {
                    //Transition Action
                    UrScr++;
                    Send_MSR("OutA");
                    //Next State
                    State_Wait(tag.Now); //Next State
                }
                else
                    WriteError("No matching condition.", "EXT_State_Attack()");
            }
            else if (input.Type == InputType.MDP)
            {
                MDP mdp = input.Record as MDP;
                if (mdp.Msg.Equals("Quit"))
                {
                     //Next State
                     State_Quit(mdp.Now); 
                }
                else if (mdp.Msg.Equals("OVER"))
                {
                    //Next State
                    State_OVER(mdp.Now);
                }
                else
                    WriteError("No matching message.", "EXT_State_Attack()");
            }
        }

        private void State_Defense(double now)
        {
            STATE = PlayerAState.Defense;

            //Entry Action
            _Clock = now;
            Send_TAR(double.MaxValue, "BallB", "OutB", "Quit", "OVER");
        }

        private void EXT_State_Defense(INPUT input)
        {
            if (input.Type == InputType.MDP)
            {
                MDP mdp = input.Record as MDP;
                if (mdp.Msg.Equals("BallB"))
                {
                    //Next State
                    State_Attack(mdp.Now);
                }
                else if (mdp.Msg.Equals("OutB"))
                {
                    //Transition Action
                    MyScr++;
                    //Next State
                    State_Wait(mdp.Now); 
                }
                else if (mdp.Msg.Equals("Quit"))
                {
                    //Next State
                    State_Quit(mdp.Now); 
                }
                else if (mdp.Msg.Equals("OVER"))
                {
                    //Next State
                    State_OVER(mdp.Now); 
                }
                else
                    WriteError("No matching message.", "EXT_State_Defense()");
            }
            else
                WriteError("No matching input.", "EXT_State_Defense()");
        }

        private void State_Gameover(double now)
        {
            STATE = PlayerAState.Gameover;

            //Entry Action
            _Clock = now;
            Send_TAR(double.MaxValue, "OVER");
        }

        private void EXT_State_Gameover(INPUT input)
        {
            if (input.Type == InputType.MDP)
            {
                MDP mdp = input.Record as MDP;
                if (mdp.Msg.Equals("OVER"))
                {
                    //Next State
                    State_OVER(mdp.Now); 
                }
                else if (mdp.Msg.Equals("STOP"))
                {
                    //Next State
                    State_STOP(mdp.Now); 
                }
                else
                    WriteError("No matching message.", "EXT_State_Gameover()");
            }
            else
                WriteError("No matching input.", "EXT_State_Gameover()");
        }

        private void State_OVER(double now)
        {
            STATE = PlayerAState.OVER;
        }

        private void State_Quit(double now)
        {
            STATE = PlayerAState.Quit;

            //Entry Action
            _Clock = now;
            Send_TAR(double.MaxValue, "OVER");
        }

        private void EXT_State_Quit(INPUT input)
        {
            if (input.Type == InputType.MDP)
            {
                MDP mdp = input.Record as MDP;
                if (mdp.Msg.Equals("OVER"))
                {
                    //Next State
                    State_OVER(mdp.Now); 
                }
                else if (mdp.Msg.Equals("STOP"))
                {
                    //Next State
                    State_STOP(mdp.Now); 
                }
                else
                    WriteError("No matching message.", "EXT_State_Quit()");
            }
            else
                WriteError("No matching input.", "EXT_State_Quit()");
        }

        private void State_Wait(double now)
        {
            STATE = PlayerAState.Wait;

            //Entry Action
            _Clock = now;
            Send_TAR(_Clock + wa, "Quit", "OVER");
        }

        private void EXT_State_Wait(INPUT input)
        {
            if (input.Type == InputType.TAG)
            {
                TAG tag = input.Record as TAG;
                if ((MyScr < 11) && (UrScr < 11) && (Rcv == 2)) //Condition
                {
                    //Transition Action
                    Srv += 1;
                    if (Srv == 2) Rcv = 0;
                    //Next State
                    State_Attack(tag.Now); 
                }
                else if ((MyScr < 11) && (UrScr < 11) && (Srv == 2)) //Condition
                {
                    //Transition Action
                    Rcv += 1;
                    if (Rcv == 2) Srv = 0;
                    //Next State
                    State_Defense(tag.Now);
                }
                else if ((MyScr == 11) || (UrScr == 11)) //Condition
                {
                    //Transition Action
                    Send_MSR("Over");
                    //Next State
                    State_Gameover(tag.Now); 
                }
                else
                    WriteError("No matching condition.", "EXT_State_Wait()");
            }
            else if (input.Type == InputType.MDP)
            {
                MDP mdp = input.Record as MDP;
                if (mdp.Msg.Equals("Quit"))
                {
                    //Next State
                    State_Quit(mdp.Now); 
                }
                else if (mdp.Msg.Equals("OVER"))
                {
                    //Next State
                    State_OVER(mdp.Now); 
                }
                else
                    WriteError("No matching message.", "EXT_State_Wait()");
            }
        }
        #endregion
    }
}