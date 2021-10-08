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
            => $"Speed is: {value} speed exp is {xpProgressValue}";
    }
}
