namespace Assets.Script.globalVar
{
    [System.Serializable]
    public class Weapon
    {
        public string name;
        public string labelName;
        public int power;
        public int roundGroupNumber;
        public string category;
        public string subCategory;
        public int totalMagezine;
        public float magazineLoadDelay;
        public int bulletPerMagazine;
        public string magaZineLoadSound;
        public string bulletFireSound;
        public string gunSprite;
        public string magazineSprite;
        public string heroSprite;
        public bool isLocked;
        public int bulletPrice;
        public bool isSelect;
        public int number;
        public int level;
        public int basePrice;
        public int actualPower;
        public int upgradePrice;
        public int uniqueWeaponIndex;
        public int maxMagazine;
        public float magazineDelay;

        // Start is called before the first frame update
        public Weapon(string name, string labelName, int power, float magazineDelay, int roundGroupNumber,
                    string category, string subCategory, int totalMagezine,
                    float magazineLoadDelay, int bulletPerMagazine,
                    string magaZineLoadSound, string bulletFireSound,
                    string gunSprite, string magazineSprite, string heroSprite, bool isLocked,
                    int bulletPrice, bool isSelect, int number, int level, int basePrice,
                    int uniqueWeaponIndex, int maxMagazine = 8)
        {
            this.name = name;
            this.labelName = labelName;
            this.power = power;
            this.roundGroupNumber = roundGroupNumber;
            this.category = category;
            this.subCategory = subCategory;
            this.magazineLoadDelay = magazineLoadDelay;
            this.bulletPerMagazine = bulletPerMagazine;
            this.magaZineLoadSound = magaZineLoadSound;
            this.bulletFireSound = bulletFireSound;
            this.totalMagezine = totalMagezine;
            this.gunSprite = gunSprite;
            this.magazineSprite = magazineSprite;
            this.heroSprite = heroSprite;

            this.isLocked = isLocked;
            this.bulletPrice = bulletPrice;
            this.isSelect = isSelect;
            this.number = number;
            this.level = level;
            this.basePrice = basePrice;
            this.uniqueWeaponIndex = uniqueWeaponIndex;
            this.maxMagazine = maxMagazine;
            this.magazineDelay = magazineDelay;

            //
            this.upgradePrice = this.level * this.basePrice;
            this.actualPower = (this.level+1) * this.power;
        }
    }

}
