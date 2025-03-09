namespace Assets.Script.globalVar
{
    [System.Serializable]
    public class Enemy
    {
        public string name;
        public int number;
        public int power;
        public int unlockedRoundGroup;
        public bool canFly;
        public bool canFire;

        // Start is called before the first frame update
        public Enemy(string name, int number, int power, int unlockedRoundGroup, bool canFly, bool canFire)
        {
            this.name = name;
            this.power = power;
            this.unlockedRoundGroup = unlockedRoundGroup;
            this.canFly = canFly;
            this.canFire = canFire;
            this.number = number;
        }
    }

}
