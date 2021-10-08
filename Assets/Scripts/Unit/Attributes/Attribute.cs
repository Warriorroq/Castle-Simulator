using System.Runtime.Serialization;
namespace UnitSpace.Attributes
{
    public abstract class Attribute
    {
        public float value;
        public float xpProgressValue;
        protected int _level;
        public Attribute(float value = 1, float xpProgressValue = 0)
        {
            _level = 0;
            this.value = value;
            this.xpProgressValue = xpProgressValue;
        }
        public abstract void SetStartParams();
        public static T GetClearAttribute<T>() where T : Attribute
            => FormatterServices.GetUninitializedObject(typeof(T)) as T;
        public abstract void LevelUpThis(float value);
    }
}
