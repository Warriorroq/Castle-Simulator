using System.Runtime.Serialization;

namespace UnitSpace.Attributes
{
    public abstract class Attribute
    {
        public float value;
        public float xpProgressValue;
        private Unit _owner;
        public Attribute(float value = 1, float xpProgressValue = 0, Unit owner = null)
        {
            this.value = value;
            this.xpProgressValue = xpProgressValue;
            _owner = owner;
        }
        public static T GetClearAttribute<T>() where T : Attribute
            => FormatterServices.GetUninitializedObject(typeof(T)) as T;

    }
}
