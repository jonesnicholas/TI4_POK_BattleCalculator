using System;
using System.Collections.Generic;
using System.Text;
using TI4BattleSim.Units;

namespace TI4BattleSim
{
    public enum UnitType
    {
        Fighter, Destroyer, Cruiser, Carrier, Dreadnought, Flagship, Warsun, Infantry, Mech, PDS, SpaceDock
    };

    public enum Damage
    {
        None, RecentlyDamaged, Damaged
    }

    public class Unit : IComparable
    {
        public UnitType type;
        public Theater theater;
        public int capacity = 0;
        public bool upgraded = false;
        public bool hasPlanetaryShield = false;
        public bool bypassPlanetaryShield = false;
        public Damage damage = Damage.None;
        public CombatModule spaceCombat = CombatModule.NullModule();
        public CombatModule groundCombat = CombatModule.NullModule();
        public CombatModule spaceCannon = CombatModule.NullModule();
        public CombatModule antiFighter = CombatModule.NullModule();
        public CombatModule bombard = CombatModule.NullModule();



        public static Unit CreateUnit(UnitType type, TechModel techs = null, Faction faction = Faction.None)
        {
            techs = techs ?? new TechModel();
            switch (type)
            {
                case UnitType.Destroyer:
                    return new Destroyer(techs, faction);
                case UnitType.Cruiser:
                    return new Cruiser(techs, faction);
                case UnitType.Carrier:
                    return new Carrier(techs, faction);
                case UnitType.Dreadnought:
                    return new Dreadnought(techs, faction);
                case UnitType.Flagship:
                    return new Flagship(techs, faction);
                case UnitType.Warsun:
                    return new Warsun(techs, faction);
                case UnitType.Infantry:
                    return new Infantry(techs, faction);
                case UnitType.Mech:
                    return new Mech(techs, faction);
                case UnitType.Fighter:
                    return new Fighter(techs, faction);
                case UnitType.PDS:
                    return new PDS(techs, faction);
                case UnitType.SpaceDock:
                    return new SpaceDock(techs, faction);
                default:
                    throw new Exception("Attempted to create Unit of invalid type");
            };
        }

        public static List<Unit> CreateGenericUnitList(Dictionary<UnitType, int> counts)
        {
            List<Unit> output = new List<Unit>();
            foreach (UnitType type in counts.Keys)
            {
                for (int i = 0; i < counts[type]; i++)
                {
                    output.Add(CreateUnit(type));
                }
            }
            return output;
        }
        public static List<Unit> CreateUnitList(List<Unit> unitMenu, TechModel techs = null, Faction faction = Faction.None)
        {
            List<Unit> output = new List<Unit>();
            foreach (Unit unit in unitMenu)
            {
                output.Add(Unit.CreateUnit(unit.type, techs, faction));
            }
            return output;
        }

        public void SustainDamage() //todo: make this overrideable function
        {
            //todo: maybe check to make sure ship is one that is allowed to sustain
            if (damage != Damage.None)
                throw new Exception("Can't sustain damage on a ship that's already damaged");

            damage = Damage.RecentlyDamaged;
        }

        public void Repair()
        {
            if (damage == Damage.None)
                throw new Exception("Tried to repair a unit that wasn't damaged");

            damage = Damage.None;
        }

        public virtual void DestroyUnit(Battle battle, Player owner)
        {
            owner.units.Remove(this);
        }

        public bool ParticipatesInCombat(Theater theater)
        {
            return
                theater == Theater.Space && spaceCombat.NumDice > 0 ||
                theater == Theater.Ground && groundCombat.NumDice > 0;
        }

        public bool HasAFB => antiFighter.NumDice > 0;

        public bool CanSustain(Theater theater)
        {
            return
                theater == Theater.Space && spaceCombat.canSustain ||
                theater == Theater.Ground && groundCombat.canSustain;
        }

        public int CompareTo(object obj)
        {
            Unit other = (Unit)obj;
            // using default comparator for comparing capacity
            return capacity - other.capacity;
        }

        public static IComparer<Unit> SortCombat(Theater theater)
        {
            if (theater == Theater.Space)
                return new SortSpaceCombatHelper();
            if (theater == Theater.Ground)
                return new SortGroundCombatHelper();
            throw new Exception("Tried to sort by a combat value other than space or ground");
        }

        private class SortSpaceCombatHelper : IComparer<Unit>
        {
            int IComparer<Unit>.Compare(Unit unit1, Unit unit2)
            {
                int comp = unit1.spaceCombat.effectiveness.CompareTo(unit2.spaceCombat.effectiveness);
                if (comp == 0)
                {
                    // prioritize assigning hits to things that can still sustain over those that can't
                    if (unit1.spaceCombat.canSustain && unit1.damage == Damage.None)
                        comp++;
                    if (unit2.spaceCombat.canSustain && unit2.damage == Damage.None)
                        comp--;
                }
                if (comp == 0)
                {
                    // todo: prioritize losing "cheaper" things
                    comp = unit1.type - unit2.type;
                }
                return comp;
            }
        }

        private class SortGroundCombatHelper : IComparer<Unit>
        {
            int IComparer<Unit>.Compare(Unit unit1, Unit unit2)
            {
                int comp = unit1.groundCombat.effectiveness.CompareTo(unit2.groundCombat.effectiveness);
                if (comp == 0)
                {
                    // prioritize assigning hits to things that can still sustain over those that can't
                    if (unit1.groundCombat.canSustain && unit1.damage == Damage.None)
                        comp++;
                    if (unit2.groundCombat.canSustain && unit2.damage == Damage.None)
                        comp--;
                }
                return comp;
            }
        }




        public static List<UnitType> GetAllTypes()
        {
            List<UnitType> types = new List<UnitType>();
            foreach(UnitType type in Enum.GetValues(typeof(UnitType)))
            {
                types.Add(type);
            }
            return types;
        }
    }
}
