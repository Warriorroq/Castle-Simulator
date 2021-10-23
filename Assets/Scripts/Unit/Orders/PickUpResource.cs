using UnitSpace.Interfaces;
using Resource;
namespace UnitSpace.Orders
{
    public class PickUpResource : IOrder
    {
        private Unit _owner;
        private ResourceContainer _resource;
        private IOrder.OrderState _state;
        public PickUpResource(Unit resource)
        {
            resource.TryGetComponent(out _resource);
        }
        public void EndOrder()
        {
            _state = IOrder.OrderState.Finished;
            _owner.resourcePosition.TakeResource(_resource);
        }
        public IOrder.OrderState GetState()
            => _state;
        public void SetUnitOwner(Unit owner)
        {
            _owner = owner;
            _state = IOrder.OrderState.Ready;
        }
        public void StartOrder()
        {
            _state = IOrder.OrderState.InProgress;
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
        public void UpdateOrder(){}
    }
}
