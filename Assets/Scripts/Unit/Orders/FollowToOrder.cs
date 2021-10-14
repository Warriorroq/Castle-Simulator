using System.Collections;
using System.Collections.Generic;
using UnitSpace.Interfaces;
using UnityEngine;
using UnitSpace.Attributes;
namespace UnitSpace.Orders
{

    public class FollowToOrder : IOrder
    {
        private Unit _owner;
        private Unit _target;
        private Vector3 _lastTargetPosition;
        private IOrder.OrderState _state;
        private Speed _ownerSpeed;
        public FollowToOrder(Unit target) {
            _target = target;
        }
        public void EndOrder()
        {
            _state = IOrder.OrderState.Finished;
            _owner.navMeshAgent.isStopped = true;
        }
        public IOrder.OrderState GetState()
            => _state;
        public void SetUnitOwner(Unit owner)
            =>_owner = owner;
        public void StartOrder()
        {
            if (_target is null)
            {
                EndOrder();
                return;
            }
            _state = IOrder.OrderState.InProgress;
            _owner.navMeshAgent.SetDestination(_target.transform.position);
            _ownerSpeed = _owner.attributes.GetAttribute<Speed>();
            _owner.navMeshAgent.speed = _ownerSpeed.value;
            _owner.navMeshAgent.isStopped = false;
        }
        public void UpdateOrder()
        {
            if(!_target)
            {
                EndOrder();
                return;
            }

            var comparePositions = _lastTargetPosition - _target.transform.position;
            if(comparePositions.sqrMagnitude > 1f)
                _owner.navMeshAgent.SetDestination(_target.transform.position);

            var distance = _owner.transform.position - _target.transform.position;
            if (distance.sqrMagnitude < 1f)
                EndOrder();

            _owner.navMeshAgent.speed = _ownerSpeed.value;
        }
    }
}
