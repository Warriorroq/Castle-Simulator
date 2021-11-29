namespace UnitSpace.Attributes
{
    public class IteractDistance : Attribute
    {
        public IteractDistance()
        {
            value = 3f;
            _level = 0;
        }
        public override void LevelUp(float value)
        {
            value += 1f;
        }
        public override string ToString()
            => $"Distance: | level {_level} | value {value} {base.ToString()}";
    }
}
