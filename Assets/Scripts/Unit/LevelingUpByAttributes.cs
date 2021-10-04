namespace UnitSpace
{
    public class LevelingUpByAttributes
    {
        private LevelingUpByAttributes _thisInstance;
        public LevelingUpByAttributes GetInstance()
        {
            if (_thisInstance is null)
                _thisInstance = new LevelingUpByAttributes();
            return _thisInstance;
        }
        public void CheckOnLevelUp(Unit unit)
        {
            //do something
        }
        private void LevelUpAttributesIn(Unit unit)
        {
            //do something
        }
        private void LevelUpSkillsIn(Unit unit)
        {
            //do something
        }
    }
}
