namespace UnitSpace.Attributes
{
    public class Strenght : Attribute
    {
        public Strenght()
        {
            value = 2;
        }
        public override void LevelUp(float value)
        {
            this.value += value;
            _level++;
        }
        public override void ConnectToUnit(Unit unit)
        {
            unit.healthComponent.giveIteractData.AddListener(ModifyIteractData);
        }
        protected override void ModifyIteractData(IteractData arg0)
        {
            arg0.damage = value;
        }
        public override string ToString()
        {
            return $"Strenght: | level {_level} | value {value} {base.ToString()}";
        }
    }
}
