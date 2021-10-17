using UnityEngine;
using UnitSpace.Interfaces;
using UnitSpace.Attributes;

namespace UnitSpace.Orders
{
    public class RecoverOrder : IOrder
    {
        private Unit _owner;
        private Health _health;
        private IOrder.OrderState _state;
        private float _timer;
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
            _health = _owner.attributes.GetOrCreateAttribute<Health>();
        }
        public void StartOrder()
        {
            _state = IOrder.OrderState.InProgress;
        }
        public void UpdateOrder(){
            if(_timer > 3.5f)
            {
                Heal();
                _timer = 0;
            }
            _timer += Time.deltaTime;
        }
        private void Heal()
        {
            if (_health.value <= _health.currentHp + 1)
            {
                _health.currentHp = _health.value;
                EndOrder();
            }
            else
                _health.currentHp++;
        }
    }
}
