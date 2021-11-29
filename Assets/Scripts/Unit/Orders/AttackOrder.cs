using System.Collections.Generic;
using UnitSpace.Attributes;
using System.Linq;
namespace UnitSpace.Orders
{

    public class AttackOrder : Order
    {
        private List<Unit> _targets;
        private Unit _currentTarget;
        private Strenght _strenght;
        private IteractDistance _iteractDistance;
        public AttackOrder(IEnumerable<Unit> targets)
        {
            _targets = new List<Unit>(targets);
        }
        public override void SetUnitOwner(Unit owner)
        {
            base.SetUnitOwner(owner);
            _strenght = owner.attributes.GetOrCreateAttribute<Strenght>();
            _iteractDistance = owner.attributes.GetOrCreateAttribute<IteractDistance>();
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
            }

            //Target couldn't exist
            if (!_currentTarget) 
            {
                EndOrder();
                return;
            }

            AttackTarget();
        }
        private void AttackTarget()
        {
            var distance = _owner.transform.position - _currentTarget.transform.position;
            if (distance.sqrMagnitude <= _iteractDistance.value)
                GiveDamageAndEXPForAttack();
            else
            {
                _owner.unitOrders.AddOrder(new FollowToOrder(_currentTarget.transform));
                _owner.unitOrders.AddOrder(this);
                EndOrder();
            }
        }
        private void FindNearestTarget()
        {
            var unitTransform = _owner.transform.TakeNearestInSpace(_targets.Select(x => x.transform));
            _currentTarget = unitTransform.GetComponent<Unit>();
            _targets.Remove(_currentTarget);
        }
        private void GiveDamageAndEXPForAttack()
        {
            if(_owner.healthComponent.IsReadyToAttack())
            {
                _owner.healthComponent.GiveDamage(_currentTarget);
                _strenght.GiveExp(10);
            }
        }
    }
}
