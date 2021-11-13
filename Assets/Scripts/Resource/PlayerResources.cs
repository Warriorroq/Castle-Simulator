using System.Collections.Generic;
using UnityEngine;
using UnitSpace.Enums;
using System;

namespace Resource
{
    public class PlayerResources : MonoBehaviour
    {
        private Dictionary<ResourceType, int> _takedUnits;
        public void TakeResources(ResourceType resourceType, int howMany)
        {
            _takedUnits[resourceType] += howMany;
            Debug.Log(ToString());
        }
        public bool IsAvailable(ResourceType resourceType, int howMany)
            => _takedUnits[resourceType] >= howMany;
        public override string ToString()
        {
            string str = string.Empty;
            foreach (var i in _takedUnits)
                str += $"{i.Key} : {i.Value} ";
            return str;
        }
        private void Awake()
        {
            _takedUnits = new Dictionary<ResourceType, int>();
            foreach (var type in Enum.GetValues(typeof(ResourceType)))
                _takedUnits.Add((ResourceType)type, 0);
            Debug.Log(ToString());
        }
    }
}
