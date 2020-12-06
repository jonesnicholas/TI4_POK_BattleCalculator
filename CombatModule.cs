using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace TI4BattleSim
{
    public class CombatModule
    {
        public int NumDice;
        public int ToHit;
        public bool canSustain;
        public int effectiveness => NumDice * (11 - ToHit);

        public CombatModule(int num, int value, bool sustain = false)
        {
            NumDice = num;
            ToHit = value;
            canSustain = sustain;
        }

        public int doCombat(Battle battle, Player owner, Player target)
        {
            return doRoll(battle, owner, target, NumDice, ToHit);
        }

        public Func<Battle, Player, Player, int, int, int> doRoll = defaultRoll;

        public static Func<Battle, Player, Player, int, int, int> noRoll = (battle, owner, target, num, toHit) => 0;
        public static Func<Battle, Player, Player, int, int, int> defaultRoll = (battle, owner, target, num, toHit) =>
        {
            int hits = 0;
            for (int i = 0; i < num; i ++)
            {
                if (battle.random.Next(1,11) >= toHit)
                    hits++;
            }
            return hits;
        };

        public static CombatModule NullModule()
        {
            CombatModule module = new CombatModule(0, 0, false);
            module.doRoll = CombatModule.noRoll;
            return module;
        }
    }
}
