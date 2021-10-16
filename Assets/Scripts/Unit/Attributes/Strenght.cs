namespace UnitSpace.Attributes
{
    public class Strenght : Attribute
    {
        public Strenght()
        {
            value = 2;
        }
        public override void LevelUpThis(float value)
        {
            this.value += value;
            _level++;
        }        
        public override string ToString()
        {
            return $"Strenght: | level {_level} | value {value}";
        }
    }
}
