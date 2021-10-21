using UnityEngine;
using UnitSpace.Enums;
namespace Resource
{
    [CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resource")]
    public class ResourceData : ScriptableObject
    {
        public string desription;
        public ResourceType type;
    }
}
