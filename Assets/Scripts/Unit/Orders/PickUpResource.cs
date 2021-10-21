using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitSpace.Interfaces;
using UnitSpace.Attributes;
using Resource;
namespace UnitSpace.Orders
{
    public class PickUpResource : IOrder
    {
        private Unit _owner;
        private ResourceContainer _resource;
        private IOrder.OrderState _state;
        public PickUpResource(Unit resource)
        {
            resource.TryGetComponent(out _resource);
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
        }
        public void StartOrder()
        {
            _state = IOrder.OrderState.InProgress;
            var distance = _owner.transform.position - _resource.transform.position;
            if(distance.sqrMagnitude > 2f && _resource.IsAvaliable)
            {
                MoveToResourceAlgorithm();
                return;
            }
            _owner.resourcePosition.TakeResource(_resource);
            EndOrder();
        }
        private void MoveToResourceAlgorithm()
        {
            _owner.unitOrders.AddOrder(new MoveToOrder(_resource.transform.position));
            _owner.unitOrders.AddOrder(this);
            EndOrder();
        }
        public void UpdateOrder(){}
    }
}
