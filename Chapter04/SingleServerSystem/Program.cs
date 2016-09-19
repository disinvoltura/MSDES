/*
 * Copyright (c) Donghun Kang and Byoung K. Choi.  
 * This file is part of the book, "Modeling and Simulation of Discrete-Event Systems". 
 */

using System;

namespace MSDES.Chap04.SingleServerSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //Start a new simulation
            Simulator sim = new Simulator();
            
            //Catch Exception
            try
            {
                sim.Run(500);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //AQL : Average queue length from the simulation
            double AQL = (Math.Round(sim.AverageQueueLength * 100)) / 100.0;

            //Print out the statistics
            Console.WriteLine("[Statistics] ===========================================================");
            Console.WriteLine("Average Queue Length: " + AQL);
        }
    }
}
