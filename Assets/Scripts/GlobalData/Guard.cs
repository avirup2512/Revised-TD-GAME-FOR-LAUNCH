namespace Assets.Script.globalVar
{
    [System.Serializable]
    public class Guard
    {
        public GuardType guardType;
        public int purchasedGuard;
        public int price;
        public bool selected;
        public Guard(GuardType guardType, int purchasedGuard, int price, bool selected)
        {
            this.guardType = guardType;
            this.purchasedGuard = purchasedGuard;
            this.price = price;
            this.selected = selected;
        }
    }

    [System.Serializable]
    public class GuardType
    {
        public string name;
        public int power;
        public string sprite;
        public string guardPrefabs;

        public bool hasLife;

        public GuardType(string name, int power, string sprite, string guardPrefabs, bool hasLife)
        {
            this.name = name;
            this.power = power;
            this.sprite = sprite;
            this.hasLife = hasLife;
            this.guardPrefabs = guardPrefabs;
        }
    }

}
