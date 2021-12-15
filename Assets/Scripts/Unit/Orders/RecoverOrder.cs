using UnitSpace.Attributes;
using UnityEngine;

namespace UnitSpace.Orders
{
    public class RecoverOrder : Order
    {      
        private HealthComponent _healthComponent;
        private Recovery _recovery;
        private float _timer;
        public override void SetUnitOwner(Unit owner)
        {
            base.SetUnitOwner(owner);
            _healthComponent = _owner.healthComponent;
            _recovery = _owner.attributes.GetOrCreateAttribute<Recovery>();
        }
        protected override void OnUpdateOrder(){
            if (_timer > 0.5f && !_healthComponent.HealthIsFullHealed)
            {
                _healthComponent.HealthHeal(_recovery.value);
                _recovery.GiveExp(_recovery.value * 10);
                _timer = 0;                
                GameObject.Instantiate(ResourceEffects.Healing, _owner.transform.position, Quaternion.identity);
            }
            _timer += Time.deltaTime;
        }
    }
}
