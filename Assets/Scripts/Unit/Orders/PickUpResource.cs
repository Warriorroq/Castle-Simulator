using UnitSpace.Interfaces;
using Resource;
namespace UnitSpace.Orders
{
    public class PickUpResource : Order
    {
        private ResourceContainer _resource;
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
            var distance = _owner.transform.position - _resource.transform.position;
            if(distance.sqrMagnitude > 2f && _resource.IsAvaliable)
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
