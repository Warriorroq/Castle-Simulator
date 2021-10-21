using Resource;
using UnityEngine;
namespace UnitSpace
{
    public class ResourcePosition : MonoBehaviour
    {
        [SerializeField]private ResourceContainer _resource;
        [SerializeField] private int _maxWeight = 10;
        public void TakeResource(ResourceContainer resource)
        {
            if(_resource && resource.resourceData.type == _resource.resourceData.type)
            {
                AddResources(resource);
                return;
            }
            else
                DropResource();
            _resource = resource;
            SetResourceParentAndTransform();
        }
        public void DropResource()
        {
            _resource.ChangeSelectorState(true);
            var res = _resource;
            _resource.transform.SetParent(null);
            _resource = null;
            res.transform.position = transform.position + Vector3.left;
        }
        private void AddResources(ResourceContainer resource)
        {
            if(_resource.Amount + resource.Amount > _maxWeight)
            {
                resource.Amount = _resource.Amount + resource.Amount - _maxWeight;
                _resource.Amount = _maxWeight;
            }
            else
            {
                _resource.Amount += resource.Amount;
                Destroy(resource.gameObject);
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
