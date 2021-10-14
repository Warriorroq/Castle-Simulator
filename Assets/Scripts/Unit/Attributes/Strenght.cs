namespace UnitSpace.Attributes
{
    public class Strenght : Attribute
    {
        public override void LevelUpThis(float value)
        {
            this.value += value;
            _level++;
        }
        public override void SetStartParams()
        {
            value = 2;
        }
        public override string ToString()
        {
            return $"Strenght: | level {_level} | value {value}";
        }
    }
}
