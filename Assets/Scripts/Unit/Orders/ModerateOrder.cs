using UnityEngine;
using UnitSpace.Interfaces;
using UnitSpace.Attributes;
using UnitSpace.Enums;
using System.Collections.Generic;
namespace UnitSpace.Orders
{
    public class ModerateOrder : Order
    {
        private Sensitivity _sensitivity;
        private Strenght _strenght;
        private IteractDistance _iteractDistance;
        private Speed _speed;
        private Unit _target;
        private Vector3 _moderatePosition;
        private List<UnitType> _enemyFraction;
        public ModerateOrder(Vector3 moderatePosition, params UnitType[] enemyFraction)
        {
            _moderatePosition = moderatePosition;
            _enemyFraction = new List<UnitType>(enemyFraction);
        }
        public override void EndOrder()
        {
            _state = IOrder.OrderState.Finished;
            if(_owner)
                _owner.navMeshAgent.Stop();
        }
        public override void SetUnitOwner(Unit owner)
        {
            base.SetUnitOwner(owner);
            _owner = owner;
            _sensitivity = _owner.unitAttributes.GetOrCreateAttribute<Sensitivity>();
            _strenght = _owner.unitAttributes.GetOrCreateAttribute<Strenght>();
            _iteractDistance = owner.unitAttributes.GetOrCreateAttribute<IteractDistance>();
            _speed = _owner.unitAttributes.GetOrCreateAttribute<Speed>();
        }

        public override void StartOrder()
        {
            base.StartOrder();
            _owner.navMeshAgent.isStopped = false;
        }

        protected override void OnUpdateOrder()
        {
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
            if (distance.sqrMagnitude > _iteractDistance.value)
            {
                _owner.navMeshAgent.SetDestination(_target.transform.position);
                _owner.navMeshAgent.speed = _speed.value;
            }
        }
        private void AttackTarget(Vector3 distance)
        {
            if (distance.sqrMagnitude < _iteractDistance.value && _owner.healthComponent.IsReadyToAttack())
            {
                Vector3 position = (_target.transform.position + _owner.transform.position)/2f;
                GameObject.Instantiate(ResourceEffects.Hit, position, Quaternion.identity);
                _owner.healthComponent.GiveDamage(_target);
                _strenght.GiveExp(10);
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
                if (hit.TryGetComponent<Unit>(out var target) && TargetIsNormal(target))
                {
                    _target = target;
                    return;
                }
            }
        }
        private bool TargetIsNormal(Unit target)
            => target != _owner && _enemyFraction.Contains(target.fraction);
    }
}
