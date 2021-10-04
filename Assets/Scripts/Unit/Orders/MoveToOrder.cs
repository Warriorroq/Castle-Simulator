using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitSpace.Interfaces;
using UnitSpace.Attributes;
using UnitSpace.Skills;
namespace UnitSpace.Orders
{

    public class MoveToOrder : IOrder
    {
        private IOrder.OrderState state;
        private Unit _owner;
        private Speed _ownerSpeed;
        private Vector3 _target;
        private float points;
        public MoveToOrder(Vector3 target)
        {
            _target = target;
        }
        public void EndOrder()
        {
            state = IOrder.OrderState.Finished;
            _owner.navMeshAgent.isStopped = true;
            _ownerSpeed.xpProgressValue += points;
            Debug.Log($"{_owner.name} finished tast \n{_ownerSpeed}");
        }
        public IOrder.OrderState GetState()
            => state;

        public void SetUnitOwner(Unit owner)
        {
            _owner = owner;
            points = Vector3.Distance(_owner.transform.position, _target) * 10;
        }

        public void StartOrder()
        {
            _ownerSpeed = _owner.attributes.GetAttribute<Speed>();
            _owner.skills.ActivateSkill<Dash>();
            _owner.navMeshAgent.speed = _ownerSpeed.value;
            _owner.navMeshAgent.SetDestination(_target);
            _owner.navMeshAgent.isStopped = false;
        }

        public void UpdateOrder()
        {
            if (Vector3.Distance(_owner.transform.position, _target) < 4)
                EndOrder();
        }
    }
}
