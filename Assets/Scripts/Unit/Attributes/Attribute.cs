namespace UnitSpace.Attributes
{
    public abstract class Attribute
    {
        public float value;
        public float xpProgressValue;
        protected int _level;
        public Attribute(float value = 1, float xpProgressValue = 0)
        {
            _level = 0;
            this.value = value;
            this.xpProgressValue = xpProgressValue;
        }
        public virtual void ConnectToUnit(Unit unit){}
        public abstract void LevelUpThis(float value);
    }
}
