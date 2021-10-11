namespace UnitSpace.Attributes
{
    public class Sensitivity : Attribute
    {
        public override void LevelUpThis(float value)
        {
            this.value += value;
            _level++;
        }
        public override void SetStartParams()
        {
            value = 5;
        }
        public override string ToString()
            => $"Sensitivity: | level {_level} | value {value}";
    }
}
