namespace UnitSpace.Attributes
{
    public class Recovery : Attribute
    {
        public Recovery()
        {
            value = 1.5f;
            _level = 0;
        }
        public override void LevelUp(float valueToAdd)
        {
            _level++;
            value += 0.5f;
        }
        public override string ToString()
            => $"Recovery: | level {_level} | value {value} {base.ToString()}";
    }
}
