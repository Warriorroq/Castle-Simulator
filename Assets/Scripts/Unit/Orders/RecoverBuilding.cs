using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitSpace.Orders
{
    public class RecoverBuilding : Order
    {
        private Unit _building;
        public RecoverBuilding(Unit building) {
            _building = building;
        }
        public override void SetUnitOwner(Unit owner)
        {
            if(_owner is null)
                base.SetUnitOwner(owner);
        }
        public override void StartOrder()
        {
            var vector = _building.transform.position - _owner.transform.position;
            if(vector.sqrMagnitude > 3)
            {
                _owner.unitOrders.AddToStart(new MoveToOrder(_building.transform.position));
                EndOrder();
            }
        }
        protected override void OnUpdateOrder()
        {
            if (_owner.healthComponent.CanUseStateAndReloadIteract())
                _owner.healthComponent.HealthHeal(3f);
            if(_building.healthComponent.HealthIsOverHealed)
                EndOrder();
        }
    }
}
