namespace UnitSpace.Attributes
{
    public class Speed : Attribute
    {
        public override void SetStartParams()        
            => value = 2;
        public override string ToString()
            => $"Speed is: {value} speed exp is {xpProgressValue}";
    }
}
