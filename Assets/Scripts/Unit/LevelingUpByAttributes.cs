using UnitSpace.Attributes;
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
        public void LevelUp(Attribute attribute)
        {
            attribute.LevelUp(1);
        }
        private void LevelUpSkillsIn(Unit unit)
        {
            //TODO: this
        }
    }
}
