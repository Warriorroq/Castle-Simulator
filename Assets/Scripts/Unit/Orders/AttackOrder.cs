using System.Collections.Generic;
using UnitSpace.Attributes;

namespace UnitSpace.Orders
{

    public class AttackOrder : Order
    {
        private List<Unit> _targets;
        private Unit _currentTarget;
        private Strenght _strenght;
        public AttackOrder(IEnumerable<Unit> targets)
        {
            _targets = new List<Unit>(targets);
        }
        public override void SetUnitOwner(Unit owner)
        {
            base.SetUnitOwner(owner);
            _strenght = owner.attributes.GetOrCreateAttribute<Strenght>();
        }
        public override void UpdateOrder()
        {
            if (!_currentTarget)
            {
                if (_targets.Count == 0)
                {
                    EndOrder();
                    return;
                }
                FindNearestTarget();
                if (!_currentTarget)
                {
                    EndOrder();
                    return;
                }
            }
            if (_currentTarget)
                AttackTarget();
        }
        private void AttackTarget()
        {
            var distance = _owner.transform.position - _currentTarget.transform.position;
            if (distance.sqrMagnitude < 2)
                GiveDamageAndEXPForAttack();
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
                if (!target)
                    continue;
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
        private void GiveDamageAndEXPForAttack()
        {
            if(_owner.healthComponent.IsReadyToAttack())
            {
                _owner.healthComponent.GiveDamage(_currentTarget);
                _strenght.xpProgressValue += 10;
            }
        }
    }
}
