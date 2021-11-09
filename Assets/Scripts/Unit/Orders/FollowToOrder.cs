using UnityEngine;
using UnitSpace.Attributes;
namespace UnitSpace.Orders
{

    public class FollowToOrder : Order
    {
        private Unit _target;
        private Vector3 _lastTargetPosition;
        private Speed _ownerSpeed;
        public FollowToOrder(Unit target) {
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
            _owner.navMeshAgent.SetDestination(_target.transform.position);
            _ownerSpeed = _owner.attributes.GetOrCreateAttribute<Speed>();
            _owner.navMeshAgent.speed = _ownerSpeed.value;
            _owner.navMeshAgent.isStopped = false;
        }
        public override void UpdateOrder()
        {
            if(!_target)
            {
                EndOrder();
                return;
            }
            SetDestinationByDistanceChange(2f);
            var distance = _owner.transform.position - _target.transform.position;
            if (distance.sqrMagnitude < 2f)
                EndOrder();

            _owner.navMeshAgent.speed = _ownerSpeed.value;
        }
        private void SetDestinationByDistanceChange(float change)
        {
            var comparePositions = _lastTargetPosition - _target.transform.position;
            if (comparePositions.sqrMagnitude > change)
                _owner.navMeshAgent.SetDestination(_target.transform.position);
        }
    }
}
