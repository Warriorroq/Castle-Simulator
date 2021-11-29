using UnityEngine;

namespace UnitSpace.Orders
{
    public class RecoverOrder : Order
    {      
        private HealthComponent _healthComponent;
        private float _timer;
        public override void SetUnitOwner(Unit owner)
        {
            base.SetUnitOwner(owner);
            _healthComponent = _owner.healthComponent;
        }
        protected override void OnUpdateOrder(){
            if (_timer > 0.5f)
            {
                _healthComponent.HealthHeal(2f);
                _timer = 0;
            }
            _timer += Time.deltaTime;
        }
    }
}
