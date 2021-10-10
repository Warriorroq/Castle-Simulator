using System.Collections.Generic;
using UnitSpace.Attributes;
namespace UnitSpace
{
    public class UnitAttributes
    {
        private List<Attribute> _attributes;
        private Unit _owner;
        public UnitAttributes(Unit owner)
        {
            _owner = owner;
            _attributes = new List<Attribute>();
        }
        public T GetAttribute<T>() where T : Attribute
        {
            foreach (var item in _attributes)
                if (item.GetType() == typeof(T))
                    return item as T;
            var newAttribute = Attribute.GetClearAttribute<T>();
            newAttribute.SetStartParams();
            _attributes.Add(newAttribute);
            return newAttribute;
        }
        public List<Attribute> GetAttributes()
            => _attributes;
        public override string ToString()
        {
            var text = string.Empty;
            foreach (var attribute in _attributes)
                text += attribute.ToString() + "\n";
            return text;
        }
    }
}
