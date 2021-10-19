using System.Collections.Generic;
using UnitSpace.Interfaces;
using UnitSpace.Attributes;
namespace UnitSpace.Orders
{

    public class AttackOrder : IOrder
    {
        private Unit _owner;
        private List<Unit> _targets;
        private Unit _currentTarget;
        private Strenght _strenght;
        private IOrder.OrderState _state;
        public AttackOrder(params Unit[] targets)
        {
            _targets = new List<Unit>(targets);
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
            FindNearestTarget();
        }
        public void UpdateOrder()
        {
            ClearTargets();
            if (_targets.Count == 0)
            {
                EndOrder();
                return;
            }
            if (!_currentTarget)
            {
                FindNearestTarget();
                if (!_currentTarget)
                    return;
            }
            var distance = _owner.transform.position - _currentTarget.transform.position;
            if (distance.sqrMagnitude < 2)
                Attack();
            else
            {
                _owner.unitOrders.AddOrder(new FollowToOrder(_currentTarget));
                _owner.unitOrders.AddOrder(this);
                EndOrder();
            }
        }
        private void FindNearestTarget()
        {
            var minMagnitude = float.MaxValue;
            foreach(var target in _targets)
            {
                var distance = _owner.transform.position - target.transform.position;
                var distanceMagnitude = distance.sqrMagnitude;
                if (distanceMagnitude < minMagnitude)
                {
                    minMagnitude = distanceMagnitude;
                    _currentTarget = target;
                }
            }
            _targets.Remove(_currentTarget);
        }
        private void ClearTargets()
        {
            List<Unit> clearTargets = new List<Unit>();
            foreach(var target in _targets)
            {
                if (!target)
                    clearTargets.Add(target);
            }
            foreach (var target in clearTargets)
                _targets.Remove(target);
        }
        private void Attack()
        {
            if(_owner.healthComponent.IsReadyToAttack())
            {
                _owner.healthComponent.GiveDamage(_currentTarget);
                _strenght.xpProgressValue += 10;
            }
        }
    }
}
