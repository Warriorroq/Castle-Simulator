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
            unit.healthComponent.takeIteractData.AddListener(ModifyIteractData);
        }
        public override void LevelUp(float valueToAdd)
        {
            _level++;
            value += 2 * valueToAdd;
        }
        public override string ToString()
            => $"Defence: | level {_level} | value {value} {base.ToString()}";
        protected override void ModifyIteractData(IteractData arg0)
        {
            arg0.damage -= value;
            if (arg0.damage < 0)
                arg0.damage = 0;
            GiveExp(10);
        }
    }
}
