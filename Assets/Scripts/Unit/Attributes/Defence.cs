namespace UnitSpace.Attributes
{
    public class Defence : Attribute
    {
        public Defence()
        {
            value = 0;
            _level = 0;
        }
        public override void ConnectToUnit(Unit unit)
        {
            unit.healthComponent.takeIteractData.AddListener(UseDefence);
        }

        private void UseDefence(IteractData arg0)
        {
            arg0.damage -= value;
        }

        public override void LevelUpThis(float value)
        {
            _level++;
            this.value++;
        }
    }
}
