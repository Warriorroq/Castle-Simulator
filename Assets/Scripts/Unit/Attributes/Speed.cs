namespace UnitSpace.Attributes
{
    public class Speed : Attribute
    {
        public Speed()
        {
            value = 2;
        }
        public override void LevelUp(float value)
        {
            this.value += value;
            _level++;
        }
        public override string ToString()
        {
            return $"Speed: | level {_level} | value {value}";
        }
    }
}
