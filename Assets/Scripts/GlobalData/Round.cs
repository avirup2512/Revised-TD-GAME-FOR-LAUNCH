using System.Collections;
using System.Collections.Generic;
namespace Assets.Script.globalVar
{
    [System.Serializable]
    public class Round
    {
        public string name;

        public int roundNumber;
        public bool mainRound;

        public bool locked;

        public int totalIncomingEnemy;

        public List<Enemy> unLockedEnemy;

        public List<Enemy> comingEnemy;

        public int howManyTimesEnemyComes;

        public int roundGroupNumber;

        public Enemy mainEnemy;
        // Start is called before the first frame update
        public Round(string name, int roundNumber, bool mainRound, bool locked, int totalIncomingEnemy, List<Enemy> unLockedEnemy, List<Enemy> comingEnemy, int howManyTimesEnemyComes, int roundGroupNumber, Enemy mainEnemy)
        {
            this.name = name;
            this.mainRound = mainRound;
            this.roundNumber = roundNumber;
            this.locked = locked;
            this.totalIncomingEnemy = totalIncomingEnemy;
            this.unLockedEnemy = unLockedEnemy;
            this.comingEnemy = comingEnemy;
            this.howManyTimesEnemyComes = howManyTimesEnemyComes;
            this.roundGroupNumber = roundGroupNumber;
            this.mainEnemy = mainEnemy;
        }
    }

}
