using UnitSpace.Interfaces;
using Resource;
using UnitSpace.Attributes;

namespace UnitSpace.Orders
{
    public class PickUpResource : Order
    {
        private ResourceObject _resource;
        public PickUpResource(Unit resource)
        {
            resource.TryGetComponent(out _resource);
        }
        public override void EndOrder()
        {
            base.EndOrder();
            _owner.resourcePosition.TakeResource(_resource);
        }
        public override void StartOrder()
        {
            base.StartOrder();
            var iteractDistance = _owner.unitAttributes.GetOrCreateAttribute<IteractDistance>();
            var distance = _owner.transform.position - _resource.transform.position;
            if(distance.sqrMagnitude > iteractDistance.value && _resource.IsAvaliable)
            {
                _owner.unitOrders.StopImmediate();
                MoveToResourceAlgorithm();
                return;
            }
            EndOrder();
        }
        private void MoveToResourceAlgorithm()
        {
            _owner.unitOrders.AddToStart(new MoveToOrder(_resource.transform.position), this);
        }
    }
}
