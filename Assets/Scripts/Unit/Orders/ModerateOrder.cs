using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitSpace.Interfaces;
using UnitSpace.Attributes;
using UnitSpace.Enums;
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
        private UnitFraction _enemyFraction;
        public ModerateOrder(Vector3 moderatePosition, UnitFraction enemyFraction)
        {
            _moderatePosition = moderatePosition;
            _enemyFraction = enemyFraction;
        }
        public void EndOrder()
        {
            _state = IOrder.OrderState.Finished;
            _owner.navMeshAgent.Stop();
        }
        public IOrder.OrderState GetState()
            => _state;
        public void SetUnitOwner(Unit owner)
        {
            _owner = owner;
            _sensitivity = _owner.attributes.GetOrCreateAttribute<Sensitivity>();
            _strenght = _owner.attributes.GetOrCreateAttribute<Strenght>();
            _speed = _owner.attributes.GetOrCreateAttribute<Speed>();
            _state = IOrder.OrderState.Ready;
        }

        public void StartOrder()
        {
            _state = IOrder.OrderState.InProgress;
            _owner.navMeshAgent.isStopped = false;
        }

        public void UpdateOrder()
        {
            //Debug.Log($"{_target}");
            if (!_target)
            {
                FindTarget();
                if(!_target)
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
            if (distance.sqrMagnitude < 2f && _owner.healthComponent.IsReadyToAttack())
            {
                _owner.healthComponent.GiveDamage(_target);
                _strenght.xpProgressValue += 10;
            }
        }
        private void MoveToModeratePoint()
            =>_owner.navMeshAgent.SetDestination(_moderatePosition);
        private void CheckTheCurrentModerateDistance()
        {
            var distance = _owner.transform.position - _moderatePosition;
            var maxDistance = _sensitivity.value * _sensitivity.value * 2;
            if (distance.sqrMagnitude > maxDistance)
                _target = null;
        }
        private void FindTarget()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_owner.transform.position, _sensitivity.value);
            foreach (var hit in hitColliders)
            {               
                if (hit.TryGetComponent<Unit>(out var target) && TryTakeTarget(target))
                {
                    _target = target;
                    return;
                }
            }
        }
        private bool TryTakeTarget(Unit target)
            => target != _owner && target.fraction == _enemyFraction;
    }
}
