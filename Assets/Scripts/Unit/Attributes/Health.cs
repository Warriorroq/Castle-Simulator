namespace UnitSpace.Attributes
{
    public class Health : Attribute
    {
        public float currentHp;
        public bool IsOverHealed => currentHp > value;
        public Health()
        {
            value = 10;
            currentHp = value;
        }
        public override void LevelUp(float value)
        {
            this.value += 10;
            currentHp += 10;
            _level++;
        }
        public void Heal(float Amount)
        {
            if (!IsOverHealed)
                currentHp += Amount;
        }
        public override string ToString()
        {
            return $"Health: | level {_level} | value {value} / {currentHp}";
        }
        public override void LoadAttribute(AttributesParam param)
        {
            value = param.maxValue;
            currentHp = value;
        }
    }
}
