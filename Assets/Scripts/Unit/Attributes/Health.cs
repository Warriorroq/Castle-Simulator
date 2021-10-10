using UnitSpace;
using UnitSpace.Attributes;
namespace UnitSpace.Attributes
{
    public class Health : Attribute
    {
        public float currentHp;
        public override void LevelUpThis(float value)
        {
            this.value += 10;
            currentHp += 10;
            _level++;
        }
        public override void SetStartParams()
        {
            value = 10;
            currentHp = value;
        }
        public override string ToString()
        {
            return $"Health: | level {_level} | value {value}";
        }
    }
}
