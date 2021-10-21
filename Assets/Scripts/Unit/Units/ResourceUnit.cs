using UnitSpace.Enums;
namespace UnitSpace
{
    public class ResourceUnit : Unit
    {
        public void Start()
        {
            unitSelector = GetComponentInChildren<UnitSelector>();
            if (fraction == UnitFraction.None)
                fraction = UnitFraction.Resources;
        }
    }
}
