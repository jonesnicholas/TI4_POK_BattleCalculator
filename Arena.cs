using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TI4BattleSim
{
    public class Arena
    {
        public static List<double> runCrucible(int NumSimulations, Player AttackerModel, Player DefenderModel, List<Player> OthersModel = null, Theater theater = Theater.Hybrid, Random random = null)
        {
            int awins = 0;
            int dwins = 0;
            int draws = 0;
            random = random ?? new Random();
            for (int i = 0; i < NumSimulations; i++)
            {
                Player Attacker = Player.CopyPlayer(AttackerModel);
                Player Defender = Player.CopyPlayer(DefenderModel);
                OthersModel = OthersModel ?? new List<Player>();
                List<Player> Others = OthersModel.Select(player => Player.CopyPlayer(player)).ToList();

                Battle battle = new Battle(Attacker, Defender, Others, theater, random);
                Winner winner = battle.SimulateBattle();
                switch (winner)
                {
                    case Winner.Attacker:
                        awins++;
                        break;
                    case Winner.Defender:
                        dwins++;
                        break;
                    case Winner.Draw:
                        draws++;
                        break;
                }
            }
            //todo: return/explore results data in a useful manner.
            double awinrate = (double)awins / NumSimulations;
            double dwinrate = (double)dwins / NumSimulations;
            double drawrate = (double)draws / NumSimulations;
            Console.WriteLine($"A:{Math.Round(awinrate, 4)} D:{Math.Round(dwinrate, 4)} N:{Math.Round(drawrate, 4)}");
            return new List<double>() { awinrate, dwinrate, drawrate };
        }
    }
}
