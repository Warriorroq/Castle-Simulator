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
        protected override void OnUpdateOrder()
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
            if (distance.sqrMagnitude <= 3)
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
            _currentTarget = _owner.TakeNearest<Unit>(_targets);
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
