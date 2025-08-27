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

        public static List<double> GetCombatHitMatrix(string state, Player AttackerModel, Player DefenderModel)
        {
            string aUnits = state.Split(":")[0];
            string dUnits = state.Split(":")[1];

            List<double> aHits = new List<double>() { 0 };
            List<double> dHits = new List<double>() { 0 };

            foreach(char c in aUnits)
            {

            }

            throw new NotImplementedException();
        }

        public static List<double> Convolute(List<double> initial, List<double> convolution)
        {
            Dictionary<int, double> resultRaw = new Dictionary<int, double>();

            for (int i = 0; i < initial.Count; i ++)
            {
                for (int j = 0; j < convolution.Count; j ++)
                {
                    int count = i + j;
                    double prob = initial[i] * convolution[j];
                    if (!resultRaw.ContainsKey(count))
                        resultRaw[count] = 0.0;

                    resultRaw[count] += prob;
                }
            }

            int max = resultRaw.Keys.Max();
            List<double> output = new List<double>();
            for (int i = 0; i <= max; i ++)
            {
                if (resultRaw.ContainsKey(i))
                    output.Add(resultRaw[i]);
                else
                    output.Add(0.0);
            }
            return output;
        }
    }
}
