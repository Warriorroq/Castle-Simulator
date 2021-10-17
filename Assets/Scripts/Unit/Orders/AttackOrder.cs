using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitSpace.Orders;
using UnitSpace;
using UnitSpace.Interfaces;
using UnitSpace.Attributes;
namespace UnitSpace.Orders
{

    public class AttackOrder : IOrder
    {
        private Unit _owner;
        private Unit _target;
        private Strenght _strenght;
        private IOrder.OrderState _state;
        public AttackOrder(Unit target)
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
            _strenght = owner.attributes.GetOrCreateAttribute<Strenght>();
        }
        public void StartOrder()
        {
            _state = IOrder.OrderState.InProgress;
        }
        public void UpdateOrder()
        {
            if (!_target)
            {
                EndOrder();
                return;
            }

            var distance = _owner.transform.position - _target.transform.position;

            if (distance.sqrMagnitude > 2)
            {
                _owner.unitOrders.AddOrder(new FollowToOrder(_target));
                _owner.unitOrders.AddOrder(new AttackOrder(_target));
                EndOrder();
                return;
            }
            Attack();
        }
        private void Attack()
        {
            if(_owner.healthComponent.IsReadyToAttack())
            {
                _owner.healthComponent.GiveDamage(_target);
                _strenght.xpProgressValue += 10;
            }
        }
    }
}
