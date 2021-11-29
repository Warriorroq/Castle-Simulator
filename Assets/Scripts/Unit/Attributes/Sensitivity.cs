namespace UnitSpace.Attributes
{
    public class Sensitivity : Attribute
    {
        public Sensitivity()
        {
            value = 10;
        }
        public override void LevelUp(float value)
        {
            this.value += value;
            _level++;
        }
        public override string ToString()
            => $"Sensitivity: | level {_level} | value {value}";
    }
}
