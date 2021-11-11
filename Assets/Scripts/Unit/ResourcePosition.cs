using Resource;
using UnityEngine;
namespace UnitSpace
{
    public class ResourcePosition : MonoBehaviour
    {
        [SerializeField] private ResourceContainer _resource;
        [SerializeField] private int _maxWeight = 10;
        public void TakeResource(ResourceContainer resource)
        {
            if(_resource)
            {
                if (resource.resourceType != _resource.resourceType)
                    DropResource();
            }
            _resource = resource;
            SetResourceParentAndTransform();
        }
        public void DropResource()
        {
            if(_resource)
            {
                _resource.ChangeSelectorState(true);
                _resource.transform.SetParent(null);
                _resource.transform.position = transform.position + Vector3.left;
                _resource = null;
            }
        }
        private void SetResourceParentAndTransform()
        {
            _resource.transform.SetParent(transform);
            _resource.transform.rotation = transform.rotation;
            _resource.transform.localPosition = Vector3.zero;
            _resource.ChangeSelectorState(false);
        }
    }
}