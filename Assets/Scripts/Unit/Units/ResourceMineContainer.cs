using UnityEngine;
using Resource;
namespace UnitSpace
{
    public class ResourceMineContainer : MonoBehaviour
    {
        [SerializeField] private int _resourceCount;
        [SerializeField] private ResourceObject _prefab;
        public ResourceObject GetResource()
        {
            _resourceCount--;
            if (_resourceCount == 0)
                Destroy(gameObject);
            return Instantiate(_prefab, transform.position, transform.rotation);
        }
        public override string ToString()
        {
            return $"resource: {_resourceCount} | type: {_prefab.resourceType}";
        }
    }
}
