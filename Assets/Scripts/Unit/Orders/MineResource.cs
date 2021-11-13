﻿using UnityEngine;

namespace UnitSpace.Orders
{
    public class MineResource : Order
    {
        private ResourceMineContainer _target;
        private ResourceSender _sender;
        private HealthComponent _healthComponent;
        public MineResource(ResourceMineContainer resource, ResourceSender resourceSender)
        {
            _target = resource;
            _sender = resourceSender;
        }
        public override void SetUnitOwner(Unit owner)
        {
            base.SetUnitOwner(owner);
            _healthComponent = _owner.GetComponent<HealthComponent>();
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
                _owner.unitOrders.AddOrder(new MoveToOrder(_sender.transform.position));
                _owner.unitOrders.AddOrder(new GiveResourceToSender(_sender));
                _owner.unitOrders.AddOrder(this);
                EndOrder();
            }
            else if(distance.sqrMagnitude > 3 || !_owner.resourcePosition.HasCurrency)
            {
                EndOrder();
                _owner.unitOrders.AddOrder(new MoveToOrder(_target.transform.position));
                _owner.unitOrders.AddOrder(this);
            }
        }
    }
}
