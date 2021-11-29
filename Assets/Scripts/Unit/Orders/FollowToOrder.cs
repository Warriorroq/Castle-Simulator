using UnityEngine;
using UnitSpace.Attributes;
namespace UnitSpace.Orders
{

    public class FollowToOrder : Order
    {
        private Transform _target;
        private Vector3 _lastTargetPosition;
        private Speed _ownerSpeed;
        public FollowToOrder(Transform target) {
            _target = target;
        }
        public override void EndOrder()
        {
            base.EndOrder();
            _owner.navMeshAgent.isStopped = true;
        }
        public override void StartOrder()
        {
            base.StartOrder();
            if (!_target)
            {
                EndOrder();
                return;
            }
            _owner.navMeshAgent.SetDestination(_target.position);
            _ownerSpeed = _owner.attributes.GetOrCreateAttribute<Speed>();
            _owner.navMeshAgent.speed = _ownerSpeed.value;
            _owner.navMeshAgent.isStopped = false;
        }
        protected override void OnUpdateOrder()
        {
            if (!_target)
            {
                EndOrder();
                return;
            }
            SetDestinationByDistanceChange(2f);
            var distance = _owner.transform.position - _target.position;
            if (distance.sqrMagnitude < 2f)
                EndOrder();

            _owner.navMeshAgent.speed = _ownerSpeed.value;
        }
        private void SetDestinationByDistanceChange(float change)
        {
            var comparePositions = _lastTargetPosition - _target.position;
            if (comparePositions.sqrMagnitude > change)
                _owner.navMeshAgent.SetDestination(_target.position);
        }
    }
}
