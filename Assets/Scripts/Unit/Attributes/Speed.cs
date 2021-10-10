namespace UnitSpace.Attributes
{
    public class Speed : Attribute
    {
        public override void LevelUpThis(float value)
        {
            this.value += value;
            _level++;
        }
        public override void SetStartParams()        
            => value = 2;
        public override string ToString()
        {
            return $"Speed: | level {_level} | value {value}";
        }
    }
}
