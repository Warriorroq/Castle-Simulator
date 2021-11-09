using UnityEngine;
using UnitSpace.Interfaces;
using UnitSpace.Attributes;
namespace UnitSpace.Orders
{
    public class MoveToOrder : Order
    {
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
            _ownerSpeed.xpProgressValue += points;
        }

        public override void StartOrder()
        {
            points = Vector3.Distance(_owner.transform.position, _target) * 10;
            _ownerSpeed = _owner.attributes.GetOrCreateAttribute<Speed>();
            _owner.navMeshAgent.speed = _ownerSpeed.value;
            _owner.navMeshAgent.SetDestination(_target);
            _owner.navMeshAgent.isStopped = false;
        }

        public override void UpdateOrder()
        {
            if (Vector3.Distance(_owner.transform.position, _target) < 1.2f)
                EndOrder();

            _owner.navMeshAgent.speed = _ownerSpeed.value;
        }
    }
}
