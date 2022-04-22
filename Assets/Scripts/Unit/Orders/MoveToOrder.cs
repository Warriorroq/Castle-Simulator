using UnityEngine;
using UnitSpace.Interfaces;
using UnitSpace.Attributes;
namespace UnitSpace.Orders
{
    public class MoveToOrder : Order
    {
        private IteractDistance _iteractDistance;
        private Speed _ownerSpeed;
        private Vector3 _target;
        private float points;
        public MoveToOrder(Vector3 target)
        {
            _target = target;
        }
        public override void EndOrder()
        {
            base.EndOrder();
            _owner.navMeshAgent.isStopped = true;
            _ownerSpeed.GiveExp(points);
        }

        public override void StartOrder()
        {
            points = Vector3.Distance(_owner.transform.position, _target) * 10;
            _iteractDistance = _owner.unitAttributes.GetOrCreateAttribute<IteractDistance>();
            _ownerSpeed = _owner.unitAttributes.GetOrCreateAttribute<Speed>();
            _owner.navMeshAgent.speed = _ownerSpeed.value;
            _owner.navMeshAgent.SetDestination(_target);
            _owner.navMeshAgent.isStopped = false;
        }

        protected override void OnUpdateOrder()
        {
            if (Vector3.Distance(_owner.transform.position, _target) < _iteractDistance.value)
            {
                EndOrder();
            }

            _owner.navMeshAgent.speed = _ownerSpeed.value;
        }
    }
}
