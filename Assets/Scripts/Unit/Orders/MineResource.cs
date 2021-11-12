namespace UnitSpace.Orders
{
    public class MineResource : Order
    {
        private ResourceMineContainer _target;
        private HealthComponent _healthComponent;
        private MoveToOrder _moveOrder;
        public MineResource(ResourceMineContainer resource)
        {
            _target = resource;
        }
        public override void SetUnitOwner(Unit owner)
        {
            base.SetUnitOwner(owner);
            _healthComponent = _owner.GetComponent<HealthComponent>();
            if(_moveOrder is null)
                _moveOrder = new MoveToOrder(_owner.transform.position);
        }
        protected override void OnUpdateOrder()
        {
            if (!_target)
            {
                EndOrder();
                return;
            }
            var distance = _owner.transform.position - _target.transform.position;
            if (distance.sqrMagnitude <= 3 && _healthComponent.CanUseStateAndReloadIt())
            {
                var resource = _target.GetResource();
                _owner.resourcePosition.TakeResource(resource);
                EndOrder();
            }
            else if(distance.sqrMagnitude > 3 || _owner.resourcePosition.HasCurrency)
            {
                EndOrder();
                _owner.unitOrders.AddOrder(new MoveToOrder(_target.transform.position));
                _owner.unitOrders.AddOrder(this);
            }
            if (!_owner.resourcePosition.HasCurrency)
            {
                _owner.unitOrders.AddOrder(_moveOrder);
                _owner.unitOrders.AddOrder(new DropResource());
                _owner.unitOrders.AddOrder(this);
            }

        }
    }
}
