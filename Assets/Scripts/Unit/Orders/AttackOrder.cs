using System.Collections.Generic;
using UnitSpace.Attributes;
using System.Linq;
using UnityEngine;

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
            _strenght = owner.unitAttributes.GetOrCreateAttribute<Strenght>();
            _iteractDistance = owner.unitAttributes.GetOrCreateAttribute<IteractDistance>();
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
            var unitTransform = _owner.transform.TakeNearestInSpace(_targets.Where(x => x).Select(x => x.transform));
            if (unitTransform is null)
                return;
            _currentTarget = unitTransform.GetComponent<Unit>();
            _targets.Remove(_currentTarget);
        }
        private void GiveDamageAndEXPForAttack()
        {
            if(_owner.healthComponent.IsReadyToAttack())
            {
                Vector3 position = (_currentTarget.transform.position + _owner.transform.position) / 2f;
                var effect = GameObject.Instantiate(ResourceEffects.Hit, position, Quaternion.identity);
                _owner.healthComponent.GiveDamage(_currentTarget);
                _strenght.GiveExp(10);
            }
        }
    }
}
