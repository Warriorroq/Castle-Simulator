using UnitSpace;
using UnitSpace.Enums;
using UnityEngine;
namespace Resource
{
    public class ResourceObject : MonoBehaviour
    {
        public bool IsAvaliable => !transform.parent;
        public ResourceType resourceType;
        public int Amount = 1;
        private Unit _owner;
        public void ChangeSelectorState(bool state)
        {
            _owner?.unitSelector.ChangeSelectorColor(Color.white);
            _owner?.unitSelector.gameObject.SetActive(state);
        }
        private void Start()
        {
            TryGetComponent(out _owner);
        }
    }
}
