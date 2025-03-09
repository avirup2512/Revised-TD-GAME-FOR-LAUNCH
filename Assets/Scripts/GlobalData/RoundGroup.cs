using System.Collections;
using System.Collections.Generic;
namespace Assets.Script.globalVar
{
    [System.Serializable]
    public class RoundGroup
    {
        public int roundGroupNumber;

        public List<Enemy> unlockedEnemies;

        public List<Round> totalRounds;

        public List<Weapon> unlockedWeapon;

        public int howManyTimesEnemyComes;
        // Start is called before the first frame update
        public RoundGroup(int roundGroupNumber, List<Round> totalRounds, List<Enemy> unlockedEnemies, List<Weapon> unlockedWeapon, int howManyTimesEnemyComes)
        {
            this.roundGroupNumber = roundGroupNumber;
            this.totalRounds = totalRounds;
            this.unlockedEnemies = unlockedEnemies;
            this.unlockedWeapon = unlockedWeapon;
            this.howManyTimesEnemyComes = howManyTimesEnemyComes;
        }
    }

    public class RoundGroupClass
    {
        public List<RoundGroup> roundGroup;
        public int TotalCoins;
        public int TotalDiamond;

        public List<Guard> Guards;
        public List<Weapon> AllWeapon;
        public bool firstTimeGamePlaying;
        public RoundGroupClass(List<RoundGroup> roundGroup, int TotalCoins, int TotalDiamond, List<Guard> Guards, List<Weapon> AllWeapon, bool firstTimeGamePlaying)
        {
            this.roundGroup = roundGroup;
            this.TotalCoins = TotalCoins;
            this.TotalDiamond = TotalDiamond;
            this.Guards = Guards;
            this.AllWeapon = AllWeapon;
            this.firstTimeGamePlaying = firstTimeGamePlaying;
        }
    }

}
