namespace UnitSpace.Attributes
{
    public class Defence : Attribute
    {
        public Defence()
        {
            value = 1;
            _level = 0;
        }
        public override void ConnectToUnit(Unit unit)
        {
            unit.healthComponent.takeIteractData.AddListener(UseDefence);
        }

        private void UseDefence(IteractData arg0)
        {
            arg0.damage -= value;
            if (arg0.damage < 0)
                arg0.damage = 0;
            xpProgressValue += 10;
        }
        public override void LevelUpThis(float value)
        {
            _level++;
            this.value += 2 * value;
        }
        public override string ToString()
            => $"Defence: | level {_level} | value {value}";
    }
}
