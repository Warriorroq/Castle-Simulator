namespace UnitSpace.Attributes
{
    public abstract class Attribute
    {
        public float value;
        public string Name => GetType().Name;
        private float xpProgressValue;
        protected int _level;
        public Attribute(float value = 1, float xpProgressValue = 0)
        {
            _level = 0;
            this.value = value;
            this.xpProgressValue = xpProgressValue;
        }
        public virtual void GiveExp(float xpAmount)
        {
            xpProgressValue += xpAmount;
            if (xpProgressValue >= 1000)
                LevelingUpByAttributes.GetInstance().LevelUp(this);
        }
        public void SetExp(float value) => xpProgressValue = value;
        public virtual void ConnectToUnit(Unit unit){}
        public abstract void LevelUp(float value);
        public virtual void LoadAttribute(AttributesParam param)
        {
            value = param.value;
        }
        protected virtual void ModifyIteractData(IteractData arg0) { }
        public override string ToString()
            => $"exp is {xpProgressValue} | {base.ToString()}";
    }
}
