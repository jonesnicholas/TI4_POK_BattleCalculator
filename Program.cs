using System;
using System.Collections.Generic;

namespace TI4BattleSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<double> test;
            //Scenarios.VeldyrDreadSim(100000, veldyr: false, tech: false, upgrade: false);
            //Scenarios.VeldyrDreadSim(100000, veldyr: false, tech: false, upgrade: true);
            //Scenarios.VeldyrDreadSim(100000, veldyr: true, tech: false, upgrade: false);
            //Scenarios.VeldyrDreadSim(100000, veldyr: true, tech: true, upgrade: false);
            //Scenarios.VeldyrDreadSim(100000, veldyr: true, tech: false, upgrade: true);
            Scenarios.VeldyrDreadSim(100000, veldyr: true, tech: true, upgrade: true);

            //Scenarios.ArgentFlightDestroyerSim(1000000);

            Console.WriteLine("Done");
        }
    }
}
