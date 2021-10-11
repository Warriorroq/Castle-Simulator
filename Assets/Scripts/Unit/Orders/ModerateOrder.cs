using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitSpace.Interfaces;
using UnitSpace.Attributes;
namespace UnitSpace.Orders
{
    public class ModerateOrder : IOrder
    {
        private Unit _owner;
        private IOrder.OrderState _state;
        private Sensitivity _sensitivity;
        private Strenght _strenght;
        private Speed _speed;
        private Unit _target;
        private Vector3 _moderatePosition;
        public ModerateOrder(Vector3 moderatePosition)
        {
            _moderatePosition = moderatePosition;
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
            _sensitivity = _owner.attributes.GetAttribute<Sensitivity>();
            _strenght = _owner.attributes.GetAttribute<Strenght>();
            _speed = _owner.attributes.GetAttribute<Speed>();
            _state = IOrder.OrderState.Ready;
        }

        public void StartOrder()
        {
            _state = IOrder.OrderState.InProgress;
        }

        public void UpdateOrder()
        {
            if (!_target)
            {
                FindTarget();
                MoveToModeratePoint();
            }
            CheckTheCurrentModerateDistance();
            if (_target)
            {
                var distance = _owner.transform.position - _target.transform.position;
                AttackTarget(distance);
                MoveToTarget(distance);
            }
        }
        private void MoveToTarget(Vector3 distance)
        {
            if (distance.sqrMagnitude > 2f)
            {
                _owner.navMeshAgent.SetDestination(_target.transform.position);
                _owner.navMeshAgent.speed = _speed.value;
            }
        }
        private void AttackTarget(Vector3 distance)
        {
            if (distance.sqrMagnitude < 2f)
            {
                _target.healthComponent.TakeDamage(new IteractData()
                {
                    damage = _strenght.value
                });
            }
        }
        private void MoveToModeratePoint()
            =>_owner.navMeshAgent.SetDestination(_moderatePosition);
        private void CheckTheCurrentModerateDistance()
        {
            var distance = _owner.transform.position - _moderatePosition;
            var maxDistance = _sensitivity.value * _sensitivity.value;
            if (distance.sqrMagnitude > maxDistance)
                _target = null;
        }
        private void FindTarget()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_owner.transform.position, _sensitivity.value);
            foreach (var hit in hitColliders)
            {
                if (hit.TryGetComponent<Unit>(out var target) && target != _owner)
                {
                    _target = target;
                }
            }
        }
    }
}
