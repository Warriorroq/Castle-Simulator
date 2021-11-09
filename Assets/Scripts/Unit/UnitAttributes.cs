using System.Collections.Generic;
using UnitSpace.Attributes;
namespace UnitSpace
{
    public class UnitAttributes
    {
        public List<Attribute> attributes;
        private Unit _owner;
        public Attribute this[string Name]
        {
            get
            {
                foreach (var param in attributes)
                    if (param.Name == Name)
                        return param;
                return null;
            }
        }
        public UnitAttributes(Unit owner)
        {
            _owner = owner;
            attributes = new List<Attribute>();
        }
        public T GetOrCreateAttribute<T>() where T : Attribute, new()
        {
            foreach (var item in attributes)
                if (item.GetType() == typeof(T))
                    return item as T;
            var newAttribute = new T();
            attributes.Add(newAttribute);
            newAttribute.ConnectToUnit(_owner);
            return newAttribute;
        }
        public override string ToString()
        {
            var text = string.Empty;
            foreach (var attribute in attributes)
                text += attribute.ToString() + "\n";
            return text;
        }
    }
}
