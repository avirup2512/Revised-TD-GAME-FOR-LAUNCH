using System.Collections;
using System.Collections.Generic;
namespace Assets.Script.globalVar
{
    [System.Serializable]
    public class SelectedWeapon
    {
        public Weapon selectedPrimary;
        public Weapon selectedSecondary;
        public Weapon selectedSpecial;
        public SelectedWeapon(Weapon selectedPrimary, Weapon selectedSecondary, Weapon selectedSpecial)
        {
            this.selectedPrimary = selectedPrimary;
            this.selectedSecondary = selectedSecondary;
            this.selectedSpecial = selectedSpecial;
        }
    }

}
