namespace UnitSpace.Attributes
{
    public class Distance : Attribute
    {
        public Distance()
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
