using System.Runtime.Serialization;
namespace UnitSpace.Attributes
{
    public abstract class Attribute
    {
        public float value;
        public float xpProgressValue;
        public Attribute(float value = 1, float xpProgressValue = 0)
        {
            this.value = value;
            this.xpProgressValue = xpProgressValue;
        }
        public abstract void SetStartParams();
        public static T GetClearAttribute<T>() where T : Attribute
            => FormatterServices.GetUninitializedObject(typeof(T)) as T;
    }
}
