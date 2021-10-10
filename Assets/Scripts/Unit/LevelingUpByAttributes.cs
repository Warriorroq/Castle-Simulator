using System;
using System.Linq;
namespace UnitSpace
{
    public class LevelingUpByAttributes
    {
        private static LevelingUpByAttributes _thisInstance;
        private const float expToLevelUp = 1000;
        public static LevelingUpByAttributes GetInstance()
        {
            if (_thisInstance is null)
                _thisInstance = new LevelingUpByAttributes();
            return _thisInstance;
        }
        public void CheckOnLevelUp(Unit unit)
        {
            CountUnitTotalExp(unit, out var totalExp);
            if(totalExp >= expToLevelUp)
            {
                LevelUpAttributesIn(unit);
            }
        }
        public void CountUnitTotalExp(Unit unit, out float currentExp)
        {
            currentExp = 0;
            foreach(var attribute in unit.attributes.GetAttributes())
            {
                currentExp += attribute.xpProgressValue;
            }
        }
        private void LevelUpAttributesIn(Unit unit)
        {
            foreach (var attribute in unit.attributes.GetAttributes())
            {
                attribute.value += attribute.xpProgressValue / expToLevelUp;
                attribute.xpProgressValue = 0;
            }
        }
        private void LevelUpSkillsIn(Unit unit)
        {
            //do something
        }
    }
}
