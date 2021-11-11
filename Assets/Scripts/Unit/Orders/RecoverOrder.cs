using UnityEngine;
using UnitSpace.Attributes;

namespace UnitSpace.Orders
{
    public class RecoverOrder : Order
    {      
        private Health _health;
        private float _timer;
        public override void SetUnitOwner(Unit owner)
        {
            base.SetUnitOwner(owner);
            _health = _owner.attributes.GetOrCreateAttribute<Health>();
        }
        protected override void OnUpdateOrder(){
            if (_timer > 3.5f)
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
