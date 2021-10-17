using UnityEngine;
using UnitSpace.Interfaces;
namespace UnitSpace.Orders
{
    public class RecoverOrder : IOrder
    {
        private Unit _owner;
        private Unit _target;
        private HealthComponent _healthComponent;
        private IOrder.OrderState _state;
        public RecoverOrder(Unit target)
        {
            _target = target;
        }
        public void EndOrder()
        {
            _state = IOrder.OrderState.Finished;
        }
        public IOrder.OrderState GetState()
            => _state;
        public void SetUnitOwner(Unit owner)
        {
            _owner = owner;
            _state = IOrder.OrderState.Ready;
            _healthComponent = _owner.healthComponent;
        }
        public void StartOrder()
        {
            _state = IOrder.OrderState.InProgress;
        }
        public void UpdateOrder()
        {
            
        }        
    }
}
